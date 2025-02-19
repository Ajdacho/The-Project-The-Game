Shader "Custom/CaveWall"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _ScanMask("Scan Mask", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _ScanMask;
            sampler2D _BaseColor;
            fixed4 _BaseColor_Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float maskValue = tex2D(_ScanMask, i.uv).r;
                if (maskValue < 0.5)
                {
                    discard;
                }
                return _BaseColor_Color;
            }
            ENDCG
        }
    }
}