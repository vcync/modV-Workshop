// License: MIT 2021 by NERDDISCO (Tim Pietrusky)
// Based on the episode of "curiouslyminded - S01E13 with Sean Zellmer"

/*{
	"DESCRIPTION": "nd-torus",
	"CREDIT": "NERDDISCO",
	"ISFVSN": "2",
	"CATEGORIES": [
		"Torus", 
		"Iridescence"
	],
	"INPUTS": [
		{
			"NAME": "inputImage",
			"TYPE": "image"
		},
		{
			"NAME": "radius_x",
			"TYPE": "float",
			"DEFAULT": 1.5,
            "MIN": 0.0,
            "MAX": 10.0
		},
		{
			"NAME": "radius_y",
			"TYPE": "float",
			"DEFAULT": 0.75,
            "MIN": 0.0,
            "MAX": 10.0
		},
		{
			"NAME": "rotation",
			"TYPE": "float",
			"DEFAULT": 0.0,
            "MIN": -3.0,
            "MAX": 3.0
		},
		{
			"NAME": "rotation_auto",
			"TYPE": "bool",
			"DEFAULT": 0.0
		},
        {
            "NAME": "brightness",
			"TYPE": "float",
			"DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 5.0
        },
        {
            "NAME": "contrast",
			"TYPE": "float",
			"DEFAULT": 0.6,
            "MIN": 0.0,
            "MAX": 5.0
        },
        {
            "NAME": "oscillate",
			"TYPE": "float",
			"DEFAULT": 1.5,
            "MIN": 0.0,
            "MAX": 10.0
        },
        {
            "NAME": "light",
			"TYPE": "float",
			"DEFAULT": 1.14,
            "MIN": 0.0,
            "MAX": 5.0
        },
        {
			"NAME": "warpScale",
			"TYPE": "float",
			"DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 50.0
		},
        {
			"NAME": "warp1_intensity",
			"TYPE": "float",
			"DEFAULT": 3.0,
            "MIN": 0.0,
            "MAX": 50.0
		},
        {
			"NAME": "warp2_intensity",
			"TYPE": "float",
			"DEFAULT": 6.0,
            "MIN": 0.0,
            "MAX": 50.0
		},
        {
			"NAME": "warp3_intensity",
			"TYPE": "float",
			"DEFAULT": 9.0,
            "MIN": 0.0,
            "MAX": 50.0
		},
		{
			"NAME": "surfaceDistortion",
			"TYPE": "float",
			"DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 10.0
		},
		{
			"NAME": "surfaceDistortion_auto",
			"TYPE": "bool",
			"DEFAULT": 0.0
		}
	]
}*/

#define EPSILON 0.0001
#define RAYMARCH_MAX_STEPS 400
#define RAYMARCH_MAX_DIST 10.

// https://iquilezles.org/
vec3 palette(in vec3 t, in vec3 a, in vec3 b, in vec3 c, in vec3 d) { 
    return a + b * cos(6.28318 * (c * t + d)); 
}

// akella - twitter.com/akella
mat4 rotationMatrix(vec3 axis, float angle) {
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    return mat4(oc * axis.x * axis.x + c, 
				oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           
				oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           
				0.0,
                0.0,                                
				0.0,                                
				0.0,                                
				1.0);
}

vec3 rotate(vec3 v, vec3 axis, float angle) {
	mat4 m = rotationMatrix(axis, angle);
	return (m * vec4(v, 1.0)).xyz;
}

// Torus function by Inigo Quilez - iquilezles.org
// p - position, t - size
float sdTorus(vec3 p, vec2 t) {
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	return length(q)-t.y;
}

float scene(vec3 pos) {
    float _rotation = rotation * 2.;

    if (rotation_auto) {
        _rotation += TIME;
    }

	vec3 rotation = rotate(vec3(pos.x, pos.y, pos.z + 1.5), vec3(1.), _rotation);	
	vec3 wPos = rotation; // warp position
    float _surfaceDistortion = surfaceDistortion;

    if (surfaceDistortion_auto) {
        _surfaceDistortion += TIME;
    }

	// Cosine domain warp
	wPos += warpScale * 0.10000 * cos(warp1_intensity * wPos.yzx + _surfaceDistortion);
	wPos += warpScale * 0.05000 * cos(warp2_intensity * wPos.yzx + surfaceDistortion);
	wPos += warpScale * 0.02500 * cos(warp3_intensity * wPos.yzx + surfaceDistortion);
	
	rotation = wPos.xzy;
	
	float torus = sdTorus(rotation, vec2(radius_x, radius_y));
	
	return torus;
}

// housing for raymarch fn return val	
vec4 shade (vec3 pos, vec3 rayDir, float rayDepth);

vec4 raymarch(vec3 rayDir, vec3 pos) {
	float currentDist = 0.0; 
	float rayDepth = 0.0;
	vec3 rayLength = vec3(0.0);
	
	// shooting the ray
 	for (int i = 0; i < RAYMARCH_MAX_STEPS; i++) {
     	// steps traveled
		currentDist = scene(pos + rayDir * rayDepth);
		rayDepth += currentDist;
		
		// We're inside the scene - magic happens here
 	    if (currentDist < EPSILON) return shade(pos + rayDir * rayDepth, rayDir, rayDepth);
		
		// We've gone too far
 		if (rayDepth > RAYMARCH_MAX_DIST) return vec4(0, 0, 0, 1.0); 
	}
	
	return vec4(0, 0, 0, 1.);
}

vec3 getNormal (in vec3 pos, in float depth) {
	const vec2 epsilon = vec2(0.0001, 0.);
	vec3 nor = vec3(
		scene(pos + epsilon.rgg) - scene(pos - epsilon.rgg), // x
		scene(pos + epsilon.grg) - scene(pos - epsilon.grg), // y
		scene(pos + epsilon.ggr) - scene(pos - epsilon.ggr) // z
	);
	return normalize(nor);
}
	
float diffuse (in vec3 light, in vec3 nor) {
	return clamp(0., 1., dot(nor, light));
}
	
vec3 baseColor (in vec3 pos, in vec3 nor, in vec3 rayDir, in float rayDepth) {
	vec3 color = vec3(0);
	
	float dNR = dot(nor, -rayDir);
	
	color = palette(
		vec3(dNR),
		vec3(brightness),
		vec3(contrast),
		vec3(oscillate),
		vec3(.03, .33, .66) // phase
	);

	// vec4 prevColor = IMG_NORM_PIXEL(inputImage, isf_FragNormCoord);

	// if (color.r + color.g + color.b < 1.45) {
	// 	color /= prevColor.rgb;
	// }

	return color;
}
	
vec4 shade (vec3 pos, vec3 rayDir, float rayDepth) {
	vec3 nor = getNormal(pos, rayDepth);
	
	nor += 0.1 * sin(13. * nor + surfaceDistortion);
	nor = normalize(nor);
	
	vec3 lightPos = rotate(vec3(1.), vec3(0, 1, 0), light);
	
	float dif = diffuse(lightPos, nor);
	vec3 color = dif * baseColor(pos, nor, rayDir, rayDepth);
	vec4 shapeColor = vec4(color, 1.0);

	return shapeColor;
}

void main()	{
    vec4 color;

    vec2 uv = (gl_FragCoord.xy - RENDERSIZE.xy * .5) / RENDERSIZE.yy; 
	vec3 camPos = vec3(0.0, 0.0, 5.0);	
	vec3 rayDir = normalize(vec3(uv, -1.0));

    color = vec4(raymarch(rayDir, camPos));

	// if (color.a < 1.0) {
	// 	color = IMG_NORM_PIXEL(inputImage, isf_FragNormCoord);
	// 	color.a = 1.0;
	// }
	
    gl_FragColor = color;
}
