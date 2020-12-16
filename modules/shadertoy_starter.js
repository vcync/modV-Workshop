export default {
    meta: {
      type: 'isf',
      name: 'Get started with ISF using the default shadertoy shader',
      author: 'NERDDISCO'
    },
  
    fragmentShader: `
      /*{
        "DESCRIPTION": "Get started with ISF using the default shadertoy shader",
        "CREDIT": "shadertoy for the code & NERDDISCO for converting it to ISF",
        "ISFVSN": "2",
        "CATEGORIES": [
          "gradient"
        ],
        "INPUTS": [
          {
            "NAME": "alpha",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
          }
        ]
      }*/
  
      // We can't use
      // void mainImage( out vec4 fragColor, in vec2 fragCoord )
      // as we would do in shadertoy, but use main() instead
      void main()	{
        // ISF is using RENDERSIZE instead of iResolution
        vec2 uv = gl_FragCoord.xy/RENDERSIZE.xy;
  
        // ISF is using TIME instead of iTime
        float iTime = TIME;
  
        // The following line is the default code for a shader
        // created in shadertoy
        vec3 col = 0.5 + 0.5*cos(iTime+uv.xyx+vec3(0,2,4));
  
        gl_FragColor = vec4(col, alpha);
      }
    `,
  };