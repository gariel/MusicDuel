using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Text;
using System.Text.Json;
using Domain;
using Godot;

namespace MusicDuel.source;

public partial class Rooms : Control
{
	private VBoxContainer _vbox;
	private PackedScene _roomListItem;
	private HttpRequest _req;
	private HttpRequest _reqCreate;
	private HttpRequest _reqJoin;

	public override void _Ready()
	{
		_vbox = GetNode<VBoxContainer>("vboxRooms");
		_roomListItem = GD.Load<PackedScene>("res://RoomListItem.tscn");
		_req = GetNode<HttpRequest>("req");
		_reqCreate = GetNode<HttpRequest>("reqCreate");
		_reqJoin = GetNode<HttpRequest>("reqJoin");
		
		_req.Request(
			"http://localhost:8888/rooms",
			customHeaders: new []
			{
				"Content-Type: application/json",
				$"Authorization: Bearer {GameState.Token}",
			});
	}

	public override void _Process(double delta)
	{
	}

	public void _on_http_request_request_completed(long result, long responseCode, string[] headers, byte[] body)
	{
		var content = Encoding.UTF8.GetString(body);
		if (responseCode == (int)HttpStatusCode.OK)
		{
			foreach (var child in _vbox.GetChildren().ToImmutableArray())
			{
				_vbox.RemoveChild(child);
				child.QueueFree();
			}

			var rooms = JsonSerializer.Deserialize<List<RoomInfo>>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});
		    
			foreach (var room in rooms)
			{
				var item = _roomListItem.Instantiate<RoomListItem>();
				item.Room = room;
				item.OnJoin += RequestAndJoin;
				_vbox.AddChild(item);
			}
		}
	}

	public void _on_req_create_request_completed(long result, long responseCode, string[] headers, byte[] body)
	{
		var content = Encoding.UTF8.GetString(body);
		if (responseCode == (int)HttpStatusCode.OK)
		{
			var roomInfo = JsonSerializer.Deserialize<RoomInfo>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});
			GD.Print(roomInfo.Id);
			Join(roomInfo);
		}
	}

	public void _on_req_join_request_completed(long result, long responseCode, string[] headers, byte[] body)
	{
		GetTree().ChangeSceneToFile("res://Lobby.tscn");
	}
	
	public void RequestAndJoin(RoomInfo room)
	{
		GameState.Room = room;
		_reqJoin.Request(
			$"http://localhost:8888/rooms/{room.Id}/join",
			customHeaders: new []
			{
				"Content-Type: application/json",
				$"Authorization: Bearer {GameState.Token}",
			},
			method: HttpClient.Method.Post);
	}

	public void Join(RoomInfo room)
	{
		GameState.Room = room;
		GetTree().ChangeSceneToFile("res://Lobby.tscn");
	}

	public void _on_btn_create_pressed()
	{
		_reqCreate.Request(
			"http://localhost:8888/rooms",
			customHeaders: new []
			{
				"Content-Type: application/json",
				$"Authorization: Bearer {GameState.Token}",
			},
			method: HttpClient.Method.Post,
			requestData: "true");
	}
}