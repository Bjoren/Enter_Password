using Godot;
using System;

public class HangmanInput : Node
{
	private HangmanLogic hangmanLogic;

	public override void _Ready()
	{
		hangmanLogic = GetNode<HangmanLogic>("../HangmanLogic");
	}
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if (keyEvent.Scancode == 32)
			{
				hangmanLogic.GiveHintChar();
			}
			if (keyEvent.Scancode > 64 && keyEvent.Scancode < 91)
			{
				hangmanLogic.GuessOneChar((char)keyEvent.Scancode);
			}
		}
	}
}
