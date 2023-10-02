// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader"MyShader/Transparent character occlusion"
{
    Properties
    {
        //������
        _MainTex("Main_Tex",2D)="white"{}
        _Tex_Color("Tex_Color",Color) = (1,1,1,1)
        _Diffuse("Diffuse",Color) = (1,1,1,1)
        //�߹���ɫ
        _Specular("Specular",Color) = (1,1,1,1)
        //�߹�ϵ��
        _Gloss("Gloss",Range(1.0,256))=20
       

        //�ڵ��������
        _OcclusionTex("_OcclusionTex",2D)="white"{}
        //�ڵ������ɫ
        _OcclusionTex_Color("_OcclusionTex_Color",Color) = (1,1,1,1)
        _Cutoff("_Cutoff",Range(0,1.0))=0.5
        
    }
    SubShader
    {
        //ָ��SubShader��LOD�ȼ�����������ߵ�100������ʾ��SubShader���κ�����¶�Ӧ�ñ���Ⱦ��
        
        Pass
		{
            Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "LightMode" = "ForwardBase" "Rendering Mode" ="TransparentCutout" }
			//Tags { "Queue" = "AlphaTest" "IgnoreProjector"="True"  "RenderType" = "TransparentCutout" }
			LOD 200
			ZTest Greater
			ZWrite Off
 
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma target 3.0
			#include "UnityCG.cginc"
			
			fixed4 _OcclusionTex_Color;
            sampler2D _OcclusionTex;
            //_MainTex�����ź�ƽ��  xyΪ���ţ�zwΪƽ��
            float4 _OcclusionTex_ST;
            fixed _Cutoff;

			struct a2v
			{
                
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;
                float4 texcoord:TEXCOORD0;
			};
            struct v2f {

                // �����ڲü��ռ��λ����Ϣ
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD2;
            };
			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)
 
			
			v2f vert(a2v v)  {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v)
                o.pos=UnityObjectToClipPos(v.vertex);
                o.uv=TRANSFORM_TEX(v.texcoord,_OcclusionTex); 
				return o;
			}
			fixed4 frag(v2f i):SV_Target{

                // Alpha test
                
				// Equal to 
//				if ((texColor.a - _Cutoff) < 0.0) {
//					discard;
//				}
                fixed4 texColor = tex2D(_OcclusionTex, i.uv);
                clip(texColor.a - _Cutoff);
                fixed3 albedo = texColor.rgb * _OcclusionTex_Color.rgb;
                
    
                return fixed4(albedo, 1.0);
}
			ENDCG
		}
 
        
        Pass
        {
            LOD 100
            //����ָ����LightMode��ǩΪForwardBase����ʾ��Pass����뵽������Ⱦ�У����ڴ���������ա�
            Tags { "LightMode" = "ForwardBase" }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
 #pragma multi_compile_shadowcaster
            fixed4 _Diffuse;
            fixed4 _Specular;
            float _Gloss;

            fixed4 _Tex_Color;
            sampler2D _MainTex;
            //_MainTex�����ź�ƽ��  xyΪ���ţ�zwΪƽ��
            float4 _MainTex_ST;
            
            //����ṹ��
            struct a2v {

                // ģ�Ϳռ䶥������
                float4 vertex:POSITION;
                // ģ�Ϳռ䷨�߷���
                float3 normal:NORMAL;
                float4 texcoord:TEXCOORD0;
 
            };
            struct v2f {

    
                // �����ڲü��ռ��λ����Ϣ
                float4 pos:SV_POSITION;
                float3 worldNormal:TEXCOORD4;
                float3 worldPos:TEXCOORD5;
                float2 uv:TEXCOORD6;
    //V2F_SHADOW_CASTER;
};

            v2f vert(a2v v) {
                v2f o;
    //TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
                //�����λ�ôӶ���ռ�ת�������ÿռ�
                o.pos = UnityObjectToClipPos(v.vertex);
                //�����㷨�ߴ�ģ�Ϳռ�ת��������ռ䣬����һ����
                o.worldNormal =normalize(mul(v.normal,(float3x3)unity_WorldToObject));
                //����ռ䶥������
                o.worldPos = normalize(mul(unity_ObjectToWorld,v.vertex)).xyz;

                //o.uv=TRANSFORM_TEX(v.texcoord,_MainTex); ���·�������ͬ
                o.uv=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw;
                
                return o;
            }
            fixed4 frag(v2f i) : SV_Target{
   // SHADOW_CASTER_FRAGMENT(i);
               //��ȡ������ɫ
               fixed3 albedo=tex2D(_MainTex,i.uv).rgb*_Tex_Color.rgb; 
               //��������ɫ��ӵ���������
               fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*   albedo;

               fixed3 worldNormal=normalize(i.worldNormal);
               //������������λ�ö���λ�ã����شӸõ㵽��Դ�ķ�������
               fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
               float halfLambert = dot(worldNormal,worldLightDir)*0.5+0.5;

               //��������ɫ��ӵ���������
               fixed3 diffuse= _LightColor0.rgb * _Diffuse.rgb*halfLambert   *albedo;
               //diffuse= _LightColor0.rgb * _Diffuse.rgb*saturate(dot(worldNormal,worldLightDir));
               //������������λ�ö���λ�ã����شӸõ㵽������ķ�������
               fixed3 viewDir=normalize(UnityWorldSpaceViewDir(i.worldPos));
               fixed3 halfDir=normalize(worldLightDir+viewDir);
               fixed3 specular =_LightColor0.rgb*_Specular.rgb*(pow(saturate(dot(worldNormal,halfDir)),_Gloss));

              

               fixed3 c = ambient+diffuse+specular;
               return fixed4(c,1.0);
            }

            ENDCG
        }


    }
    FallBack "SPECULAR"
}