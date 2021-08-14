using Godot;
using System;

public class HangmanLogic : Node
{
	private string[] password_alternatives = { "HEMLIGT", "BLUNDER", "SAJDKICK" };
	private string password = "hemligt";
	private const char mask = '_';
	private char[] guess = { };
	private Random rng = new Random();

	private char[] hintChars = { };
	private int nrReceivedHints = 0;

	private Label passwordTextNode;
	private Label hintTextNode;

	public override void _Ready()
	{
		passwordTextNode = GetNode<Label>("../PasswordText");
		hintTextNode = GetNode<Label>("../HintText");
		LoadPassword();
		LoadHintChars();
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

	private void LoadHintChars()
	{
		hintChars = new char[password.Length];
	}

	private void SetGuessText()
	{
		passwordTextNode.Text = new string(guess);
	}

	private void SetHintText()
	{
		hintTextNode.Text = new string(hintChars);
	}

	public bool GuessOneChar(char g)
	{
		bool isPlace = false;
		if (password.Contains(g + ""))
		{
			for (int i = 0; i < password.Length; ++i)
			{
				if (g == password[i])
				{
					guess[i] = g;
				}
			}
			isPlace = true;
			SetGuessText();
		}
		return isPlace;
	}

	public bool GiveHintChar(char hintChar)
	{
		bool flace = true;

		if (nrReceivedHints < hintChars.Length)
		{
			hintChars[nrReceivedHints] = hintChar;
			nrReceivedHints++;
		}

		SetHintText();
		return flace;
	}
}
