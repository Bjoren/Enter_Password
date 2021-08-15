using Godot;
using System;

public class FXManagerScript : Node
{
	public static FXManagerScript Instance { get; private set; }

	public Sprite ScreenQuad { get; private set; }
	public ShaderMaterial ScreenMaterial { get; private set; }

	private Random rng = new Random();
	private float screenShakeTime = 0f;
	private float screenShakeAmp = 1f;

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

	public void DoScreenShake(float screenShakeTime, float screenShakeAmp)
	{
		this.screenShakeTime = screenShakeTime;
		this.screenShakeAmp = screenShakeAmp;
	}

	/*
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.IsPressed())
		{
			//InstantiateShock(eventMouseButton.Position, 600f, 200f, 0.1f);
			//DoScreenShake(0.25f, 0.005f);
		}
	}
	*/

	public override void _Process(float delta)
	{
		base._Process(delta);

		ShockScript.Update(delta);

		UpdateScreenShake(delta);
	}

	private void UpdateScreenShake(float delta)
	{
		if (screenShakeTime > 0f)
		{
			Vector2 screenShakeUVOffset = new Vector2(rng.Next(-100, 100) / 100f, rng.Next(-100, 100) / 100f);
			float falloff = Mathf.Clamp(screenShakeTime / 0.15f, 0f, 1f);
			this.ScreenMaterial.SetShaderParam("screenShakeUVOffset", screenShakeUVOffset * screenShakeAmp * falloff);

			screenShakeTime -= delta;
		}
	}
}
