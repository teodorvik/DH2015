Shader "Cg Metaballs" { // defines the name of the shader

 	properties {
 	
 	} 
 	
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
			uniform float4 _positions;
			
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
				float2 screenPos;
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
				// Hax fix
				float aspect = _ScreenParams.x/_ScreenParams.y;
				float aspect2 = _ScreenParams.y/_ScreenParams.x;
				_positions[0] *= aspect + 0.5;
				_positions[2] *= aspect + 0.5;
				f.screenPos.x *= aspect + 0.5;
				f.screenPos.y *= aspect2 + 0.5;
				_positions[1] *= aspect2 + 0.5;
				_positions[3] *= aspect2 + 0.5;
			
				float gooness = 3.0, size = 0.0003, threshold = 0.2;
				float4 finalColor;
				
				float dist1 = distance(float2(_positions[0],_positions[1]), float2(f.screenPos[0], f.screenPos[1]));
				float meta 	= size/(pow(dist1,gooness));
				
				float dist2 = distance(float2(_positions[2],_positions[3]), float2(f.screenPos[0], f.screenPos[1]));
				float meta2 = size/(pow(dist2,gooness));
				
				finalColor = meta > threshold/2 ? _color1 : float4(0.0,0.0,0.0,0.0);
				finalColor = meta2 > threshold/2 ? _color2 : finalColor;
				finalColor = meta + meta2 > threshold ? finalColor : float4(0.0,0.0,0.0,0.0);
				
				// Normalize
				float temp = dist1;
				dist1 = (dist1 - min(dist1,dist2)) / (max(dist1,dist2) - min(dist1,dist2));
				dist2 = (dist2 - min(temp,dist2)) / (max(temp,dist2) - min(temp,dist2));
				
				finalColor = meta > threshold/2 && meta2 > threshold/2 ? dist1 * _color2 + dist2* _color1 : finalColor;
				
				return finalColor;
			}

			ENDCG
		}
   }
}