Shader "Custom/Character"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OcclusionColor("OcclusionColor",Color) = (1,0.5,0,1)
    }
    SubShader
    {
		
		Pass
		{
			Tags { "Queue" = "Geometry+50" "RenderType" = "Opaque" }
			LOD 200
			ZTest Greater
			ZWrite Off
 
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma target 3.0
			#include "UnityCG.cginc"
			
			
			struct appdata
			{
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;
			};
 
			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)
 
			fixed4 _OcclusionColor;
			float4 vert(appdata v) : SV_POSITION {
				UNITY_SETUP_INSTANCE_ID(v)
				return UnityObjectToClipPos(v.vertex);
			}
			fixed4 frag() : SV_Target{

				return _OcclusionColor;
			}
			ENDCG
		}
 
		Pass{
			//...Õý³£äÖÈ¾µÄshader
		}
	}
}