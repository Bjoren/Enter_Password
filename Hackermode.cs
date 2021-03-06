using Godot;
using System;

public class Hackermode : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public const float cooldown_rst = 10.0f;
	public float cooldown = 10.0f;
	private Label lab;
	private float colcol = 0.0f;
	Node globals;

	private const float scale1_rst = 0.04f;
	private float scale1_timer = 0.0f;
	private const float scale2_rst = 0.03f;
	private float scale2_timer = 0.0f;

	private const float hck_rst = 1.75f;
	private float hck_timer = 0.0f;
	private bool reset_hackermode = false;

	public void reset_animate_enter_hackermode() {
		reset_hackermode = true;
	}

	private void hacker_reset(float delta) {
		if (!reset_hackermode)
			return;

		var player = GetNode("../TwinStick/Player");
		var second_wind_lvl = (int)player.Get("second_wind_lvl");
		var current_level = (int)globals.Call("get_current_level");
		if (second_wind_lvl == current_level)
		{
			player.Call("hurt");
		}

		globals.Set("in_hacker_mode", false);
		float true_delta = delta / Engine.TimeScale;
		hck_timer -= true_delta;
		float hck_frac = Math.Min(1.0f, 1.0f - (hck_timer / hck_rst));
		Engine.TimeScale = Math.Max(0.1f, hck_frac);
		scale1_timer = scale1_rst;
		scale2_timer = scale2_rst;
		this.Position = new Vector2(0, 0);
		this.Scale = new Vector2(1,1);
		cooldown = cooldown_rst;
		colcol = 0f;
		if (hck_timer > 0.0f)
			return;
		Engine.TimeScale = 1.0f;
		reset_hackermode = false;
		hck_timer = hck_rst;
	}

	private void animate_enter_hackermode(float delta)
	{
		if ((bool)globals.Get("in_hacker_mode") == false)
			return;
		if (Engine.TimeScale > 0.9f) {
			Engine.TimeScale = 0.1f;
			delta *= 0.1f;
		}
		scale1_timer -= delta;
		float scale1_frac = Math.Max((scale1_timer / scale1_rst), 0.0f);
		this.Scale = new Vector2(scale1_frac, scale1_frac);

		if (scale1_timer < 0.0f)
		{
			scale2_timer -= delta;
			float scale2_frac = Math.Min(1.0f, 1.0f - (scale2_timer / scale2_rst));
			this.Scale = new Vector2(3.0f,3.0f*scale2_frac);
			this.Position = new Vector2(-GetViewportRect().Size.x, GetViewportRect().Size.y / 2 - 50.0f);
			/* this.Position = new Vector2(-GetViewportRect().Size.x*scale2_frac, GetViewportRect().Size.y / 2 - 50.0f); */
			if (scale2_timer < 0.0f) {
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globals = GetNode("/root/Globals");
		reset_animate_enter_hackermode();
		lab= GetNode<Label>("../Hangman/GuessCooldown");
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
		{
			bool isPlayerAlive = (bool)globals.Call("get_player_is_alive");
			if ((KeyList)keyEvent.Scancode == KeyList.Enter && isPlayerAlive)
			{
				if (cooldown < 0.0f) {
					globals.Set("in_hacker_mode", true);
					lab.Text = "";
				}
				else {
					if ((bool)globals.Get("in_hacker_mode") == false) {
						//lab.Text = "Hack cooldown: " + ((int)(cooldown + 0.9f)).ToString();
						colcol = 3.0f;
					}
					else
					{
						//lab.Text = "";
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
		colcol -= delta;
		if (colcol < 0.0f)
			lab.Text = "";
		else
		{
			int num = (int)(cooldown + 0.9f);
			lab.Text = num > 0 ? "Hack on cooldown: " + num.ToString() : "HACK IS READY!";
		}
		hacker_reset(delta);
	}
}
