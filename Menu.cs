using Godot;
using System;

public class Menu : Node
{
	Label titleLabel;
	Label descLabel;
	Button retryButton;
	public bool HasBeenAlive { get;  set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		retryButton = this.GetNode<Button>("RetryButton");
		titleLabel = this.GetNode<Label>("Title");
		descLabel = this.GetNode<Label>("Desc");
		
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
		
		if (isAlive || HasBeenAlive)
		{
			titleLabel.Hide();
			descLabel.Hide();
		}
  }
}
