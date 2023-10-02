Shader "MY/UiLight"
{
    Properties
    {
        _MainTex("主纹理",2D)="white"{}
        //使用黑白纹理识别边框
        _MaskTex("黑白纹理",2D)="white"{}
        _FlowTex("流光贴图",2D)="white"{}
        _FlowColor("流光颜色",Color)=(1,1,1,1)
        _FlowSpeed("流光速度",Range(0.1,2))=1.0
 
    }
    SubShader
    {
        Pass
        {
            //加这句话的原因是使用的素材中把主图片的A通道拆开了
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            sampler2D _MaskTex;
            sampler2D _FlowTex;
            fixed _FlowSpeed;
            fixed4 _FlowColor;
 
            struct a2v
            {
                float4 vertex:POSITION;
                float2 texcoord:TEXCOORD0;
            };
 
            struct v2f
            {
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
            };
 
 
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = v.texcoord;
                return o;
            }
 
            fixed4 frag(v2f i):SV_Target
            {
                fixed4 texResult_Main = tex2D(_MainTex, i.uv.xy);
 
                fixed4 texResult_Mask = tex2D(_MaskTex, i.uv.xy);
 
                //流光速度计算
                i.uv.x += _Time.y * _FlowSpeed;
 
                //用于做流光范围判定
                fixed alpha = tex2D(_FlowTex, i.uv.xy).a;
 
                //图片的黑色区域和非流光的位置全是0，代表这些位置不需要流光    
                fixed3 color = alpha * texResult_Mask * _FlowColor;
 
                //为0就是显示原来图片，非0就显示流光混合颜色
                fixed3 finalColor = texResult_Main + color;
 
                return fixed4(finalColor, texResult_Main.a);
            }
            ENDCG
 
        }
    }
}