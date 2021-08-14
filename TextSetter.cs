using Godot;
using System;

public class TextSetter : Label
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void SetText(string newText)
	{
		this.Text = newText;
	}
}
