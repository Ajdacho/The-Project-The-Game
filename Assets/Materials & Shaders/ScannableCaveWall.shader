Shader "Custom/ScannableCaveWall"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _Color("Base Color", Color) = (0, 0, 0, 1)
        _ScanCenter("Scan Center", Vector) = (0, 0, 0, 0)
        _ScanRadius("Scan Radius", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 200

        Pass
        {
            Name "MainPass"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            float4 _Color;
            float4 _ScanCenter;
            float _ScanRadius;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.worldPos = TransformObjectToWorld(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float4 baseTexture = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
                float dist = distance(i.worldPos, _ScanCenter.xyz);
                float alpha = smoothstep(_ScanRadius, _ScanRadius - 0.5, dist);

                float visibility = 1.0 - alpha;

                return lerp(_Color, baseTexture, alpha) * visibility;
            }
            ENDHLSL
        }
    }
}
