// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1470588,fgcg:0,fgcb:0,fgca:1,fgde:0.04,fgrn:3.6,fgrf:224.3,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5517,x:34594,y:33138,varname:node_5517,prsc:2|diff-3453-OUT,emission-3453-OUT,voffset-7532-OUT;n:type:ShaderForge.SFN_TexCoord,id:2235,x:32724,y:33814,varname:node_2235,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:5953,x:31291,y:35528,varname:node_5953,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3192-U;n:type:ShaderForge.SFN_Lerp,id:5006,x:32864,y:35336,varname:node_5006,prsc:2|A-3620-RGB,B-1465-RGB,T-7847-OUT;n:type:ShaderForge.SFN_Color,id:3620,x:32088,y:35043,ptovrint:False,ptlb:node_2739_copy,ptin:_node_2739_copy,varname:_node_2739_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:1465,x:32233,y:35285,ptovrint:False,ptlb:node_2255_copy,ptin:_node_2255_copy,varname:_node_2255_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Clamp01,id:7847,x:32454,y:35452,varname:node_7847,prsc:2|IN-9193-OUT;n:type:ShaderForge.SFN_Multiply,id:2815,x:31790,y:35475,varname:node_2815,prsc:2|A-6878-OUT,B-4014-OUT,C-5722-OUT;n:type:ShaderForge.SFN_Sin,id:2500,x:31953,y:35475,varname:node_2500,prsc:2|IN-2815-OUT;n:type:ShaderForge.SFN_RemapRange,id:9193,x:32288,y:35452,varname:node_9193,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-2500-OUT;n:type:ShaderForge.SFN_Tau,id:5722,x:31790,y:35631,varname:node_5722,prsc:2;n:type:ShaderForge.SFN_Slider,id:6878,x:31291,y:35429,ptovrint:False,ptlb:gradient amount wobl,ptin:_gradientamountwobl,varname:_gradientamount_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4.302442,max:8;n:type:ShaderForge.SFN_Time,id:4046,x:31110,y:35665,varname:node_4046,prsc:2;n:type:ShaderForge.SFN_Add,id:4014,x:31525,y:35528,varname:node_4014,prsc:2|A-5953-OUT,B-4419-OUT;n:type:ShaderForge.SFN_Multiply,id:4419,x:31525,y:35714,varname:node_4419,prsc:2|A-4046-TSL,B-8449-OUT;n:type:ShaderForge.SFN_Slider,id:8449,x:31110,y:35824,ptovrint:False,ptlb:speed_copy,ptin:_speed_copy,varname:_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:5;n:type:ShaderForge.SFN_Slider,id:3835,x:32528,y:35063,ptovrint:False,ptlb:brightness wobl,ptin:_brightnesswobl,varname:node_6903,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.40885,max:1;n:type:ShaderForge.SFN_TexCoord,id:3192,x:31110,y:35509,varname:node_3192,prsc:2,uv:1;n:type:ShaderForge.SFN_Multiply,id:8102,x:33426,y:33988,varname:node_8102,prsc:2|A-3835-OUT,B-5006-OUT;n:type:ShaderForge.SFN_Multiply,id:7532,x:33499,y:33777,varname:node_7532,prsc:2|A-7721-OUT,B-8102-OUT;n:type:ShaderForge.SFN_Lerp,id:7721,x:33088,y:33801,varname:node_7721,prsc:2|A-5282-OUT,B-6826-OUT,T-2235-V;n:type:ShaderForge.SFN_Vector3,id:5282,x:32851,y:33647,varname:node_5282,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:6826,x:32851,y:33736,varname:node_6826,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_TexCoord,id:2776,x:32370,y:32765,varname:node_2776,prsc:2,uv:1;n:type:ShaderForge.SFN_ComponentMask,id:8784,x:32549,y:32806,varname:node_8784,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2776-V;n:type:ShaderForge.SFN_Lerp,id:5893,x:33909,y:32722,varname:node_5893,prsc:2|A-7545-RGB,B-6399-RGB,T-758-OUT;n:type:ShaderForge.SFN_Color,id:7545,x:33315,y:32355,ptovrint:False,ptlb:colort,ptin:_colort,varname:node_2739,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.7517242,c4:1;n:type:ShaderForge.SFN_Color,id:6399,x:33460,y:32597,ptovrint:False,ptlb:color,ptin:_color,varname:node_2255,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9933063,c2:0.5147059,c3:1,c4:1;n:type:ShaderForge.SFN_Clamp01,id:758,x:33681,y:32764,varname:node_758,prsc:2|IN-2525-OUT;n:type:ShaderForge.SFN_Multiply,id:8773,x:33017,y:32787,varname:node_8773,prsc:2|A-1707-OUT,B-63-OUT,C-1259-OUT;n:type:ShaderForge.SFN_Sin,id:7706,x:33180,y:32787,varname:node_7706,prsc:2|IN-8773-OUT;n:type:ShaderForge.SFN_RemapRange,id:2525,x:33515,y:32764,varname:node_2525,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-7706-OUT;n:type:ShaderForge.SFN_Tau,id:1259,x:32892,y:32872,varname:node_1259,prsc:2;n:type:ShaderForge.SFN_Slider,id:1707,x:32377,y:32659,ptovrint:False,ptlb:gradient amount,ptin:_gradientamount,varname:node_9427,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4491841,max:200;n:type:ShaderForge.SFN_Time,id:591,x:32226,y:32961,varname:node_591,prsc:2;n:type:ShaderForge.SFN_Add,id:63,x:32752,y:32840,varname:node_63,prsc:2|A-8784-OUT,B-6797-OUT;n:type:ShaderForge.SFN_Multiply,id:6797,x:32549,y:33054,varname:node_6797,prsc:2|A-591-TSL,B-8608-OUT;n:type:ShaderForge.SFN_Slider,id:8608,x:32202,y:33154,ptovrint:False,ptlb:speed,ptin:_speed,varname:_node_9427_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:28.10831,max:200;n:type:ShaderForge.SFN_Add,id:3453,x:34267,y:33049,varname:node_3453,prsc:2|A-7676-OUT,B-5893-OUT;n:type:ShaderForge.SFN_Slider,id:7676,x:33750,y:33058,ptovrint:False,ptlb:brightness color band,ptin:_brightnesscolorband,varname:node_8769,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:-0.618355,max:1;proporder:3620-1465-6878-8449-3835-7545-6399-1707-8608-7676;pass:END;sub:END;*/

Shader "Strengen" {
    Properties {
        _node_2739_copy ("node_2739_copy", Color) = (1,1,1,1)
        _node_2255_copy ("node_2255_copy", Color) = (0,0,0,1)
        _gradientamountwobl ("gradient amount wobl", Range(0, 8)) = 4.302442
        _speed_copy ("speed_copy", Range(0, 5)) = 5
        _brightnesswobl ("brightness wobl", Range(0, 1)) = 0.40885
        _colort ("colort", Color) = (0,1,0.7517242,1)
        _color ("color", Color) = (0.9933063,0.5147059,1,1)
        _gradientamount ("gradient amount", Range(0, 200)) = 0.4491841
        _speed ("speed", Range(0, 200)) = 28.10831
        _brightnesscolorband ("brightness color band", Range(-2, 1)) = -0.618355
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _node_2739_copy;
            uniform float4 _node_2255_copy;
            uniform float _gradientamountwobl;
            uniform float _speed_copy;
            uniform float _brightnesswobl;
            uniform float4 _colort;
            uniform float4 _color;
            uniform float _gradientamount;
            uniform float _speed;
            uniform float _brightnesscolorband;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_4046 = _Time + _TimeEditor;
                v.vertex.xyz += (lerp(float3(0,0,0),float3(1,1,1),o.uv0.g)*(_brightnesswobl*lerp(_node_2739_copy.rgb,_node_2255_copy.rgb,saturate((sin((_gradientamountwobl*(o.uv1.r.r+(node_4046.r*_speed_copy))*6.28318530718))*0.5+0.5)))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_591 = _Time + _TimeEditor;
                float3 node_3453 = (_brightnesscolorband+lerp(_colort.rgb,_color.rgb,saturate((sin((_gradientamount*(i.uv1.g.r+(node_591.r*_speed))*6.28318530718))*0.5+0.5))));
                float3 diffuseColor = node_3453;
                float3 diffuse = (indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_3453;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _node_2739_copy;
            uniform float4 _node_2255_copy;
            uniform float _gradientamountwobl;
            uniform float _speed_copy;
            uniform float _brightnesswobl;
            uniform float4 _colort;
            uniform float4 _color;
            uniform float _gradientamount;
            uniform float _speed;
            uniform float _brightnesscolorband;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_4046 = _Time + _TimeEditor;
                v.vertex.xyz += (lerp(float3(0,0,0),float3(1,1,1),o.uv0.g)*(_brightnesswobl*lerp(_node_2739_copy.rgb,_node_2255_copy.rgb,saturate((sin((_gradientamountwobl*(o.uv1.r.r+(node_4046.r*_speed_copy))*6.28318530718))*0.5+0.5)))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 node_591 = _Time + _TimeEditor;
                float3 node_3453 = (_brightnesscolorband+lerp(_colort.rgb,_color.rgb,saturate((sin((_gradientamount*(i.uv1.g.r+(node_591.r*_speed))*6.28318530718))*0.5+0.5))));
                float3 diffuseColor = node_3453;
                float3 diffuse = diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
