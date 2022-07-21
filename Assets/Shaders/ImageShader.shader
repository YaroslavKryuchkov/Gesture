Shader "Unlit/ImageShader"
{
    Properties
    {
	_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 
              "Queue" = "Overlay"
              "RenderType"="Overlay"
             }
	
	GrabPass{ "_BackgroundTexture" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always
            ZWrite Off
            Cull Off

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
            
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BackgroundTexture;
            float4 _BackgroundTexture_ST;

            v2f vert (appdata v)
            {
                v2f o;
		o.vertex = float4(v.uv * 2 - 1, 0, 1);
		#ifdef UNITY_UV_STARTS_AT_TOP
			v.uv.y = 1-v.uv.y;
		#endif
		o.uv.xy = UnityStereoTransformScreenSpaceTex(v.uv);
		return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(tex2D(_BackgroundTexture, i.uv.xy), tex2D(_MainTex, i.uv.xy), 0.75);
                return col;
            }
            ENDCG
        }
    }
}
