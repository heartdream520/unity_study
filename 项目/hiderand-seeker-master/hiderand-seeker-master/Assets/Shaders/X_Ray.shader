Shader "MY/RadarWave"
{
    Properties
    {
        //背景图
        _MainTex ("Texture", 2D) = "white" {}
        //扇形用图
        _RadarTex ("Backgroud Texture",2D) = "white" {}
        //扇形半径，用模型坐标系，所以在0-0.5之间
        _RadarLengthRate ("Radar Length Rate",Range(0,0.5)) = 0.5
        //扇形左边界用的角度值
        _MaxAngle ("Max Angle",Range(0,180)) = 180
        //扇形角度值
        _DeltaAngle ("Delta Angle",Range(0,360)) = 60
        //扇形区域内y轴动画速度
        _ScanSpeed ("Scan Speed",Range(0,20)) = 2
    }
    SubShader
    {
        //设定Tags，考虑到要用透明效果，及方便预览，设定如下
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane"}
        //混合透明度
        Blend SrcAlpha One
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
            float4 _MainTex_ST;
            sampler2D _RadarTex;

            float _RadarLengthRate;
            float _MaxAngle;
            float _DeltaAngle;
            float _ScanSpeed;

            fixed delta(float2 uvPos){
                //计算uv坐标离中心点的距离
                float2 dir = uvPos - float2(0.5,0.5);
                float l = length(dir);
                //考虑到并行编程里少用if判断语句，所以我基本都是赋值0,1搭配lerp使用的方式，这里判断是否在圆内
                float s1 = l < _RadarLengthRate ? 1 : 0;
                //是否在正y轴区域
                float s2 = uvPos.y < 0.5 ? 1 : 0;
                //限制扇形左边界与正x轴的角度
                float maxAngle = _MaxAngle;
                maxAngle = clamp(maxAngle,_DeltaAngle,180);
                //限制扇形右边界与正x轴的角度
                float minAngle = maxAngle - _DeltaAngle;
                minAngle = clamp(minAngle,0,180-_DeltaAngle);
                //判断uv坐标是否在扇形区域
                float t1 = l * cos(maxAngle * 3.14 / 180) + 0.5;
                float t2 = l * cos(minAngle * 3.14 / 180) + 0.5;
                float s3 = uvPos.x < t2 && uvPos.x > t1 ? 1 : 0;
                //返回1或0
                return s1 * s2 * s3;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col1 = tex2D(_MainTex, i.uv);
                //扇形区域的y轴方向加了个动画
                fixed4 col2 = tex2D(_RadarTex, float2(i.uv.x,i.uv.y  +  _Time.y * _ScanSpeed));
                fixed dis = delta(i.uv);
                fixed4 col = lerp(col1,col2 + col1,dis);
                return col;
            }
            ENDCG
        }
    }
}
