Shader "Cg Metaballs" { // defines the name of the shader
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			CGPROGRAM
			// A strong independent shader don't need no flash
			#pragma exclude_renderers ps3 xbox360 flash
			#pragma vertex vert 
			#pragma fragment frag

			#include "UnityCG.cginc"

			// -----------------------------
			// Uniforms
			// -----------------------------
			uniform float4 _color1;
			uniform float4 _color2;
			uniform float4 _color3;
			uniform float4 _color4;
			
			uniform float4 _xPositions;
			uniform float4 _yPositions;
			
			// -----------------------------
			// Structs, sent between shaders
			// -----------------------------
			struct vertexInput
			{
				float4 vertex : POSITION;
			};
			
			struct fragmentInput
			{
				float4 pos : SV_POSITION;
				float2 screenPos : TEXCOORD0;
			};
			
			// -----------------------------
			// Vertex shader
			// -----------------------------
			fragmentInput vert(vertexInput v)
			{
				fragmentInput f;
				f.pos = mul(UNITY_MATRIX_MVP, v.vertex); 
				f.screenPos = ComputeScreenPos(f.pos);
				
				return f;
			}
			
			// -----------------------------
			// Fragment shader
			// -----------------------------
			float4 frag( fragmentInput f) : COLOR // fragment shader
			{
				float gooness = 2.0, size = 0.0004, threshold = 0.1;
				float4 finalColor = float4(0.0,0.0,0.0,0.0);
				float4 colors[4];
				float distances[4];
				float metas[4];
				float totMeta = 0;
				float aspect = _ScreenParams.x/_ScreenParams.y;
				
				colors[0] = _color1;
				colors[1] = _color2;
				colors[2] = _color3;
				colors[3] = _color4;
				
				for (int i = 0; i < 4; i++) {
					distances[i] = distance(float2(_xPositions[i] * aspect,_yPositions[i]), float2(f.screenPos[0] * aspect, f.screenPos[1]));
					metas[i] = size/(pow(distances[i],gooness));
					totMeta += metas[i];
					
					finalColor = metas[i] > threshold ? colors[i] : finalColor;
				}
				
				finalColor = totMeta > threshold * 4 ? finalColor : float4(0.0,0.0,0.0,0.0);

				// Need to add nice blending
				
				return finalColor;
			}

			ENDCG
		}
   }
}