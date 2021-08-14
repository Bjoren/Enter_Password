using Godot;
using System;

public class RetryButton : Button
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Hide();
	}

	public override void _Pressed()
	{
		GetTree().ReloadCurrentScene();
		GetNode("/root/Globals").Call("_init");
		base._Pressed();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
