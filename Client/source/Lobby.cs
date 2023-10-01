using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Godot;

namespace MusicDuel.source;

public partial class Lobby : Control
{
	private Label _lbl;
	private string _buffer;

	public override void _Ready()
	{
		_lbl = GetNode<Label>("lbl");
		_lbl.Text = "";

		var task = new GodotThread();
		task.Start(Callable.From(() =>
		{
			var client = new System.Net.Http.HttpClient();
			client.DefaultRequestHeaders.Authorization = new  AuthenticationHeaderValue("Bearer", GameState.Token);

			var req = client.Send(new HttpRequestMessage
			{
				Content = new StringContent(""),
				Method = HttpMethod.Post,
				RequestUri = new Uri($"http://localhost:8888/messageing/rooms/{GameState.Room.Id}"),
			},HttpCompletionOption.ResponseHeadersRead);
			
			using var stream = req.Content.ReadAsStream();
			using var sr = new StreamReader(stream);
			while (!sr.EndOfStream)
			{
				var content = sr.ReadLine();
				_buffer += content + "\n";
			}
		}), GodotThread.Priority.High);
	}

	public override void _Process(double delta)
	{
		if (_buffer is null)
			return;
		
		if (_lbl.Text.Length < _buffer.Length)
			_lbl.Text += _buffer;
	}
}