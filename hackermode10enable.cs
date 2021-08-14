using Godot;
using System;

public class hackermode10enable : Particles2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Node globals;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globals = GetNode("/root/Globals");
		this.Emitting = false;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		this.Emitting = (bool)globals.Get("in_hacker_mode");
	}
}
