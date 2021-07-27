Shader "Hatsumi/LightningLine"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RepeatCount("Repeat Count", float) = 5
        _Spacing("Spacing", float) = 0.5
        _Offset("Offset", float) = 0
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
            float _RepeatCount;
            float _Spacing;
            float _Offset;
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
                float speed = 2.0;
                float time = fmod(_Time*4.0f, 1.0f);
                float gtime =  fmod( 0.2+(time*speed), 1.0);
                float ltime =  fmod( (time*4.), 1.0);
                float flipper = fmod( (time*3.), 1.0)-0.5;

                float repeat = _RepeatCount;
                if(flipper>0.4)repeat +=0.1;
                float2 uv = i.uv;
                float2 finnaluvpos = uv;
                finnaluvpos.x *= repeat;
                if(flipper>0.0)finnaluvpos.y = 1.0-finnaluvpos.y;
                if(time>0.8)finnaluvpos.x -= 0.5;
                if(time>0.1)finnaluvpos.x += 0.5;

                float4 colourmap = tex2D(_MainTex, finnaluvpos );
                float grey = (colourmap.r+colourmap.b+colourmap.g) / 3.0;
                float rdistcol = colourmap.r;
                float gdistcol = colourmap.g;
                float bdistcol = colourmap.b;
                float light = rdistcol;
                light = lerp(light,gdistcol,ltime);
                light = lerp(light,bdistcol,gtime);
                if(grey<0.1) discard;
                float4 c =  float4(color.rgb, light *3.0);
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