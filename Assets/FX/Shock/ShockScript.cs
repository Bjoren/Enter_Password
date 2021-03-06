using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class ShockScript
{
	private const int SHOCKCOUNT = 16;

	public Vector2 position;
	public float radialSpeed;
	public float targetRadius;
	public float targetAmpliture;

	public float time = 0f;
	public float currentRadius = 0f;
	public float currentAmpliture = 0f;

	private static List<ShockScript> Instances { get; } = new List<ShockScript>();

	public static void Instantiate(ShockScript instance)
	{
		if (Instances.Count < SHOCKCOUNT)
		{
			Instances.Add(instance);
		}
	}

	public static void Update(float delta)
	{
		Instances.ForEach((instance, index) =>
		{
			float x = instance?.position.x ?? 0;
			float y = instance?.position.y ?? 0;
			float radius = instance?.currentRadius ?? 0;
			float ampliture = instance?.currentAmpliture ?? 0;

			var vec4 = new Color(x, y, radius, ampliture);

			FXManagerScript.Instance.ScreenMaterial.SetShaderParam("shock" + index, vec4);
		},
		SHOCKCOUNT);

		Vector2 screenSize = new Vector2(FXManagerScript.Instance.ScreenQuad.Transform.Scale);
		FXManagerScript.Instance.ScreenMaterial.SetShaderParam("SCREENSIZE", screenSize);

		Instances.ForEach((x) =>
		{
			x.time += delta;
			x.currentRadius = x.radialSpeed * x.time;
			float val = x.currentRadius / x.targetRadius;
			x.currentAmpliture = (1f - (val * val)) * x.targetAmpliture;
		});

		Instances.SelectRemove(x => x.currentRadius > x.targetRadius);
	}
}
