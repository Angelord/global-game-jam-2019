// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Camera/Hue"
{
    Properties
    {
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
        _Hue("Hue", Range(0, 1)) = 1.0
        _Brightness("Brightness", Range(0, 1)) = 0.5
        _Saturation("Saturation", Range(0, 1)) = 0.5
        _Contrast("Contrast", Range(0, 1)) = 0.5
    }
 
    SubShader
    {
        LOD 100
 
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
     
        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Offset -1, -1
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
             
            #include "CGIncludes/UnityCG.cginc"
            #include "CGIncludes/HSB.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };
 
            struct v2f
            {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };
 
            sampler2D _MainTex;
            float _Hue;
            float _Brightness;
            float _Saturation;
            float _Contrast;
         
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }
             
            fixed4 frag (v2f i) : COLOR
            {
                float4 startColor = tex2D(_MainTex, i.texcoord);
                float4 hsbColor = applyHSBEffect(startColor, _Hue, _Brightness, _Saturation, _Contrast);
                return hsbColor;
            }
            ENDCG
        }
    }
 
    SubShader
    {
        LOD 100
 
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
     
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Offset -1, -1
            ColorMask RGB
            //AlphaTest Greater .01
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMaterial AmbientAndDiffuse
         
            SetTexture [_MainTex]
            {
                Combine Texture * Primary
            }
        }
    }
}