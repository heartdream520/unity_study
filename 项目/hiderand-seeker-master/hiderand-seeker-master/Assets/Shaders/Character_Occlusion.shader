// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "MyShader/不透明的人物遮挡"
{
    Properties
    {
        //主纹理
        _MainTex("Main_Tex",2D)="white"{}
        _Tex_Color("Tex_Color",Color) = (1,1,1,1)
        _Diffuse("Diffuse",Color) = (1,1,1,1)
        //高光颜色
        _Specular("Specular",Color) = (1,1,1,1)
        //高光系数
        _Gloss("Gloss",Range(1.0,256))=20
       

        //遮挡后的纹理
        _OcclusionTex("_OcclusionTex",2D)="white"{}
        //遮挡后的颜色
        _OcclusionTex_Color("_OcclusionTex_Color",Color) = (1,1,1,1)

    }
    SubShader
    {
        //指定SubShader的LOD等级，这里是最高的100级，表示该SubShader在任何情况下都应该被渲染。
        
        Pass
		{
			Tags { "Queue" = "Geometry+110" "RenderType" = "Opaque" }
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
            //_MainTex的缩放和平移  xy为缩放，zw为平移
            float4 _OcclusionTex_ST;


			struct a2v
			{
                
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;
                float4 texcoord:TEXCOORD0;
			};
            struct v2f {

                // 顶点在裁剪空间的位置信息
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

                fixed3 albedo=tex2D(_OcclusionTex,i.uv).rgb*_OcclusionTex_Color.rgb;
				return fixed4(albedo,1.0) ;
			}
			ENDCG
		}
 
        
        Pass
        {
            LOD 100
            //这里指定了LightMode标签为ForwardBase，表示该Pass会参与到正向渲染中，用于处理基础光照。
            Tags { "LightMode" = "ForwardBase" }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            fixed4 _Diffuse;
            fixed4 _Specular;
            float _Gloss;


            fixed4 _Tex_Color;
            sampler2D _MainTex;
            //_MainTex的缩放和平移  xy为缩放，zw为平移
            float4 _MainTex_ST;
            
            //定义结构体
            struct a2v {

                // 模型空间顶点坐标
                float4 vertex:POSITION;
                // 模型空间法线方向
                float3 normal:NORMAL;
                float4 texcoord:TEXCOORD0;
            };
            struct v2f {

                // 顶点在裁剪空间的位置信息
                float4 pos:SV_POSITION;
                float3 worldNormal:TEXCOORD0;
                float3 worldPos:TEXCOORD1;
                float2 uv:TEXCOORD2;
            };

            v2f vert(a2v v) {
                v2f o;
                //顶点的位置从对象空间转换到剪裁空间
                o.pos = UnityObjectToClipPos(v.vertex);
                //将顶点法线从模型空间转换到世界空间，并归一化。
                o.worldNormal =normalize(mul(v.normal,(float3x3)unity_WorldToObject));
                //世界空间顶点坐标
                o.worldPos = normalize(mul(unity_ObjectToWorld,v.vertex)).xyz;

                //o.uv=TRANSFORM_TEX(v.texcoord,_MainTex); 与下方代码相同
                o.uv=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw;
                
                return o;
            }
            fixed4 frag(v2f i) : SV_Target{

               //获取纹理颜色
               fixed3 albedo=tex2D(_MainTex,i.uv).rgb*_Tex_Color.rgb; 
               //将纹理颜色添加到环境光中
               fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*   albedo;

               fixed3 worldNormal=normalize(i.worldNormal);
               //根据世界坐标位置顶点位置，返回从该点到光源的方向向量
               fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
               float halfLambert = dot(worldNormal,worldLightDir)*0.5+0.5;

               //将纹理颜色添加到漫反射中
               fixed3 diffuse= _LightColor0.rgb * _Diffuse.rgb*halfLambert   *albedo;
               //diffuse= _LightColor0.rgb * _Diffuse.rgb*saturate(dot(worldNormal,worldLightDir));
               //根据世界坐标位置顶点位置，返回从该点到摄像机的方向向量
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