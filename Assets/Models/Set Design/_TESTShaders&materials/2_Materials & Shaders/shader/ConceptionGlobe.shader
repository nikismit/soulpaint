// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:0,fgcb:0.8482757,fgca:1,fgde:0.12,fgrn:5.52,fgrf:11.3,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5552,x:34638,y:32963,varname:node_5552,prsc:2|diff-2767-OUT,emission-7547-OUT,voffset-1339-OUT,tess-7297-OUT;n:type:ShaderForge.SFN_TexCoord,id:1842,x:32306,y:32701,varname:node_1842,prsc:2,uv:1;n:type:ShaderForge.SFN_ComponentMask,id:7588,x:32485,y:32742,varname:node_7588,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1842-V;n:type:ShaderForge.SFN_Lerp,id:2767,x:33845,y:32658,varname:node_2767,prsc:2|A-2739-RGB,B-2255-RGB,T-2944-OUT;n:type:ShaderForge.SFN_Color,id:2739,x:33251,y:32291,ptovrint:False,ptlb:node_2739,ptin:_node_2739,varname:node_2739,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.7517242,c4:1;n:type:ShaderForge.SFN_Color,id:2255,x:33396,y:32533,ptovrint:False,ptlb:node_2255,ptin:_node_2255,varname:node_2255,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9933063,c2:0.5147059,c3:1,c4:1;n:type:ShaderForge.SFN_Clamp01,id:2944,x:33617,y:32700,varname:node_2944,prsc:2|IN-1072-OUT;n:type:ShaderForge.SFN_Multiply,id:8128,x:32953,y:32723,varname:node_8128,prsc:2|A-9427-OUT,B-7224-OUT,C-6470-OUT;n:type:ShaderForge.SFN_Sin,id:6719,x:33116,y:32723,varname:node_6719,prsc:2|IN-8128-OUT;n:type:ShaderForge.SFN_RemapRange,id:1072,x:33451,y:32700,varname:node_1072,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-6719-OUT;n:type:ShaderForge.SFN_Tau,id:6470,x:32828,y:32808,varname:node_6470,prsc:2;n:type:ShaderForge.SFN_Slider,id:9427,x:32313,y:32595,ptovrint:False,ptlb:gradient amount,ptin:_gradientamount,varname:node_9427,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4491841,max:200;n:type:ShaderForge.SFN_Time,id:4901,x:32162,y:32897,varname:node_4901,prsc:2;n:type:ShaderForge.SFN_Add,id:7224,x:32688,y:32776,varname:node_7224,prsc:2|A-7588-OUT,B-2282-OUT;n:type:ShaderForge.SFN_Multiply,id:2282,x:32485,y:32990,varname:node_2282,prsc:2|A-4901-TSL,B-4533-OUT;n:type:ShaderForge.SFN_Slider,id:4533,x:32138,y:33090,ptovrint:False,ptlb:speed,ptin:_speed,varname:_node_9427_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:28.10831,max:200;n:type:ShaderForge.SFN_Add,id:7547,x:34203,y:32985,varname:node_7547,prsc:2|A-8769-OUT,B-2767-OUT;n:type:ShaderForge.SFN_Slider,id:8769,x:33686,y:32994,ptovrint:False,ptlb:brightness color band,ptin:_brightnesscolorband,varname:node_8769,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:-0.618355,max:1;n:type:ShaderForge.SFN_ComponentMask,id:2160,x:32113,y:33688,varname:node_2160,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1708-V;n:type:ShaderForge.SFN_Lerp,id:882,x:33908,y:33532,varname:node_882,prsc:2|A-3478-RGB,B-5547-RGB,T-8033-OUT;n:type:ShaderForge.SFN_Color,id:3478,x:33139,y:33231,ptovrint:False,ptlb:node_2739_copy,ptin:_node_2739_copy,varname:_node_2739_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.7517242,c4:1;n:type:ShaderForge.SFN_Color,id:5547,x:33277,y:33481,ptovrint:False,ptlb:node_2255_copy,ptin:_node_2255_copy,varname:_node_2255_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9933063,c2:0.5147059,c3:1,c4:1;n:type:ShaderForge.SFN_Clamp01,id:8033,x:33498,y:33648,varname:node_8033,prsc:2|IN-8028-OUT;n:type:ShaderForge.SFN_Multiply,id:8803,x:32834,y:33671,varname:node_8803,prsc:2|A-3786-OUT,B-5718-OUT,C-3337-OUT;n:type:ShaderForge.SFN_Sin,id:6057,x:32997,y:33671,varname:node_6057,prsc:2|IN-8803-OUT;n:type:ShaderForge.SFN_RemapRange,id:8028,x:33332,y:33648,varname:node_8028,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-6057-OUT;n:type:ShaderForge.SFN_Tau,id:3337,x:32709,y:33756,varname:node_3337,prsc:2;n:type:ShaderForge.SFN_Slider,id:3786,x:32302,y:33416,ptovrint:False,ptlb:gradient amount wobl,ptin:_gradientamountwobl,varname:_gradientamount_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4.302442,max:99;n:type:ShaderForge.SFN_Time,id:8424,x:32019,y:33868,varname:node_8424,prsc:2;n:type:ShaderForge.SFN_Add,id:5718,x:32569,y:33724,varname:node_5718,prsc:2|A-2160-OUT,B-9013-OUT;n:type:ShaderForge.SFN_Multiply,id:9013,x:32323,y:33788,varname:node_9013,prsc:2|A-8424-TSL,B-4880-OUT;n:type:ShaderForge.SFN_Slider,id:4880,x:31164,y:34743,ptovrint:False,ptlb:speed_copy,ptin:_speed_copy,varname:_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:5;n:type:ShaderForge.SFN_Multiply,id:1339,x:34137,y:33394,varname:node_1339,prsc:2|A-6903-OUT,B-882-OUT;n:type:ShaderForge.SFN_Slider,id:6903,x:33572,y:33259,ptovrint:False,ptlb:brightness wobl,ptin:_brightnesswobl,varname:node_6903,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:30;n:type:ShaderForge.SFN_ValueProperty,id:7297,x:34211,y:33250,ptovrint:False,ptlb:tesselation,ptin:_tesselation,varname:node_7297,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:1708,x:31704,y:33590,varname:node_1708,prsc:2,uv:1;n:type:ShaderForge.SFN_ValueProperty,id:8354,x:34403,y:33442,ptovrint:False,ptlb:tesselation_copy,ptin:_tesselation_copy,varname:_tesselation_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1958,x:34361,y:33164,ptovrint:False,ptlb:node_1958,ptin:_node_1958,varname:node_1958,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:255;proporder:2739-2255-9427-4533-8769-3478-5547-3786-4880-6903-7297-1958;pass:END;sub:END;*/

Shader "ConceptionGlobe" {
    Properties {
        _node_2739 ("node_2739", Color) = (0,1,0.7517242,1)
        _node_2255 ("node_2255", Color) = (0.9933063,0.5147059,1,1)
        _gradientamount ("gradient amount", Range(0, 200)) = 0.4491841
        _speed ("speed", Range(0, 200)) = 28.10831
        _brightnesscolorband ("brightness color band", Range(-2, 1)) = -0.618355
        _node_2739_copy ("node_2739_copy", Color) = (0,1,0.7517242,1)
        _node_2255_copy ("node_2255_copy", Color) = (0.9933063,0.5147059,1,1)
        _gradientamountwobl ("gradient amount wobl", Range(0, 99)) = 4.302442
        _speed_copy ("speed_copy", Range(0, 5)) = 5
        _brightnesswobl ("brightness wobl", Range(0, 30)) = 0
        _tesselation ("tesselation", Float ) = 1
        _node_1958 ("node_1958", Float ) = 255
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _node_2739;
            uniform float4 _node_2255;
            uniform float _gradientamount;
            uniform float _speed;
            uniform float _brightnesscolorband;
            uniform float4 _node_2739_copy;
            uniform float4 _node_2255_copy;
            uniform float _gradientamountwobl;
            uniform float _speed_copy;
            uniform float _brightnesswobl;
            uniform float _tesselation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_8424 = _Time + _TimeEditor;
                v.vertex.xyz += (_brightnesswobl*lerp(_node_2739_copy.rgb,_node_2255_copy.rgb,saturate((sin((_gradientamountwobl*(o.uv1.g.r+(node_8424.r*_speed_copy))*6.28318530718))*0.5+0.5))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }            
            float4 frag(VertexOutput i) : COLOR {
                float4 node_4901 = _Time + _TimeEditor;
                float3 node_2767 = lerp(_node_2739.rgb,_node_2255.rgb,saturate((sin((_gradientamount*(i.uv1.g.r+(node_4901.r*_speed))*6.28318530718))*0.5+0.5)));
                float3 diffuseColor = node_2767;
                float3 diffuse = diffuseColor;
////// Emissive:
                float3 emissive = (_brightnesscolorband+node_2767);
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
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _node_2739;
            uniform float4 _node_2255;
            uniform float _gradientamount;
            uniform float _speed;
            uniform float _brightnesscolorband;
            uniform float4 _node_2739_copy;
            uniform float4 _node_2255_copy;
            uniform float _gradientamountwobl;
            uniform float _speed_copy;
            uniform float _brightnesswobl;
            uniform float _tesselation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_8424 = _Time + _TimeEditor;
                v.vertex.xyz += (_brightnesswobl*lerp(_node_2739_copy.rgb,_node_2255_copy.rgb,saturate((sin((_gradientamountwobl*(o.uv1.g.r+(node_8424.r*_speed_copy))*6.28318530718))*0.5+0.5))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
               
/////// Diffuse:
                float4 node_4901 = _Time + _TimeEditor;
                float3 node_2767 = lerp(_node_2739.rgb,_node_2255.rgb,saturate((sin((_gradientamount*(i.uv1.g.r+(node_4901.r*_speed))*6.28318530718))*0.5+0.5)));
                float3 diffuse = node_2767;
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
