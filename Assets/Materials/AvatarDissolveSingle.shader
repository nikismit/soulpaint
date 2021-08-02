// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyStencil/AvatarDissolveSingle"
{
	Properties
	{
		_SpecColor("Specular Color",Color)=(1,1,1,1)
		_Basecolor1("Base color 1", Color) = (0,0,0,0)
		_Cutoff( "Mask Clip Value", Float ) = 0.82
		_Glowcolor1("Glow color 1", Color) = (0,0,0,0)
		_Power1("Power 1", Range( -3 , 6)) = 2
		_Progress("Progress", Range( 0 , 1)) = 0
		_DissolveMap("DissolveMap", 2D) = "white" {}
		_MainTexture("MainTexture", 2D) = "white" {}
		_Gloss("Gloss", Range( 0 , 10)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		Stencil
		{
			Ref 0
			Fail Keep
			ZFail Keep
		}
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf BlinnPhong keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform float4 _Basecolor1;
		uniform float4 _Glowcolor1;
		uniform float _Power1;
		uniform float _Gloss;
		uniform float _Progress;
		uniform sampler2D _DissolveMap;
		uniform float4 _DissolveMap_ST;
		uniform float _Cutoff = 0.82;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV32 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode32 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV32, _Power1 ) );
			o.Emission = ( ( tex2D( _MainTexture, uv_MainTexture ) * _Basecolor1 ) + ( _Glowcolor1 * fresnelNode32 ) ).rgb;
			o.Gloss = _Gloss;
			o.Alpha = 1;
			float2 uv_DissolveMap = i.uv_texcoord * _DissolveMap_ST.xy + _DissolveMap_ST.zw;
			float4 temp_cast_1 = (_Progress).xxxx;
			float4 ifLocalVar48 = 0;
			if( _Progress < 1.0 )
				ifLocalVar48 = step( tex2D( _DissolveMap, uv_DissolveMap ) , temp_cast_1 );
			clip( ifLocalVar48.r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
0;0;1920;1139;1172.593;115.2795;1.04829;True;True
Node;AmplifyShaderEditor.RangedFloatNode;30;-1122.365,225.6519;Float;False;Property;_Power1;Power 1;4;0;Create;True;0;0;0;False;0;False;2;0.09;-3;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;32;-797.943,127.7329;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-657.5533,-143.0954;Float;False;Property;_Basecolor1;Base color 1;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.09936776,0.5029557,0.7264151,0.1294118;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;-747.359,-339.6553;Inherit;True;Property;_MainTexture;MainTexture;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;-1045.227,18.82997;Float;False;Property;_Glowcolor1;Glow color 1;3;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-548.8105,543.7791;Float;False;Property;_Progress;Progress;5;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-706.9556,242.8678;Inherit;True;Property;_DissolveMap;DissolveMap;6;0;Create;True;0;0;0;False;0;False;-1;None;ef1b15b6b4cf05f4f8adc227bc02e1b1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-459.4801,36.93577;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-351.8756,-192.8173;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;12;-236.304,297.0325;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-451.3698,658.4151;Inherit;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-220.3647,24.4224;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-306.4146,180.4917;Float;False;Property;_Gloss;Gloss;8;0;Create;True;0;0;0;False;0;False;0;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;48;22.45713,524.2339;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;284.1831,1.102584;Float;False;True;-1;2;ASEMaterialInspector;0;0;BlinnPhong;AmplifyStencil/AvatarDissolveSingle;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.82;True;False;0;False;TransparentCutout;;AlphaTest;ForwardOnly;18;all;True;True;True;True;0;False;-1;True;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;5;False;-1;10;False;-1;0;1;False;-1;1;False;-1;0;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;0;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;32;3;30;0
WireConnection;36;0;31;0
WireConnection;36;1;32;0
WireConnection;35;0;34;0
WireConnection;35;1;33;0
WireConnection;12;0;29;0
WireConnection;12;1;47;0
WireConnection;37;0;35;0
WireConnection;37;1;36;0
WireConnection;48;0;47;0
WireConnection;48;1;53;0
WireConnection;48;4;12;0
WireConnection;0;2;37;0
WireConnection;0;4;42;0
WireConnection;0;10;48;0
ASEEND*/
//CHKSM=5312F6946D713B16D56F76295E6AC1F73C409114