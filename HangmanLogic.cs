using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class HangmanLogic : Node
{
	private string[] password_alternatives = { "HEMLIGT", "BLUNDER", "SAJDKICK" };
	private string password = "hemligt";
	private const char mask = '_';
	private char[] guess = { };
	private Random rng = new Random();

	private List<char> hintChars = new List<char>();
	private List<char> greyHintChars = new List<char>();
	private int nrReceivedHints = 0;

	private Label passwordTextNode;
	private Label hintTextNode;
	private Label greyHintTextNode;

	public override void _Ready()
	{
		passwordTextNode = GetNode<Label>("../PasswordText");
		hintTextNode = GetNode<Label>("../HintText");
		greyHintTextNode = GetNode<Label>("../GreyHintText");
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
		List<char> passwordList = new List<char>(password);
		passwordList = passwordList.OrderBy(a => rng.Next()).ToList();

		hintChars = passwordList;
		hintChars.ForEach(c => greyHintChars.Add(' '));
	}

	private void SetGuessText()
	{
		passwordTextNode.Text = new string(guess);
	}

	private void SetHintText()
	{
		List<char> whiteChars = new List<char>();

		for(int i = 0; i < hintChars.Count; i++)
		{
			if (hintChars[i] == greyHintChars[i])
			{
				whiteChars.Add(' ');
			} else
			{
				whiteChars.Add(hintChars[i]);
			}
		}

		hintTextNode.Text = new string(whiteChars.GetRange(0, nrReceivedHints).ToArray());
		greyHintTextNode.Text = new string(greyHintChars.GetRange(0, nrReceivedHints).ToArray());
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
					SetGreyChar(g);
				}
			}
			isPlace = true;
			SetHintText();
			SetGuessText();
		}
		return isPlace;
	}

	private void SetGreyChar(char c)
	{
		for(int i = 0; i < hintChars.Count; i++)
		{
			if(hintChars[i] == c)
			{
				greyHintChars[i] = c;
			}
		}
	}

	public bool GiveHintChar()
	{
		bool flace = true;
		if (nrReceivedHints < hintChars.Count)
		{
			nrReceivedHints++;
			SetHintText();
		}
		return flace;
	}
}