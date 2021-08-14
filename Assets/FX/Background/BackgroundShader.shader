shader_type canvas_item;

uniform vec3 gridColor = vec3(1.0, 0.1, 1.0);
uniform float gridAmp = 0.06;
uniform	vec2 gridScale = vec2(10.0, 20.0);
uniform	float gridWidth = 0.02;

uniform vec3 texColor = vec3(1.0, 0.1, 1.0);
uniform float texAmp = 0.2;

void fragment() {
	vec2 uvOffset = vec2(TIME * -0.01, TIME * -0.1);
	vec2 uv = fract(UV + uvOffset);
	vec3 finalTexColor = texture(TEXTURE, uv).xyz * texColor * texAmp;
	
	vec2 gridUV = fract(UV * gridScale);
	vec2 stepRes = step(gridUV, vec2(gridWidth, gridWidth));
	float gridVal = clamp(stepRes.x + stepRes.y, 0, 1);
	
	vec3 finalGridColor = gridColor *  gridVal * gridAmp;
	
	vec3 finalColor = finalTexColor + finalGridColor;
	
	COLOR = vec4(finalColor, 1.0);
	
	//vec2 uvOffset = vec2(TIME * -0.01, TIME * -0.1);
	//vec2 uv = fract(UV + uvOffset);
	//vec3 texColor = texture(TEXTURE, uv).xyz;
	//COLOR = vec4(texColor * color * amp, 1.0);
}