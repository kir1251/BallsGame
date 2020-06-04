Shader "Unlit/BallShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[PerRendererData] _RendererColor("RendererColor", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM			
		#pragma surface surf Standard vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
		#pragma multi_compile _ PIXELSNAP_ON
		#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
		#include "UnitySprites.cginc"

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
			float4 screenPos;
			//float4 centerScreenPos;
		};

		void vert(inout appdata_full v, out Input o)
		{
			v.vertex = UnityFlipSprite(v.vertex, _Flip);

			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap(v.vertex);
			#endif

			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _RendererColor;
			//o.centerScreenPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
			
		}

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			
			float4 pos = IN.screenPos;
			// sample the texture
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex);

			float uvd = length(IN.uv_MainTex - 0.5f) * 2;

			o.Emission = col * _RendererColor * IN.color;
			o.Alpha = pow(uvd, 2) * (pow(saturate(pos.y / pos.w), 2.0f) + 0.2f);
		}

		ENDCG
		
	}
}
