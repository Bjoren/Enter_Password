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

		Node globals = this.GetNode<Node>("/root/Globals");
		globals.Set("fx_manager", this);

		this.ScreenQuad = this.GetNode<Sprite>("ScreenQuad");
		this.ScreenMaterial = this.ScreenQuad.Material as ShaderMaterial;
	}

	public void InstantiateShock(Vector2 position, float radialSpeed, float targetRadius, float targetAmpliture)
	{
		ShockScript.Instantiate(new ShockScript()
		{
			position = position,
			radialSpeed = radialSpeed,
			targetRadius = targetRadius,
			targetAmpliture = targetAmpliture,
		});
	}

	/*
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.IsPressed())
		{
			InstantiateShock(eventMouseButton.Position, 600f, 200f, 0.1f);
		}
	}
	*/

	public override void _Process(float delta)
	{
		base._Process(delta);

		ShockScript.Update(delta);
	}
}
