using Godot;
using System;

public class Button : Godot.Button
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Pressed()
	{
		Node globals = this.GetNode<Node>("/root/Globals");
		globals.Call("set_player_is_alive", true);
		this.Hide();
		base._Pressed();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
