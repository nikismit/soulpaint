Shader "Hatsumi/FireLine"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _AltColor ("AltColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RepeatCount("Repeat Count", float) = 5
        _Spacing("Spacing", float) = 0.5
        _Offset("Offset", float) = 0

        _Speed("Speed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Blend SrcAlpha One
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

            float snoise(float3 uv, float res){
                float3 s = float3(1e0, 1e2, 1e3);
                uv *= res;
                float3 uv0 = floor(fmod(uv, res))*s;
                float3 uv1 = floor(fmod(uv+float3(1.,1.,1.), res))*s;
                float3 f = frac(uv);
                f = f*f*(3.0-2.0*f);
                float4 v = float4(uv0.x+uv0.y+uv0.z, uv1.x+uv0.y+uv0.z,uv0.x+uv1.y+uv0.z, uv1.x+uv1.y+uv0.z);
                float4 r = frac(sin(v*1e-1)*1e3);
                float r0 = lerp(lerp(r.x, r.y, f.x), lerp(r.z, r.w, f.x), f.y);
                r = frac(sin((v + uv1.z - uv0.z)*1e-1)*1e3);
                float r1 = lerp(lerp(r.x, r.y, f.x), lerp(r.z, r.w, f.x), f.y);
                return lerp(r0, r1, f.z)*2.-1.;

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
                float3 altcolor = _AltColor;
                float lwidth = 0.5f;
                float repeat = 1.31;//_RepeatCount;
                float time = fmod(_Time*4.0f*_RepeatCount*_Speed, 1.0f);
                float slowtime = fmod(_Time, 1.0f);
                float2 uv = i.uv;
                float3 col = float3(1.0,1.0,1.0);
                float4 c = float4(1.0,1.0,1.0,1.0);
                float2 finnaluvpos = uv * repeat+((1.0-lwidth)*30.0);
                float rtime =  fmod( (time*3.)+(uv.x*2.0), 1.0);
                float loopgtime = abs(time-0.5)*1.0;
                float _static = snoise( float3(1.,4.,1.)*(loopgtime)+finnaluvpos.x, 1.0 );
                finnaluvpos += float2(_static,_static)*.1;
                float2 p = finnaluvpos - float2(0.5, 0.5);
                float distFromAxis = distance(i.lineuv.y,0.5);
                float centerDist = 1.0-distance(i.lineuv.y-0.5,0.0)/0.5;
                finnaluvpos.y += time ;
                finnaluvpos.x -= sin((distFromAxis*6.284)+(time*3.141*2.))*0.08;
                float4 colourmap = tex2D( _MainTex, finnaluvpos );
                float grey = (colourmap.r+colourmap.b+colourmap.g) / 3.0;

                float vignette = smoothstep(0.0, 1., centerDist);
                vignette *= 1.0+(grey*0.931);
                c = colourmap;
                c += vignette;
                c.r *= c.r*c.r*0.5;//*(1.-(pressureRange*0.8));
                c.g *= c.g*(0.35+abs(sin(uv.x*12.0))*0.1);
                c.b *= 1.1;
                float lengthNormal = 1.0;
                float lineEnds = smoothstep(0.0,0.2, i.lineuv.x);
                lineEnds *= smoothstep( 1.,0.8, clamp(i.lineuv.x/lengthNormal, 0.0, 1.));
                float endswidth = (1.0-lineEnds)*0.5;
                // c.a *= endswidth;
                if(c.g <0.58+endswidth) discard;
                float3 colourMix = color.rgb;//mix(color.rgb, altcolor.rgb,distFromAxis*0.4);
                colourMix = lerp(colourMix, pow(altcolor.rgb,float3(10.0,10.0,10.0))*float3(vignette,vignette,vignette),c.r-c.b); //inner rim
                c.rgb = colourMix*(2.0 + (i.lineuv.x*3.0));
                return c;
            }
            ENDCG
        }
    }
}