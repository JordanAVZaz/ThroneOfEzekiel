Shader "Custom/2DPlaneOutline"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth("Outline Width", Range(0, 1)) = 0.05
        _BaseColor("Base Color", Color) = (1,1,1,1)
    }
    
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
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
            float _OutlineWidth;
            float4 _OutlineColor;
            float4 _BaseColor;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half outlineAlpha = step(0.0 + _OutlineWidth, i.uv.x) * step(0.0 + _OutlineWidth, i.uv.y) * 
                                    step(i.uv.x, 1.0 - _OutlineWidth) * step(i.uv.y, 1.0 - _OutlineWidth);
                                    
                half4 mainColor = tex2D(_MainTex, i.uv) * _BaseColor; // Multiply with base color
                half4 outlineColor = _OutlineColor;
                
                return lerp(outlineColor, mainColor, outlineAlpha);
            }
            ENDCG
        }
    }
}
