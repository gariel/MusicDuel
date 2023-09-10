using System.Net;
using System.Text;
using System.Text.Json;
using Godot;
using MusicDuel;

public partial class Login : Control
{
	private TextEdit _txtUserName;
	private TextEdit _txtPassword;
	private HttpRequest _req;
	private Control _grpError;
	private Label _lblError;

	public override void _Ready()
	{
		_txtUserName = GetNode<TextEdit>("txtUserName");
		_txtPassword = GetNode<TextEdit>("txtPassword");
		_grpError = GetNode<Control>("grpError");
		_lblError = _grpError.GetNode<Label>("lblError");
		_req = GetNode<HttpRequest>("req");
		
		
		_grpError.Visible = false;
	}

	public override void _Process(double delta)
	{
	}

	public void _on_btn_ok_pressed()
	{
		var username = _txtUserName.Text;
		var password = _txtPassword.Text;

		_req.Request(
			"http://localhost:8888/login",
			customHeaders: new [] {"Content-Type: application/json"},
			method: HttpClient.Method.Post,
			requestData: JsonSerializer.Serialize(new LoginInformation
			{
				UserName = username,
				Password = password,
			}));
	}
	
	private void _on_req_request_completed(long result, long responseCode, string[] headers, byte[] body)
	{
		var content = Encoding.UTF8.GetString(body);
	    if (responseCode == (int)HttpStatusCode.OK)
	    {
	        var json = Json.ParseString(content).AsGodotDictionary();
	        var token = json["token"].ToString();

	        GameState.UserName = _txtUserName.Text;
	        GameState.Token = token;

	        GetTree().ChangeSceneToFile("res://Rooms.tscn");
	    }
	    else
	    {
		    _lblError.Text = content;
		    _grpError.Visible = true;
	    }
    }
}

public class LoginInformation
{
	public string UserName { get; set; }
	public string Password { get; set; }
}
