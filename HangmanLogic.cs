using Godot;
using System;

public class HangmanLogic : Node
{
	private string[] password_alternatives = { "HEMLIGT", "BLUNDER", "SAJDKICK" };
	private string password = "hemligt";
	private const char mask = '*';
	private char[] guess = { };
	private Random rng = new Random();

	private Label passwordTextNode;

	public override void _Ready()
	{
		passwordTextNode = GetNode<Label>("../PasswordText");
		LoadPassword();
		SetGuessText();
	}
	private void LoadPassword()
	{
		password = password_alternatives[rng.Next(password_alternatives.Length)] + "";
		guess = new char[password.Length];
		for (int i = 0; i < password.Length; ++i)
		{
			guess[i] = mask;
		}
	}
	public void SetGuessText()
	{
		passwordTextNode.Text = new string(guess);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
