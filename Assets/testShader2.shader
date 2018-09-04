Shader "Unlit/testShader2"
{
	Properties
	{
		[IntRange] _NumColors("Number of colors", Range(2, 4)) = 2
		_Color1("Color 1", Color) = (0,0,0,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		_Color3("Color 3", Color) = (1,0,1,1)
		_Color4("Color 4", Color) = (0,0,1,1)
		_Tiling("Tiling", Range(1, 500)) = 1
		_WidthShift("Width Shift", Range(-1, 1)) = 0
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			int _NumColors;
			fixed4 _Color1;
			fixed4 _Color2;
			fixed4 _Color3;
			fixed4 _Color4;
			int _Tiling;
			float _WidthShift;

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
				o.uv = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float pos = i.uv.y * _Tiling;
				int value = floor((frac(pos)) * _NumColors + _WidthShift);
				value = clamp(value, 0, _NumColors - 1);
				switch (value) {
					case 3: return _Color4;
					case 2: return _Color3;
					case 1: return _Color2;
					default: return _Color1;
				}
			}
			ENDCG
		}
	}
}
