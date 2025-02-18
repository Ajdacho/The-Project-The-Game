<<<<<<< HEAD
Shader "MultiScanWall"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _Color("Base Color", Color) = (0, 0, 0, 1)
        _ScanCenters("Scan Centers", Vector) = (0, 0, 0, 0)
        _ScanRadii("Scan Radii", Float) = 0.0
        _ScanCount("Scan Count", Float) = 0
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

            #define MAX_SCANS 10

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
            float4 _ScanCenters[MAX_SCANS];
            float _ScanRadii[MAX_SCANS];
            int _ScanCount;

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

                float visibility = 0.0;

                for (int j = 0; j < _ScanCount; j++)
                {
                    float dist = distance(i.worldPos, _ScanCenters[j].xyz);
                    visibility = max(visibility, smoothstep(_ScanRadii[j], _ScanRadii[j] - 0.5, dist));
                }

                return lerp(_Color, baseTexture, visibility);
            }
            ENDHLSL
        }
    }
}
=======
Shader "MultiScanWall"
{
    Properties
    {
        _BaseMap("Base Map", 2D) = "white" {}
        _Color("Base Color", Color) = (0, 0, 0, 1)
        _ScanCenters("Scan Centers", Vector) = (0, 0, 0, 0)
        _ScanRadii("Scan Radii", Float) = 0.0
        _ScanCount("Scan Count", Float) = 0
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

            #define MAX_SCANS 10

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
            float4 _ScanCenters[MAX_SCANS];
            float _ScanRadii[MAX_SCANS];
            int _ScanCount;

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

                float visibility = 0.0;

                for (int j = 0; j < _ScanCount; j++)
                {
                    float dist = distance(i.worldPos, _ScanCenters[j].xyz);
                    visibility = max(visibility, smoothstep(_ScanRadii[j], _ScanRadii[j] - 0.5, dist));
                }

                return lerp(_Color, baseTexture, visibility);
            }
            ENDHLSL
        }
    }
}
>>>>>>> 03e551f (Dodanie plików audio / efektów dźwiękowych / ambientu w tle)
