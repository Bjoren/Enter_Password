using Godot;
using System;

public class Hackermode : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public float cooldown = 10.0f;
	Node globals;

	private bool start_hack = false;
	private const float scale1_rst = 0.2f;
	private float scale1_timer = 0.0f;
	private const float scale2_rst = 0.1f;
	private float scale2_timer = 0.0f;

	public void reset_animate_enter_hackermode() {
		start_hack = false;
		Engine.TimeScale = 1.0f;
		globals.Set("in_hacker_mode", false);
		scale1_timer = scale1_rst;
		scale2_timer = scale2_rst;
		this.Scale = new Vector2(1,1);
		cooldown = 10.0f;
	}

	private void animate_enter_hackermode(float delta)
	{
		if (!start_hack)
			return;
		scale1_timer -= delta;
		float scale1_frac = Math.Max((scale1_timer / scale1_rst), 0.0f);
		this.Scale = new Vector2(scale1_frac, scale1_frac);

		/* this.Scale = new Vector2(Math.Min(1.0f, scale1_frac * 2.0f), scale1_frac * 5.0f + .3f); */
		if (scale1_timer < 0.0f)
		{
			scale2_timer -= delta;
			float scale2_frac = Math.Min(1.0f, 1.0f - (scale2_timer / scale2_rst));
			this.Scale = new Vector2(3.0f,3.0f*scale2_frac);
			this.Position = new Vector2(-GetViewportRect().Size.x, GetViewportRect().Size.y / 2 - 50.0f);
			/* this.Position = new Vector2(-GetViewportRect().Size.x*scale2_frac, GetViewportRect().Size.y / 2 - 50.0f); */
			if (scale2_timer < 0.0f) {
				globals.Set("in_hacker_mode", true);
				Engine.TimeScale = 0.1f;
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globals = GetNode("/root/Globals");
		reset_animate_enter_hackermode();
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if ((KeyList)keyEvent.Scancode == KeyList.Enter)
			{
				if (cooldown < 0.0f) {
					start_hack = true;
					GetNode<Label>("../Hangman/GuessCooldown").Text = "";
				}
				else {
					if ((bool)globals.Get("in_hacker_mode") == false) {
						GetNode<Label>("../Hangman/GuessCooldown").Text = "Hack cooldown: " + ((int)(cooldown + 0.9f)).ToString();
					}
					else
					{
						GetNode<Label>("../Hangman/GuessCooldown").Text = "";
					}
				}
			}
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		cooldown -= delta;
		animate_enter_hackermode(delta);
	}
}
