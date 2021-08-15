using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class HangmanLogic : Node
{
	private string[] password_level1 = {
		"COW",
		"PET",
		"YOU",
		"JOB",
		"JAM",
		"AND",
		"ONE",
		"PEA",
		"PEE",
		"ASS",
		"OHL",
		"THE",
		"MAN",
		"HIM",
		"RUN",
		"CAR",
		"BUS",
		"DOG",
		"GIT",
		"FUN"
	};

	private string[] password_level2 = {
		"PANIC",
		"VENOM",
		"GNOME",
		"YACHT",
		"NINJA",
		"MAGMA",
		"FEVER",
		"ANVIL",
		"CACAO",
		"HORDE",
		"ONION",
		"ARROW",
		"CRIME",
		"MEMES",
		"WHALE"
	};

	private string[] password_level3 = {
		"FREEDOM",
		"AMERICA",
		"PUMPKIN",
		"HISTORY",
		"DIAMOND",
		"PROBLEM",
		"SAUSAGE",
		"POPCORN",
		"CHICKEN",
		"THUNDER",
		"SNOWMAN",
		"CALCIUM",
		"HIPSTER",
		"GOGGLES"
	};
	
	private string password = "hemligt";
	private int nrCorrectGuesses = 0;
	private const char mask = '_';
	private char[] guess = { };
	private Random rng = new Random();

	private List<char> hintChars = new List<char>();
	private List<char> greyHintChars = new List<char>();
	private int nrReceivedHints = 0;

	private Label passwordTextNode;
	private Label hintTextNode;
	private Label greyHintTextNode;

	private void Reset()
	{
		nrReceivedHints = 0;
		nrCorrectGuesses = 0;
		hintChars.Clear();
		greyHintChars.Clear();

		LoadPassword((int)this.GetNode("/root/Globals").Call("get_current_level"));
		LoadHintChars();
		SetGuessText();
		SetHintText();
	}

	public override void _Ready()
	{
		passwordTextNode = GetNode<Label>("../PasswordText");
		hintTextNode = GetNode<Label>("../HintText");
		greyHintTextNode = GetNode<Label>("../GreyHintText");
		Reset();
	}

	private void LoadPassword(int level)
	{
		switch(level)
		{
			case 0: password = password_level1[rng.Next(password_level1.Length)] + ""; break;
			case 1: password = password_level2[rng.Next(password_level2.Length)] + ""; break;
			case 2: password = password_level3[rng.Next(password_level3.Length)] + ""; break;
			default: password = ""; break;
		}

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

	public bool GuessOneChar(char guessChar)
	{
		FXManagerScript.Instance.DoScreenShake(0.05f, 0.01f);

		bool isPlace = false;
		if (HasGuessingLeft() && password[nrCorrectGuesses] == guessChar)
		{
			guess[nrCorrectGuesses] = guessChar;
			SetGreyChar(guessChar);
			nrCorrectGuesses++;
			isPlace = nrCorrectGuesses == password.Length;
			this.GetNode<AudioStreamPlayer2D>("../SfxCorrect").Play();
		}
		else if (!IsAlreadyGuessed(guessChar))
		{
			this.GetNode("../../Hangman").Call("reset_animate_enter_hackermode");
			this.GetNode<AudioStreamPlayer2D>("../SfxIncorrect").Play();
		}
		
		if (!HasGuessingLeft())
		{
			LevelCompleted();
		}

		SetHintText();
		SetGuessText();
		return isPlace;
	}

	private bool IsAlreadyGuessed(char guessChar)
	{
		return new List<char>(guess).GetRange(0, nrCorrectGuesses).Contains(guessChar);
	}

	public bool HasGuessingLeft()
	{
		return nrCorrectGuesses < password.Length;
	}	

	private void SetGreyChar(char c)
	{
		for(int i = 0; i < hintChars.Count; i++)
		{
			if(hintChars[i] == c)
			{
				greyHintChars[i] = c;
				i = hintChars.Count;
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
		} else if (nrCorrectGuesses < password.Length)
		{
			GuessOneChar(password[nrCorrectGuesses]);
		}

		if (nrCorrectGuesses == password.Length)
		{
			LevelCompleted();
		}
		
		return flace;
	}

	public void LevelCompleted()
	{
		FXManagerScript.Instance.DoScreenShake(0.5f, 0.05f);

		this.GetNode("../../TwinStick").Call("kill_enemies");
		this.GetNode("../../Hangman").Call("reset_animate_enter_hackermode");
		this.GetNode("/root/Globals").Call("init");

		int level = (int)this.GetNode("/root/Globals").Call("get_current_level");
		this.GetNode("/root/Globals").Call("reset_current_difficulty");
		this.GetNode("/root/Globals").Call("set_current_level", level + 1);
		
		Reset();
	}
}
