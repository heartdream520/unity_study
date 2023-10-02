Shader "LSQ/Screen Effect/XRay"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            sampler2D _XRayDepthTexture;
            float4x4 _ViewToWorld;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float depth = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                float xrayDepth = Linear01Depth(tex2D(_XRayDepthTexture, i.uv).r);

                if(xrayDepth > 0 && xrayDepth < 1 && xrayDepth > depth + 0.01)
                {
                    //return fixed4(1,0,0,1);

                    // view depth
                    float z = depth * _ProjectionParams.z;
                    // convert to world space position
                    float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
                    float3 viewPos = float3((i.uv * 2 - 1) / p11_22, -1) * z;
                    float3 worldPos = mul(_ViewToWorld, float4(viewPos, 1)).xyz;
                    return abs(round(worldPos.y * 5 + _Time.y * 5)) % 2 * fixed4(1,0,0,1);

                    //Fresnel ? - Normal
                    
                }

                return col;
            }
            ENDCG
        }
    }
}
