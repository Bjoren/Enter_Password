using Godot;
using System;

public class Menu : Node
{
	Button retryButton;
	public bool HasBeenAlive { get;  set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		retryButton = this.GetNode<Button>("RetryButton");
		Node globals = this.GetNode<Node>("/root/Globals");
		globals.Call("set_player_is_alive", false);
		this.HasBeenAlive = false;
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		Node globals = this.GetNode<Node>("/root/Globals");
		bool isAlive = (bool)globals.Call("get_player_is_alive");
		if (!isAlive && HasBeenAlive)
		{
			retryButton.Show();
		}
  }
}
