﻿Shader "Explorer/PookieVision"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Area("Area", vector) = (0, 0, 4, 4)
			_MaxIter("Iterations", range(4, 1000)) = 255
			_Angle("Angle", range(-3.1415, 3.1415)) = 0
			_Color("Color", range(0, 1)) = 0.5
			_Repeat("Repeat", range(0, 20)) = 10
			_Speed("Speed", range(-1, 1)) = 0
			_Symmetry("Symmetry", range(0, 1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float4 _Area;
			float _Angle, _MaxIter, _Color, _Repeat, _Speed, _Symmetry;
            sampler2D _MainTex;

			float2 rot(float2 p, float2 pivot, float a) {
				float s = sin(a);
				float c = cos(a);

				p -= pivot;
				p = float2(p.x * c - p.y * s, p.x * s + p.y * c);
				p += pivot;

				return p;
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv - 0.5;

				// ALT4 -- Split into sections
				uv = abs(uv);							// 8 folds / slider
				uv = rot(uv, 0, 0.25 * 3.1415);			// 8 folds / slider
				uv = abs(uv);							// 8 folds / slider
				uv = lerp(i.uv - 0.5, uv, _Symmetry);	// 8 folds / slider
				//uv = abs(uv);							// 8 folds
				//uv = rot(uv, 0, 0.25 * 3.1415);		// 8 folds
				//uv = abs(uv);							// 8 folds
				//uv = abs(uv);							// 4 folds
				//uv.x = abs(uv.x);						// 2 folds w/ vertical split
				//uv.y = abs(uv.y);						// 2 folds w/ horizontal split
				uv = uv;								// 0 folds

				float2 c = _Area.xy + uv * _Area.zw;
				c = rot(c, _Area.xy, _Angle);

				float r = 20; // escape radius
				float r2 = r * r;

				// ALT1 -- Add 'leaves'
				//float2 z;				// no leaves
				float2 z, zPrevious;	// leaves

				float iter;
				for (iter = 0; iter < _MaxIter; iter++) {
					// ALT1 -- Add 'leaves'
					// no zPrevious						// no leaves			
					// ALT3 -- Gives a waving motion
					//zPrevious = z;						// leaves	// no waves	
					zPrevious = rot(z, 0, _Time.y);			// leaves	// waves	

					z = float2(z.x * z.x - z.y * z.y, 2 * z.x * z.y) + c;

					// ALT1 -- Add 'leaves'
					//if (dot(z, z) > r2) break;			// no leaves
					if (dot(z, zPrevious) > r2) break;		// leaves
				}

				if (iter >= _MaxIter) return 0; 

				float dist = length(z); // distance from origin
				float fracIter = (dist - r) / (r2 - r); // linear interpolation
				fracIter = log2( log(dist) / log(r) ); // double exponential interpolation

				// ALT1 -- Add 'leaves'
				//iter -= fracIter;		// no leaves
				// 						// leaves

				float m = sqrt(iter / _MaxIter);
				float4 col = sin(float4(0.3, 0.45, 0.65, 1) * m * 20) * 0.5 + 0.5; // procedural colors
				col = tex2D(_MainTex, float2(m * _Repeat + _Time.y * _Speed, _Color));

				// ALT1 -- Add 'leaves'
				// ALT2 -- Gives 'leaves' striations
				//float angle = 0;								// leaves	// no striations
				float angle = atan2(z.x, z.y); // -pi & pi		// leaves	// striations
				col *= smoothstep(3, 0, fracIter);				// leaves

				// ALT3 -- Gives a waving motion
				//col *= 1 + sin(angle * 2) * 0.2;					// leaves	// striations	// no waves	
				col *= 1 + sin(angle * 2 + _Time.y * 4) * 0.2;		// leaves	// striations	// waves	
				return col;
            }
            ENDCG
        }
    }
}
