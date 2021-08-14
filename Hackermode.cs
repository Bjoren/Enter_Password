using Godot;
using System;

public class Hackermode : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public float cooldown = 10.0f;

	private bool start_hack = true;
	private const float scale1_rst = 0.2f;
	private float scale1_timer = 0.0f;
	private const float scale2_rst = 0.07f;
	private float scale2_timer = 0.0f;

	private void reset_animate_enter_hackermode() {
		if (!start_hack)
			return;
		start_hack = false;
		scale1_timer = scale1_rst;
		scale2_timer = scale2_rst;
		this.Scale = new Vector2(1,1);
	}

	private void animate_enter_hackermode(float delta)
	{
		if (!start_hack)
			return;
		scale1_timer -= delta;
		float scale1_frac = Math.Max((scale1_timer / scale1_rst), 0.0f);
		this.Scale = new Vector2(Math.Min(1.0f, scale1_frac * 2.0f), scale1_frac * 5.0f + .3f);
		if (scale1_timer < 0.0f) {
			scale2_timer -= delta;
			float scale2_frac = Math.Min(1.0f, 1.0f - (scale2_timer / scale2_rst));
			this.Scale = new Vector2(3.0f,3.0f*scale2_frac);
			this.Position = new Vector2(-GetViewportRect().Size.x*scale2_frac, GetViewportRect().Size.y / 2 - 50.0f);
			/* reset_animate_enter_hackermode(); */
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		reset_animate_enter_hackermode();
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if ((KeyList)keyEvent.Scancode == KeyList.Enter)
			{
				GD.Print("Test whats good");
				GD.Print(this.Position);
				start_hack = true;
			}
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		animate_enter_hackermode(delta);
	}
}
