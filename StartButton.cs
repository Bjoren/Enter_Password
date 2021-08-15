using Godot;
using System;

public class StartButton : Godot.Button
{
	Menu menuNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menuNode = this.GetNode<Menu>("../../Menu");
	}

	public override void _Pressed()
	{
		Node globals = this.GetNode<Node>("/root/Globals");
		globals.Call("set_player_is_alive", true);
		menuNode.HasBeenAlive = true;
		GetNode("/root/Globals").Call("set_current_level", 0);
		this.Hide();
		base._Pressed();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
