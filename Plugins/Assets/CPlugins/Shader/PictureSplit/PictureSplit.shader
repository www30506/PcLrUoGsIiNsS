Shader "Unlit/PictureSplit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Value("Value", range(0,0.5)) = 0
//		_Center("Center", vector) = new vector(0.5,0.5)
	}
	SubShader
	{
		Tags {
        "QUEUE"="Transparent" 
        "RenderType"="Opaque" 
        }
		LOD 100

		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Value;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col ;
				col.a = 0;

				if(i.uv.x < 0.5){
					if(i.uv.x < 0.5 - _Value){
						if(i.uv.y < 0.5){
							if(i.uv.y < 0.5 - _Value){
								col = tex2D(_MainTex, float2(i.uv.x + _Value, i.uv.y + _Value));
							}
						}
						else{
							if(i.uv.y > 0.5 + _Value){
								col = tex2D(_MainTex, float2(i.uv.x + _Value, i.uv.y - _Value));
							}
						}
					}
				}
				else{
					if(i.uv.x > 0.5 + _Value){
						if(i.uv.y < 0.5){
							if(i.uv.y < 0.5 - _Value){
								col = tex2D(_MainTex, float2(i.uv.x - _Value, i.uv.y + _Value));
							}
						}
						else{
							if(i.uv.y > 0.5 + _Value){
								col = tex2D(_MainTex, float2(i.uv.x - _Value, i.uv.y - _Value));
							}
						}
					}
				}
				return col;
			}
			ENDCG
		}
	}
}
