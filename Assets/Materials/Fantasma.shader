Shader "Custom/Fantasma"
{
    Properties
    {
        _Color ("Color", Color) = (0.7,0.7,0.7,0.3)
        _Noise ("Noise", 2D) = "gray" {}
        _NoiseStrength ("Noise Strength", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _Noise;
            float4 _Color;
            float _NoiseStrength;

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
                o.uv = v.uv; // UV FIJO, SIN TIEMPO
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float noise = tex2D(_Noise, i.uv).r;
                float alpha = lerp(_Color.a, _Color.a * noise, _NoiseStrength);
                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}
