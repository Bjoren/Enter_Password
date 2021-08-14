using Godot;
using System;

public class HangmanInput : Godot.Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private string [] password_alternatives = {"HEMLIGT", "BLUNDER", "SAJDKICK"};
	private string password = "hemligt";
	private const char mask = '*';
	private char [] guess = {};
	private Random rng = new Random();

	private void LoadPassword() {
		password = password_alternatives[rng.Next(password_alternatives.Length)] + "";
		guess = new char[password.Length];
		for (int i = 0; i < password.Length; ++i) {
			guess[i] = mask;
		}
	}

	public bool GuessOneChar(char g) {
		bool isPlace = false;
		if (password.Contains(g + "")) {
			for (int i = 0; i < password.Length; ++i) {
				if (g == password[i]) {
					guess[i] = g;
				}
			}
			isPlace = true;
		}
		//GD.Print(new String(guess));
		//GD.Print(password + "");
		return isPlace;
	}

	public void SetGuessText() {
		this.Text = new string(guess);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadPassword();
	}
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Scancode > 64 && keyEvent.Scancode < 91)
			{
				GuessOneChar((char)keyEvent.Scancode);
			}
		}
		SetGuessText();
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
