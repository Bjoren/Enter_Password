using Godot;
using System;

public class HangmanInput : Node
{
	private HangmanLogic hangmanLogic;
	private Node globals;
	
	public override void _Ready()
	{
		hangmanLogic = GetNode<HangmanLogic>("../HangmanLogic");
		globals = GetNode("/root/Globals");
	}
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Scancode == 32)
			{
				hangmanLogic.GiveHintChar();
			}
			if (keyEvent.Scancode > 64 && keyEvent.Scancode < 91 && (bool)globals.Get("in_hacker_mode"))
			{
				hangmanLogic.GuessOneChar((char)keyEvent.Scancode);
				this.GetNode<AudioStreamPlayer2D>("../SfxType").Play();
			}
		}
	}
}
