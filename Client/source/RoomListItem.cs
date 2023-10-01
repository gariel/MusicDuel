using System;
using Domain;
using Godot;

namespace MusicDuel.source;

public partial class RoomListItem : HBoxContainer
{
	private Label _lblId;
	private Label _lblOwner;
	private Button _btnJoin;
	private bool _reset = true;

	public RoomInfo Room { get; set; }
	public event Action<RoomInfo> OnJoin;

	public override void _Ready()
	{
		_lblId = GetNode<Label>("lblId");
		_lblOwner = GetNode<Label>("lblOwner");
		_btnJoin = GetNode<Button>("btnJoin");
		
		_lblId.Text = Room.Id.ToString();
		_lblOwner.Text = Room.Host;
		_btnJoin.Disabled = Room.IsPlaying;
	}

	public void _on_btn_join_pressed()
		=> OnJoin?.Invoke(Room);
}