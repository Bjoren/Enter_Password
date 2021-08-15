using Godot;
using System;

public class HackermodeCountdown : Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private float countdown_rst = 7.0f;
	private float countdown = 0.0f;
	private bool reset = false;
	Node globals;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/* this.Position = new Vector2(0.0f, GetViewportRect().Size.y / 2.5f); */
		this.Text = "";
		globals = GetNode("/root/Globals");
		countdown = countdown_rst;
	}

	public override void _Process(float delta)
	{
		bool isHackerMode = (bool)globals.Get("in_hacker_mode");
		if (countdown > 0.0f && isHackerMode)
		{
			reset = true;
			countdown -= delta / Engine.TimeScale;
			this.Text = ((int)(countdown + 0.99f)).ToString();
		}
		else {
			this.Text = "";
			if (reset) {
				GetNode<Hackermode>("../../Hangman").reset_animate_enter_hackermode();
				reset = false;
				countdown = countdown_rst;
			}
		}
	}
}
