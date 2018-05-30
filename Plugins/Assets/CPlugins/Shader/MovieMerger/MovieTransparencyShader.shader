Shader "Custom/MovieTransparency" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        
        Tags {
        "QUEUE"="Transparent" 
        "RenderType"="Opaque" 
        }
        LOD 200

      Cull Off
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha

      Pass{
	      CGPROGRAM
	      #pragma vertex vert
	      #pragma fragment frag
	      #include "UnityCG.cginc"

	      uniform sampler2D _MainTex;

	      struct appdata{
		      float4 vertex:POSITION;
		      float4 uv:TEXCOORD0;
	      };

	      struct V2F{
		      float4 pos:SV_POSITION;
		      float4 uv:TEXCOORD0;
	      };


	      V2F vert(appdata i){
		      V2F o;
		      o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
		      o.uv = i.uv;

		      return o;
	      }

	      float4 frag( V2F i ):COLOR{
		      float4 col;
		      col.rgb = tex2D(_MainTex, float2(i.uv.x, i.uv.y*0.5+0.5));
		      col.a = tex2D(_MainTex, float2(i.uv.x, i.uv.y*0.5));
		      return col;
	      }

	      ENDCG
      }
    }
}