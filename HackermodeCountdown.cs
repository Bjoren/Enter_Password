using Godot;
using System;

public class HackermodeCountdown : Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	float countdown_rst = 5.0f;
	float countdown = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/* this.Position = new Vector2(0.0f, GetViewportRect().Size.y / 2.5f); */
		this.Text = "";
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if ((KeyList)keyEvent.Scancode == KeyList.Enter)
			{
				countdown = countdown_rst;
			}
		}
	}

	public override void _Process(float delta)
	{
		if (countdown > 0.0f)
		{
			countdown -= delta;
			this.Text = ((int)(countdown + 0.99f)).ToString();
		}
		else
			this.Text = "";
	}
}
