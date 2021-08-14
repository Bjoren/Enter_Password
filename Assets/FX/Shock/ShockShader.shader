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

void calShockFX(vec2 screenXY, vec4 shock, inout vec2 out_uvOffset, inout float out_amp) {
	vec2 xy = shock.xy;
	float radius = shock.z;
	float ampliture = shock.w;
	
	float dist = distance(screenXY, xy);
	
	float width = 25.0;
	float hWidth = width / 2.0;

	float amp = 1.0 - clamp(abs(dist - radius) / width, 0.0, 1.0); // [0,1,0] linear
	vec2 uvOffset = normalize(screenXY - xy) * amp;
	uvOffset = vec2(uvOffset.x, -uvOffset.y);
	
	amp *= ampliture;
	uvOffset *= ampliture;
	
	out_uvOffset += uvOffset;
	out_amp += amp;
}

void fragment() {
	vec2 screenXY = vec2(SCREEN_UV.x, 1.0 - SCREEN_UV.y) * SCREENSIZE;
	
	vec2 uvOffset = vec2(0.0, 0.0);
	float amp = 0.0;
	calShockFX(screenXY, shock0, uvOffset, amp);
	calShockFX(screenXY, shock1, uvOffset, amp);
	calShockFX(screenXY, shock2, uvOffset, amp);
	calShockFX(screenXY, shock3, uvOffset, amp);
	calShockFX(screenXY, shock4, uvOffset, amp);
	calShockFX(screenXY, shock5, uvOffset, amp);
	calShockFX(screenXY, shock6, uvOffset, amp);
	calShockFX(screenXY, shock7, uvOffset, amp);
	
	vec3 screenColor = textureLod(SCREEN_TEXTURE, SCREEN_UV + uvOffset, 0.0).xyz;
	//float t = (1.0 + cos(TIME * PI)) / 2.0; // [0,1]
	//vec3 finalColor = mix(screenColor, vec3(1.0), vec3(shockAmp));
    //COLOR = vec4(finalColor, 1.0);
	COLOR = vec4(screenColor, 1.0);
	//COLOR = vec4(normalize(uvOffset).xy, 0.0, 1.0);
}