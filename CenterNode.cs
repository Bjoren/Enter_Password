using Godot;
using System;

public class CenterNode : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Position = new Vector2(GetViewportRect().Size.x / 2.0f, GetViewportRect().Size.y / 7.0f);
		this.Scale = new Vector2(3.0f, 3.0f);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
