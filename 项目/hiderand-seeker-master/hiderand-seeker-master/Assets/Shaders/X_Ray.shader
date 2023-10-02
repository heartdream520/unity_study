Shader "MY/RadarWave"
{
    Properties
    {
        //����ͼ
        _MainTex ("Texture", 2D) = "white" {}
        //������ͼ
        _RadarTex ("Backgroud Texture",2D) = "white" {}
        //���ΰ뾶����ģ������ϵ��������0-0.5֮��
        _RadarLengthRate ("Radar Length Rate",Range(0,0.5)) = 0.5
        //������߽��õĽǶ�ֵ
        _MaxAngle ("Max Angle",Range(0,180)) = 180
        //���νǶ�ֵ
        _DeltaAngle ("Delta Angle",Range(0,360)) = 60
        //����������y�ᶯ���ٶ�
        _ScanSpeed ("Scan Speed",Range(0,20)) = 2
    }
    SubShader
    {
        //�趨Tags�����ǵ�Ҫ��͸��Ч����������Ԥ�����趨����
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane"}
        //���͸����
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
                //����uv���������ĵ�ľ���
                float2 dir = uvPos - float2(0.5,0.5);
                float l = length(dir);
                //���ǵ����б��������if�ж���䣬�����һ������Ǹ�ֵ0,1����lerpʹ�õķ�ʽ�������ж��Ƿ���Բ��
                float s1 = l < _RadarLengthRate ? 1 : 0;
                //�Ƿ�����y������
                float s2 = uvPos.y < 0.5 ? 1 : 0;
                //����������߽�����x��ĽǶ�
                float maxAngle = _MaxAngle;
                maxAngle = clamp(maxAngle,_DeltaAngle,180);
                //���������ұ߽�����x��ĽǶ�
                float minAngle = maxAngle - _DeltaAngle;
                minAngle = clamp(minAngle,0,180-_DeltaAngle);
                //�ж�uv�����Ƿ�����������
                float t1 = l * cos(maxAngle * 3.14 / 180) + 0.5;
                float t2 = l * cos(minAngle * 3.14 / 180) + 0.5;
                float s3 = uvPos.x < t2 && uvPos.x > t1 ? 1 : 0;
                //����1��0
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
                //���������y�᷽����˸�����
                fixed4 col2 = tex2D(_RadarTex, float2(i.uv.x,i.uv.y  +  _Time.y * _ScanSpeed));
                fixed dis = delta(i.uv);
                fixed4 col = lerp(col1,col2 + col1,dis);
                return col;
            }
            ENDCG
        }
    }
}
