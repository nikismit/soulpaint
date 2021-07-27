// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "translucent"
{
	Properties
	{
		_moving("moving", 2D) = "white" {}
		_noise("noise", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _noise;
		uniform sampler2D _moving;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (0.1).xx;
			float2 uv_TexCoord4 = i.uv_texcoord + float2( 1,1 );
			float2 panner38 = ( _Time.y * temp_cast_0 + uv_TexCoord4);
			float4 color40 = IsGammaSpace() ? float4(1,0.5423229,0,1) : float4(1,0.2553266,0,1);
			float2 temp_cast_1 = (0.2).xx;
			float2 panner5 = ( _Time.y * temp_cast_1 + uv_TexCoord4);
			float4 temp_output_41_0 = ( tex2D( _noise, panner38 ) * color40 * ( color40 * tex2D( _moving, ( panner5 * float2( 0,0 ) ) ) ) );
			o.Normal = temp_output_41_0.rgb;
			o.Albedo = temp_output_41_0.rgb;
			o.Emission = ( temp_output_41_0 * 1.0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
0;6;1920;1133;1211.219;703.6272;1.523503;True;True
Node;AmplifyShaderEditor.CommentaryNode;28;-1182.658,-630.7726;Inherit;False;770.1443;760.0441;PANNER;6;21;4;11;5;30;43;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;11;-1082.971,-460.7818;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;30;-971.6354,-165.4534;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-891.3146,-582.3729;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;43;-898.3645,-267.1711;Inherit;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;5;-620.7144,-492.9725;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-647.8596,452.6837;Inherit;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-48.76489,118.429;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;40;90.58573,524.2341;Inherit;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;0;False;0;False;1,0.5423229,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-35.86448,-275.3222;Inherit;True;Property;_moving;moving;1;0;Create;True;0;0;0;False;0;False;-1;00b54db44fb90497d8e51b542bb5d340;00b54db44fb90497d8e51b542bb5d340;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;38;-452.295,95.83038;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;309.0294,-143.5816;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-282.9709,435.2182;Inherit;True;Property;_noise;noise;2;0;Create;True;0;0;0;False;0;False;-1;fff70cc323e7244f0a6fed5ecb11a9a1;fff70cc323e7244f0a6fed5ecb11a9a1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;542.0309,366.3227;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;864.1393,361.1702;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;795.2343,627.9146;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;746.4349,-305.571;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;45;626.4349,-470.371;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;44;357.635,-398.371;Inherit;False;Property;_EMISSION;EMISSION;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0.7450981;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosTime;21;-1132.658,-53.72886;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1055.9,48.49993;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;translucent;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;1;11;0
WireConnection;5;0;4;0
WireConnection;5;2;43;0
WireConnection;5;1;30;0
WireConnection;42;0;5;0
WireConnection;6;1;42;0
WireConnection;38;0;4;0
WireConnection;38;2;39;0
WireConnection;38;1;30;0
WireConnection;16;0;40;0
WireConnection;16;1;6;0
WireConnection;12;1;38;0
WireConnection;41;0;12;0
WireConnection;41;1;40;0
WireConnection;41;2;16;0
WireConnection;47;0;41;0
WireConnection;47;1;17;0
WireConnection;46;0;44;0
WireConnection;46;1;45;2
WireConnection;46;2;17;0
WireConnection;0;0;41;0
WireConnection;0;1;41;0
WireConnection;0;2;47;0
ASEEND*/
//CHKSM=A8D56BD976DB6BAB6BDC1226DF9F98DB354BAB4F