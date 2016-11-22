Shader "Skybox/DayNightCycle" {
Properties {
	_Tint ("Tint Color", Color) = (.5, .5, .5, .5)
	[Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
	_Rotation ("Rotation", Range(0, 360)) = 0
	[NoScaleOffset] _Tex ("Cubemap   (HDR)", Cube) = "grey" {}
	[NoScaleOffset] _Tex2("Cubemap   (HDR)", Cube) = "grey" {}
	_TimeOfDay("Time of Day", Range(0, 360)) = 0
}

SubShader {
	Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
	Cull Off ZWrite Off

	Pass {
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0

		#include "UnityCG.cginc"

		samplerCUBE _Tex;
		samplerCUBE _Tex2;
		half4 _Tex_HDR;
		half4 _Tex2_HDR;
		half4 _Tint;
		half _Exposure;
		float _Rotation;
		float _TimeOfDay;

		float3 RotateAroundYInDegrees (float3 vertex, float degrees)
		{
			float alpha = degrees * UNITY_PI / 180.0;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, -sina, sina, cosa);
			return float3(mul(m, vertex.xz), vertex.y).xzy;
		}
		
		struct appdata_t {
			float4 vertex : POSITION;
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			float3 texcoord : TEXCOORD0;
		};

		v2f vert (appdata_t v)
		{
			v2f o;
			float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation + _TimeOfDay);
			o.vertex = UnityObjectToClipPos(rotated);
			o.texcoord = v.vertex.xyz;
			return o;
		}

		fixed4 frag (v2f i) : SV_Target
		{
			half4 tex = texCUBE (_Tex, i.texcoord);
			half4 tex2 = texCUBE(_Tex2, i.texcoord);
			half3 c = DecodeHDR (tex, _Tex_HDR);
			half3 c2 = DecodeHDR(tex2, _Tex_HDR);

			float blend = 1;

			if (_TimeOfDay > 160 && _TimeOfDay <= 180) {
				blend = 1 - (_TimeOfDay - 160) / 20;
			}
			else if (_TimeOfDay > 180 && _TimeOfDay <= 340) {
				blend = 0;
			}
			else if (_TimeOfDay > 340 && _TimeOfDay <= 360) {
				blend = (_TimeOfDay - 340) / 20;
			}

			c = c * blend + c2 * (1 - blend);

			c = c * _Tint.rgb * unity_ColorSpaceDouble.rgb;
			c *= _Exposure;
			return half4(c, 1);
		}
		ENDCG 
	}
} 	


Fallback Off

}
