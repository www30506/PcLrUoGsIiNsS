Shader "Custom/Dissolve"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_BurnRamp("BurnRamp",2D) = "white"{}
		_DisolveGuide("DisolveGuide", 2D) = "white"{}
		_DisolveValue("Disolve Value", range(0,1)) = 0
		_BurnWidth("BurnWidth", range(0.01,0.139)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};


			sampler2D _MainTex;
			sampler2D _BurnRamp;
			sampler2D _DisolveGuide;
			float4 _MainTex_ST;
			fixed _DisolveValue;
			fixed _BurnWidth;

			v2f vert (appdata  v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 disolveCol = tex2D(_DisolveGuide, i.uv);

				clip(disolveCol.r - _DisolveValue);

				if(disolveCol.r - _DisolveValue > 0 && disolveCol.r - _DisolveValue < _BurnWidth){
					fixed _a = 1-((disolveCol.r - _DisolveValue) * (1/_BurnWidth));
					fixed2 BurnRamp_uv = fixed2(_a, 0.5);
					fixed4 col2 = tex2D(_BurnRamp, BurnRamp_uv);
					col += col2 * _a;
				}
				return col;
			}
			ENDCG
		}
	}
}
