// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SoulPaint/electricity"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 316
		_Tex("Tex", 2D) = "white" {}
		_Size("Size", Range( 0 , 10)) = 1
		_TextureSample4("Texture Sample 4", 2D) = "white" {}
		_TextureSample5("Texture Sample 5", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample4;
		uniform float4 _TextureSample4_ST;
		uniform sampler2D _Tex;
		uniform sampler2D _TextureSample5;
		uniform float4 _TextureSample5_ST;
		uniform float _Size;
		uniform float _Cutoff = 316;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode2 = tex2D( _TextureSample0, uv_TextureSample0 );
			float2 uv_TextureSample4 = i.uv_texcoord * _TextureSample4_ST.xy + _TextureSample4_ST.zw;
			float4 tex2DNode15 = tex2D( _TextureSample4, uv_TextureSample4 );
			float2 uv_TextureSample5 = i.uv_texcoord * _TextureSample5_ST.xy + _TextureSample5_ST.zw;
			float4 tex2DNode25 = tex2D( _TextureSample5, uv_TextureSample5 );
			float2 temp_output_4_0_g15 = (( tex2DNode25.rg / _Size )).xy;
			float2 temp_output_41_0_g15 = ( float2( 1,1 ) + 0.5 );
			float2 temp_cast_1 = (2.64).xx;
			float2 temp_output_17_0_g15 = temp_cast_1;
			float mulTime22_g15 = _Time.y * 2.64;
			float temp_output_27_0_g15 = frac( mulTime22_g15 );
			float2 temp_output_11_0_g15 = ( temp_output_4_0_g15 + ( temp_output_41_0_g15 * temp_output_17_0_g15 * temp_output_27_0_g15 ) );
			float2 temp_output_12_0_g15 = ( temp_output_4_0_g15 + ( temp_output_41_0_g15 * temp_output_17_0_g15 * frac( ( mulTime22_g15 + 0.5 ) ) ) );
			float3 lerpResult9_g15 = lerp( UnpackNormal( tex2D( _Tex, temp_output_11_0_g15 ) ) , UnpackNormal( tex2D( _Tex, temp_output_12_0_g15 ) ) , ( abs( ( temp_output_27_0_g15 - 0.5 ) ) / 0.5 ));
			float3 temp_output_35_0 = lerpResult9_g15;
			float4 temp_output_19_0 = ( tex2DNode2 + tex2DNode15.a + float4( temp_output_35_0 , 0.0 ) );
			o.Albedo = temp_output_19_0.rgb;
			o.Emission = temp_output_19_0.rgb;
			o.Alpha = 1;
			float4 color3 = IsGammaSpace() ? float4(57.56495,34.91018,16.02044,0) : float4(7453.289,2480.245,446.9756,0);
			clip( ( tex2DNode2 * color3 ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
2120;73;773;526;1865.648;796.3442;3.618627;True;False
Node;AmplifyShaderEditor.RangedFloatNode;32;-892.0067,492.3705;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;2.64;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;-1115.736,-29.08151;Inherit;True;Property;_TextureSample5;Texture Sample 5;5;0;Create;True;0;0;0;False;0;False;-1;None;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;36;-728.7014,742.9481;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;35;-288.7141,592.9803;Inherit;True;Flow;1;;15;acad10cc8145e1f4eb8042bebe2d9a42;2,50,1,51,1;5;5;SAMPLER2D;;False;2;FLOAT2;0,0;False;18;FLOAT2;0,0;False;17;FLOAT2;1,1;False;24;FLOAT;0.2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;2;-515,-204;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;0;False;0;False;-1;fa0d8a286508a40c297fcede1722a6d2;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-518.3137,222.775;Inherit;True;Property;_TextureSample4;Texture Sample 4;4;0;Create;True;0;0;0;False;0;False;-1;None;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-470.4705,24.14782;Inherit;False;Constant;_Color0;Color 0;2;1;[HDR];Create;True;0;0;0;False;0;False;57.56495,34.91018,16.02044,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;22;-1270.212,292.4944;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;39.80001,-212.9999;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;26;-966.501,200.5988;Inherit;False;Whirl;-1;;16;7d75aee9e4d352a4299928ac98404afc;2,26,0,25,1;6;27;FLOAT2;0,0;False;1;FLOAT2;1,1;False;7;FLOAT2;0.5,0.5;False;16;FLOAT;8;False;21;FLOAT;2;False;10;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;31;-718.7448,405.1163;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-198.0676,334.7138;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-5.069835,223.0422;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;37;295.929,261.2742;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;404,-237;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SoulPaint/electricity;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;316;True;True;0;True;Transparent;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;35;2;25;0
WireConnection;35;18;36;0
WireConnection;35;17;32;0
WireConnection;35;24;32;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;26;21;22;1
WireConnection;31;0;25;0
WireConnection;31;1;32;0
WireConnection;33;0;15;4
WireConnection;33;1;35;0
WireConnection;19;0;2;0
WireConnection;19;1;15;4
WireConnection;19;2;35;0
WireConnection;0;0;19;0
WireConnection;0;2;19;0
WireConnection;0;10;4;0
ASEEND*/
//CHKSM=0B1FADDDE0716805178CDDE8CC71F1812DBEC912