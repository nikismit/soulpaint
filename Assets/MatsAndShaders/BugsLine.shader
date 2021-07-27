Shader "Hatsumi/BugsLine"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _AltColor ("AltColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RepeatCount("Repeat Count", float) = 5
        _Spacing("Spacing", float) = 0.5
        _Offset("Offset", float) = 0
        _Speed("Speed", float) = 3
        _ZoomFactor("ZoomFactor", float) = 1.2

    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _AltColor;
            float _RepeatCount;
            float _Spacing;
            float _Offset;
            float _Speed;
            float _ZoomFactor;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 lineuv : TEXCOORD1;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR0;
            };
            float2x2 rotate2d(float angle) {return float2x2(cos(angle),-sin(angle),sin(angle),cos(angle));}
            //-----------------------------------------------------------
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.lineuv = v.uv;
                o.uv.x = (o.uv.x + _Offset) * _RepeatCount * (1.0f + _Spacing);
                o.color = v.color;

                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                float3 color = _Color;
                float3 altcolor = _AltColor;
                float lwidth = 0.5f;
                float repeat = _RepeatCount;
                float time = fmod(_Time*_Speed, 1.0f);
                float2 uv = i.uv;
                float3 col = float3(1.0,1.0,1.0);
                float2 finnaluvpos = uv ;

                float centerDist = 1.0-distance(i.lineuv.y-0.5,0.0)/0.5;
                float prg = 2.0;
                if (fmod(finnaluvpos.x, 2.0)>1.0){
                    prg = 0.0;
                    }else{
                    prg = 2.0;
                }
                float rtime =  fmod( (time*(2.0+prg)), 1.0);
                float fwdtime =  fmod( (rtime+0.1), 1.0);
                float rrtime =  fmod(time*(2.0), 1.0);
                float fwdrrtime =  fmod( (rrtime+0.1), 1.0);
                // finnaluvpos.x -= time;
                float2 center = float2(sin(rtime*6.284)*0.7, cos(rrtime*6.284)*0.5);
                float2 centerfwd = float2(sin(fwdtime*6.284)*0.7, cos(fwdrrtime*6.284)*0.5);
                float2 newUV = (float2(fmod(finnaluvpos.x,1.0),fmod(finnaluvpos.y,1.0))/0.5);//center;
                float zoom = _ZoomFactor;
                float2 scaleCenter = float2(0.5,0.5);
                newUV = (newUV - scaleCenter) * zoom + scaleCenter;
                newUV += center*(zoom*0.5);
                newUV -= .5;
                float angle = atan2(center.y-centerfwd.y,center.x-centerfwd.x);
                newUV = mul(rotate2d(-angle), newUV);
                newUV += .5;
                if(newUV.x>1.0)discard;
                if(newUV.x<0.0)discard;
                if(newUV.y>1.0)discard;
                if(newUV.y<0.0)discard;
                float4 colourmap = tex2D( _MainTex, newUV );
                if(colourmap.a<0.3)discard;
                float4 c = float4(color.rgb*colourmap.rgb, 1.0);
                return c;
            }
            ENDCG
        }
    }
}