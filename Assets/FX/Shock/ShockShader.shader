shader_type canvas_item;

const float PI = 3.14159265358979323846;
const float TAU = PI * 2.0;

uniform vec2 SCREENSIZE = vec2(1920.0, 1080.0);
uniform vec3 testColor = vec3(0.0, 1.0, 0.0);

uniform vec4 shock0;
uniform vec4 shock1;
uniform vec4 shock2;
uniform vec4 shock3;
uniform vec4 shock4;
uniform vec4 shock5;
uniform vec4 shock6;
uniform vec4 shock7;

float smoothCurve(float x) { // [lin 0,1]
	return (1.0 + cos(((x * TAU) + PI))) / 2.0;
}

float remap(float x, float mi, float ma) {
	return clamp((x - mi) / (ma - mi), mi, ma);
}

float calShockAmp(vec2 screenXY, vec4 shock) {
	vec2 xy = shock.xy;
	float radius = shock.z;
	float active = shock.w;
	
	float dist = distance(screenXY, xy);
	
	float width = 25.0;
	float hWidth = width / 2.0;
	
	//remap(dist / radius,);
	
	//float x = clamp((dist - hWidth) / dist, 0.0, 1.0);
	
	//float amp = step(dist, radius);
	float amp = 1.0 - clamp(abs(dist - radius) / width, 0.0, 1.0); // [0,1,0] linear
	
	amp *= active;
	
	return amp;
}

void fragment() {
	vec2 screenXY = vec2(SCREEN_UV.x, 1.0 - SCREEN_UV.y) * SCREENSIZE;
	
	float shockAmp = 0.0;
	shockAmp += calShockAmp(screenXY, shock0);
	shockAmp += calShockAmp(screenXY, shock1);
	shockAmp += calShockAmp(screenXY, shock2);
	shockAmp += calShockAmp(screenXY, shock3);
	shockAmp += calShockAmp(screenXY, shock4);
	shockAmp += calShockAmp(screenXY, shock5);
	shockAmp += calShockAmp(screenXY, shock6);
	shockAmp += calShockAmp(screenXY, shock7);
	
	vec3 screenColor = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0).xyz;
	float t = (1.0 + cos(TIME * PI)) / 2.0; // [0,1]
	vec3 finalColor = mix(screenColor, vec3(1.0), vec3(shockAmp));
    COLOR = vec4(finalColor, 1.0);
}