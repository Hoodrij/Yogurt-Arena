Shader "Custom/VerticalFogIntersection"
{
    Properties
    {
       _MyTexture("My Texture", 2D) = "white" { }
       _Color("Main Color", Color) = (1, 1, 1, .5)
       _IntersectionThresholdMax("Intersection Threshold Max", float) = 1
       _Density("Density", float) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent"  }
  
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata
            {
               float4 vertex : POSITION;
            };

            struct v2f
            {
               float4 uv : TEXCOORD0;
               UNITY_FOG_COORDS(1)
               float4 vertex : SV_POSITION;
            };

            sampler2D _CameraDepthTexture;
            sampler2D _MyTexture;
            float4 _Color;
            float4 _IntersectionColor;
            float _IntersectionThresholdMax;
            float _Density;
  
            v2f vert(appdata v)
            {
               v2f o;
               o.vertex = UnityObjectToClipPos(v.vertex);
               o.uv = ComputeScreenPos(o.vertex);
               UNITY_TRANSFER_FOG(o,o.vertex);
               return o;   
            }
  
            half4 frag(v2f i) : SV_TARGET
            {
               float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.uv)));
               float diff = saturate(_IntersectionThresholdMax * (depth - i.uv.w));
  
               fixed4 col = lerp(fixed4(_Color.rgb, 0.0), _Color, pow(diff, _Density));
               // fixed4 col = lerp(fixed4(_Color.rgb, 0.0), _Color, diff * diff * diff * (diff * (10 * diff - 15) + 10));
               // UNITY_APPLY_FOG(i.fogCoord, col);

               col *= tex2D(_MyTexture, i.uv / 200);
               return col;
            }
  
            ENDCG
        }
    }
}