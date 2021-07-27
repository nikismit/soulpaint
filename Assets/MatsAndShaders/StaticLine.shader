﻿Shader "Hatsumi/StaticLine"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RepeatCount("Repeat Count", float) = 5
        _Spacing("Spacing", float) = 0.5
        _Offset("Offset", float) = 0
        _Speed("Speed", float) = 1
        _NoiseVariable("NoiseVariable", float) = 1
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
            float _RepeatCount;
            float _Spacing;
            float _Offset;
            float _Speed;
            float _NoiseVariable;

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
            float Rand(float2 co, float2 amount){
                return frac(sin(dot(co.xy ,float2(122.78, 118.92)*amount.x)) + cos(dot(co.xy ,float2(122.78, 118.92)*amount.y)  ));
            }

            float3 permute(in float3 x) { return fmod( x*x*34.+x, 289.); }
            float snoise(in float2 v) {
                float2 i = floor((v.x+v.y)*.36602540378443 + v);
                float2 x0 = (i.x+i.y)*.211324865405187 + v - i;
                float s = step(x0.x,x0.y);
                float2 j = float2(1.0-s,s);
                float2 x1 = x0 - j + .211324865405187;
                float2 x3 = x0 - .577350269189626;
                i = fmod(i,289.);
                float3 p = permute( permute( i.y + float3(0, j.y, 1 ))+ i.x + float3(0, j.x, 1 )   );
                float3 m = max( .5 - float3(dot(x0,x0), dot(x1,x1), dot(x3,x3)), 0.);
                float3 x = frac(p * .024390243902439) * 2. - 1.;
                float3 h = abs(x) - .5;
                float3 a0 = x - floor(x + .5);
                return .5 + 65. * dot( pow(m, float3(4.,4.,4.))*(-0.85373472095314*( a0*a0 + h*h )+1.79284291400159), a0 * float3(x0.x,x1.x,x3.x) + h * float3(x0.y,x1.y,x3.y) );
            }

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
                float time = fmod(_Time*_Speed, 1.0f);
                float2 uv = i.uv;
                float2 finnaluvpos = uv * _RepeatCount ;
                float centerDist = 1.0-distance(i.lineuv.y-0.5,0.0)/0.5;
                float loopgtime = abs(time-0.5)*1.0;
                float4 c = float4(color.rgb,1.0);
                float vignette = smoothstep(0.0, 1.3, centerDist);
                float randPos = Rand(float2(loopgtime,loopgtime), float2(10.2, 10.1));
                float _static = snoise( finnaluvpos*(20.0+randPos) );
                float finStatic = vignette * _static * _NoiseVariable;
                if(finStatic<(0.05 + centerDist*0.5))discard;
                c.rgb *= _static;
                c.a = _static;
                float lengthNormal = 1.0;
                float lineEnds = smoothstep(0.0,0.2, i.lineuv.x);
                lineEnds *= smoothstep( 1.,0.8, clamp(i.lineuv.x/lengthNormal, 0.0, 1.));
                float endswidth = lineEnds;
                c.a *= endswidth;
                return c;
            }
            ENDCG
        }
    }
}