Shader "Hatsumi/LavaLine"{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RepeatCount("Repeat Count", float) = 5
        _Spacing("Spacing", float) = 0.5
        _Offset("Offset", float) = 0
        _Ends("Ends", float) = 1

        _NoiseVariable_A("NoiseVariable_A", float) = 0
        _NoiseVariable_B("NoiseVariable_B", float) = 0
        _NoiseVariable_C("NoiseVariable_C", float) = 0
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
            float _Ends;
            float _NoiseVariable_A;
            float _NoiseVariable_B;
            float _NoiseVariable_C;
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
                float lwidth = 0.5f;
                float repeat = _RepeatCount;
                float time = fmod(_Time*2.0f, 1.0f);
                float slowtime = fmod(_Time*2.0f, 1.0f);
                float2 uv = i.uv;
                float3 col = float3(1.0,1.0,1.0);
                float4 c = float4(1.0,1.0,1.0,1.0);
                float2 finnaluvpos = i.lineuv;
                float2 p = finnaluvpos - float2(0.5, 0.5);
                float2 snoisef2 = finnaluvpos;// * float2(2.f,4.0f*lwidth);
                float rocks = pow( snoise( float2(i.uv.x*0.5,i.lineuv.y)*0.1*_NoiseVariable_A),4.f)*4.0f;

                float centerDist = 1.0-distance(i.lineuv.y-0.5,0.0)/0.5;
                float vignette = smoothstep(.1, 1.3, centerDist);
                float lengthNormal = 1.0;
                float lineEnds = smoothstep(0.0,0.2, i.lineuv.x);
                lineEnds *= smoothstep( 1.,0.8, clamp(i.lineuv.x/lengthNormal, 0.0, 1.));
                float endswidth = lineEnds;

                rocks *= 1.0+_NoiseVariable_B-(endswidth*0.1);
                rocks /= (smoothstep(0.0, 0.9, centerDist));

                if( distance(i.lineuv.y,0.5)*2.0 > endswidth ) discard;
                rocks = clamp(rocks+(1.0-endswidth), 0.0,1.0);

                finnaluvpos.x -= time ;
                finnaluvpos.y -= slowtime ;
                float4 colourmap = tex2D( _MainTex, finnaluvpos);
                float grey = (colourmap.r+colourmap.b+colourmap.g);

                c.rgb *= colourmap.rgb;
                c.rgb *= vignette;
                if( c.r+(1.0-rocks) < (_NoiseVariable_C*0.1) /endswidth) discard;
                float pressureRange = 1.0;
                float3 colourMix = color.rgb;
                c.rgb = colourMix * (rocks/(1.0-vignette));
                c.a *= lineEnds;
                return c;
            }
            ENDCG
        }
    }
}