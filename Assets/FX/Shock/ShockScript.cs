using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class ShockScript
{
	private const int SHOCKCOUNT = 8; 

	private static List<ShockInstance> Instances { get; } = new List<ShockInstance>();

	public static void Instantiate(Vector2 position, float radialSpeed = 200f, float targetTime = 10f)
	{
		if (Instances.Count < SHOCKCOUNT)
		{
			Instances.Add(new ShockInstance()
			{
				position = position,
				radialSpeed = radialSpeed,
				targetTime = targetTime,
			});
		}
	}

	public static void Update(float delta)
	{
		Instances.ForEach((instance, index) =>
		{
			float x = instance?.position.x ?? 0;
			float y = instance?.position.y ?? 0;
			float radius = instance?.currentRadius ?? 0;
			float active = instance != null ? 1 : 0;

			var vec4 = new Color(x, y, radius, active);

			FXManagerScript.Instance.ScreenMaterial.SetShaderParam("shock" + index, vec4);
		},
		SHOCKCOUNT);

		Vector2 screenSize = new Vector2(FXManagerScript.Instance.ScreenQuad.Transform.Scale);
		FXManagerScript.Instance.ScreenMaterial.SetShaderParam("SCREENSIZE", screenSize);

		Instances.ForEach((x) =>
		{
			x.currentTime += delta;
			x.currentRadius = x.radialSpeed * x.currentTime;
		});
		Instances.SelectRemove(x => x.currentTime > x.targetTime);
	}

	public class ShockInstance
	{
		public Vector2 position;
		public float radialSpeed;
		public float targetTime;

		public float currentRadius = 0f;
		public float currentTime = 0f;
	}
}
