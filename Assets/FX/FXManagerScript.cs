using Godot;
using System;

public class FXManagerScript : Node
{
	public static FXManagerScript Instance { get; private set; }

	public Sprite ScreenQuad { get; private set; }
	public ShaderMaterial ScreenMaterial { get; private set; }

	public override void _Ready()
	{
		base._Ready();

		Instance = this;

		this.ScreenQuad = this.GetNode<Sprite>("ScreenQuad");
		this.ScreenMaterial = this.ScreenQuad.Material as ShaderMaterial;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.IsPressed())
		{
			ShockScript.Instantiate(eventMouseButton.Position);
		}
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

		ShockScript.Update(delta);
	}
}
