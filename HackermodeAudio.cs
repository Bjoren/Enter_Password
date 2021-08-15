using Godot;
using System;

public class HackermodeAudio : AudioStreamPlayer2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	bool current = false;
	Node globals;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globals = GetNode("/root/Globals");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (current != (bool)globals.Get("in_hacker_mode")) {
			current = !current;
			this.Play();
		}
	}
}
