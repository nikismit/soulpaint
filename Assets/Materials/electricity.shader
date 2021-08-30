// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SoulPaint/electricity"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.06
		[HDR]_TextureSample4("Texture Sample 4", 2D) = "white" {}
		[HDR]_color("color", Color) = (0.972549,0.4090836,0.1411764,0)
		_TextureSample6("Texture Sample 6", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample4;
		uniform float4 _color;
		uniform sampler2D _TextureSample6;
		uniform float _Cutoff = 0.06;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner49 = ( _Time.y * float2( 0,1 ) + i.uv_texcoord);
			float4 temp_output_41_0 = ( tex2D( _TextureSample4, panner49 ) * _color * tex2D( _TextureSample6, panner49 ) );
			o.Albedo = temp_output_41_0.rgb;
			o.Emission = temp_output_41_0.rgb;
			o.Alpha = 1;
			clip( temp_output_41_0.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
0;0;2048;1091;2619.427;830.5973;1.9;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;55;-1278.337,711.7845;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;36;-1286.432,565.2121;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-1219.045,874.493;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;49;-922.5352,139.1565;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;15;-447.2816,264.4395;Inherit;True;Property;_TextureSample4;Texture Sample 4;1;1;[HDR];Create;True;0;0;0;False;0;False;-1;None;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-644.4608,920.2351;Inherit;True;Property;_TextureSample6;Texture Sample 6;3;0;Create;True;0;0;0;False;0;False;-1;None;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;42;-396.0308,507.6989;Inherit;False;Property;_color;color;2;1;[HDR];Create;True;0;0;0;False;0;False;0.972549,0.4090836,0.1411764,0;95.87451,95.87451,95.87451,0.5450981;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;0.2235734,221.3617;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;404,-238.6905;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SoulPaint/electricity;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.06;True;True;0;True;Transparent;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;49;0;54;0
WireConnection;49;2;36;0
WireConnection;49;1;55;0
WireConnection;15;1;49;0
WireConnection;52;1;49;0
WireConnection;41;0;15;0
WireConnection;41;1;42;0
WireConnection;41;2;52;0
WireConnection;0;0;41;0
WireConnection;0;2;41;0
WireConnection;0;10;41;0
ASEEND*/
//CHKSM=7842D2985CF6FD1427DACD5FE340399E98513FF7