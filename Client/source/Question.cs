using System;
using System.Net.Http;
using System.Text.Json;
using Domain;
using Godot;
using MusicDuel.source;

public partial class Question : Control
{
	private RichTextLabel _lblQuestion;
	private Control _grp;
	private AudioStreamPlayer _player;

	public GameQuestion GameQuestion { get; set; } = aaa.Q;

	public override void _Ready()
	{
		var client = new System.Net.Http.HttpClient();
		client.DefaultRequestHeaders.Add("Authorization", "Bearer Gabriel");
		var content = client.GetStringAsync("http://localhost:8888/game").GetAwaiter().GetResult();
		GameQuestion = JsonSerializer.Deserialize<GameQuestion>(content, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		});
		
		_lblQuestion = GetNode<RichTextLabel>("lblQuestion");
		_grp = GetNode<Control>("grp");
		_player = GetNode<AudioStreamPlayer>("player");

		var text = GameQuestion.GameType switch
		{
			GameType.Artist => "Qual o artista da música?",
			GameType.Music => "Pablo, Qual a música?",
			GameType.MusicWithArtits => "Qual música e artista?",
			_ => throw new ArgumentOutOfRangeException(),
		};
		_lblQuestion.Text = $"[center][wave]{text}";
		
		var stream = new AudioStreamMP3();
		stream.Data = Convert.FromBase64String(GameQuestion.Mp3Base64);
		_player.Stream = stream;
		
		var optionTupples = new[]
		{
			(_grp.GetNode<Button>("btn1"), _grp.GetNode<TextureRect>("texture1")),
			(_grp.GetNode<Button>("btn2"), _grp.GetNode<TextureRect>("texture2")),
			(_grp.GetNode<Button>("btn3"), _grp.GetNode<TextureRect>("texture3")),
			(_grp.GetNode<Button>("btn4"), _grp.GetNode<TextureRect>("texture4")),
		};
		
		for (var i = 0; i < optionTupples.Length; i++)
		{
			var tuple = optionTupples[i];
			var btn = tuple.Item1;
			var rect = tuple.Item2;
			var item = GameQuestion.Items[i];
			var png = Convert.FromBase64String(item.ImageBase64);

			btn.Text = item.Title;

			var image = new Image();
			image.LoadJpgFromBuffer(png);
			
			var texture = new ImageTexture();
			texture.SetImage(image);
			
			rect.Texture = texture;
		}

		_player.Play();
	}
}