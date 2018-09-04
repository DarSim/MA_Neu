Shader "Unlit/WorldspaceSwitch"
{
	Properties
	{
		_Color("Surface Color", Color) = (1, 1, 1, 1)
		_UnderColor("Underground Color", Color) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "DisableBatching" = "true" }
		LOD 100

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
				float3 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			// Declare the variables to hold the colours
			// we want to show above & below the line.
			fixed4 _Color;
			fixed4 _UnderColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);

				// I added a third texture coordinate,
				// and populated it with the worldspace y position.
				o.uv.z = mul(unity_ObjectToWorld, v.vertex).y;
				//o.uv.z = v.vertex.y;
				//o.uv.z = v.uv.y;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// This turns the worldspace y position
				// into a tight blending ramp from 0 to 1.
				float blend = saturate(frac(i.uv.z)*10.0f);

				// This blends between the two colours.
				fixed4 col = lerp(_UnderColor, _Color, blend);

				return col;
			}
			ENDCG
		}
	}
}