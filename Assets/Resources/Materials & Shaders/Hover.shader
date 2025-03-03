Shader "Custom/HoverScanEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (0, 1, 1, 1)
        _Transparency ("Transparency", Range(0,1)) = 0.5
        _Hover ("Hover Effect", Range(0,1)) = 0
        _LineSpeed ("Line Speed", Float) = 1
        _LineFrequency ("Line Frequency", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _LineColor;
            float _Transparency;
            float _Hover;
            float _LineSpeed;
            float _LineFrequency;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float timeFactor = _Time.y * _LineSpeed;
                
                // Dynamiczne kolorowe linie (gradient oparty na wspó³rzêdnych œwiata)
                float scanLine = sin(i.worldPos.y * _LineFrequency + timeFactor) * 0.5 + 0.5;
                float lineEffect = smoothstep(0.4, 0.6, scanLine) * _Hover;

                // Efekt Fresnela (rozœwietlone krawêdzie)
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, i.normal)), 2.0);
                float fresnelEffect = smoothstep(0.2, 1.0, fresnel) * _Hover;

                // £¹czenie efektów
                float transparency = lerp(_Transparency, 1.0, _Hover);
                float4 baseColor = tex2D(_MainTex, i.uv);
                float4 finalColor = lerp(baseColor, _LineColor, lineEffect + fresnelEffect);

                // Przezroczystoœæ
                finalColor.a *= transparency;
                return finalColor;
            }
            ENDCG
        }
    }
}
