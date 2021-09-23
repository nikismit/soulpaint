// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "electricNew"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.01
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		[HDR]_EmissiveColor("EmissiveColor", Color) = (6.556844,2.51736,2.51736,0.6156863)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		Blend One Zero , SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _EmissiveColor;
		uniform sampler2D _TextureSample1;
		uniform float _Cutoff = 0.01;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 _Vector0 = float2(1.1,1.3);
			float2 uv_TexCoord19 = i.uv_texcoord + _Vector0;
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles26 = _Vector0.x * _Vector0.y;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset26 = 1.0f / _Vector0.x;
			float fbrowsoffset26 = 1.0f / _Vector0.y;
			// Speed of animation
			float fbspeed26 = _Time.y * 6.0;
			// UV Tiling (col and row offset)
			float2 fbtiling26 = float2(fbcolsoffset26, fbrowsoffset26);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex26 = round( fmod( fbspeed26 + 0.0, fbtotaltiles26) );
			fbcurrenttileindex26 += ( fbcurrenttileindex26 < 0) ? fbtotaltiles26 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox26 = round ( fmod ( fbcurrenttileindex26, _Vector0.x ) );
			// Multiply Offset X by coloffset
			float fboffsetx26 = fblinearindextox26 * fbcolsoffset26;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy26 = round( fmod( ( fbcurrenttileindex26 - fblinearindextox26 ) / _Vector0.x, _Vector0.y ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy26 = (int)(_Vector0.y-1) - fblinearindextoy26;
			// Multiply Offset Y by rowoffset
			float fboffsety26 = fblinearindextoy26 * fbrowsoffset26;
			// UV Offset
			float2 fboffset26 = float2(fboffsetx26, fboffsety26);
			// Flipbook UV
			half2 fbuv26 = uv_TexCoord19 * fbtiling26 + fboffset26;
			// *** END Flipbook UV Animation vars ***
			float2 _Vector1 = float2(1,1.1);
			float2 uv_TexCoord36 = i.uv_texcoord + _Vector1;
			float fbtotaltiles33 = _Vector1.x * _Vector1.y;
			float fbcolsoffset33 = 1.0f / _Vector1.x;
			float fbrowsoffset33 = 1.0f / _Vector1.y;
			float fbspeed33 = _Time.y * 6.0;
			float2 fbtiling33 = float2(fbcolsoffset33, fbrowsoffset33);
			float fbcurrenttileindex33 = round( fmod( fbspeed33 + 4.0, fbtotaltiles33) );
			fbcurrenttileindex33 += ( fbcurrenttileindex33 < 0) ? fbtotaltiles33 : 0;
			float fblinearindextox33 = round ( fmod ( fbcurrenttileindex33, _Vector1.x ) );
			float fboffsetx33 = fblinearindextox33 * fbcolsoffset33;
			float fblinearindextoy33 = round( fmod( ( fbcurrenttileindex33 - fblinearindextox33 ) / _Vector1.x, _Vector1.y ) );
			fblinearindextoy33 = (int)(_Vector1.y-1) - fblinearindextoy33;
			float fboffsety33 = fblinearindextoy33 * fbrowsoffset33;
			float2 fboffset33 = float2(fboffsetx33, fboffsety33);
			half2 fbuv33 = uv_TexCoord36 * fbtiling33 + fboffset33;
			float4 temp_output_30_0 = ( tex2D( _TextureSample0, fbuv26 ) * _EmissiveColor * tex2D( _TextureSample1, fbuv33 ) );
			o.Albedo = temp_output_30_0.rgb;
			o.Emission = temp_output_30_0.rgb;
			o.Alpha = 1;
			clip( temp_output_30_0.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
616;1041;1920;989;1191.18;652.5214;1.6;True;False
Node;AmplifyShaderEditor.Vector2Node;32;-362.3401,629.5569;Inherit;False;Constant;_Vector1;Vector 1;2;0;Create;True;0;0;0;False;0;False;1,1.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;22;-577.8671,-222.5975;Inherit;True;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;0;False;0;False;1.1,1.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;24;-73.86707,-17.99756;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-316.8671,-175.9976;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-129.8671,-366.9976;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;29;-323.8671,241.0024;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-400.3399,517.5569;Inherit;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;0;False;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;35;-407.3399,934.5568;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-213.34,326.557;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-157.34,675.5569;Inherit;False;Constant;_Float3;Float 3;2;0;Create;True;0;0;0;False;0;False;6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;26;236.1329,-394.9976;Inherit;True;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;33;152.6598,298.557;Inherit;True;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;34;334.6599,599.5569;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;f9efe0b8007c598499e11d26621009f7;29627d1af34042841857c4f405be685b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;91.93288,-40.89756;Inherit;False;Property;_EmissiveColor;EmissiveColor;4;1;[HDR];Create;True;0;0;0;False;0;False;6.556844,2.51736,2.51736,0.6156863;5.037622,5.037622,5.037622,0.6156863;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;418.1329,-93.99756;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;-1;f9efe0b8007c598499e11d26621009f7;29627d1af34042841857c4f405be685b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;18.3615,979.96;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;42;74.36154,1328.96;Inherit;False;Constant;_Float5;Float 5;2;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;41;-175.6384,1587.959;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;46;379.5319,1071.177;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;570.633,166.6025;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;39;-243.6386,1191.96;Inherit;True;Constant;_Vector2;Vector 2;2;0;Create;True;0;0;0;False;0;False;0.001,0.001;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SinTimeNode;48;344.5088,1505.724;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;47;293.48,1363.753;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-168.6384,1170.96;Inherit;False;Constant;_Float4;Float 4;2;0;Create;True;0;0;0;False;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;45;566.3612,1252.96;Inherit;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;0;False;0;False;-1;f9efe0b8007c598499e11d26621009f7;f9efe0b8007c598499e11d26621009f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;943.3,-186.7;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;electricNew;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.01;True;True;0;True;Transparent;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;1;22;0
WireConnection;36;1;32;0
WireConnection;26;0;19;0
WireConnection;26;1;22;1
WireConnection;26;2;22;2
WireConnection;26;3;24;0
WireConnection;26;4;27;0
WireConnection;26;5;29;0
WireConnection;33;0;36;0
WireConnection;33;1;32;1
WireConnection;33;2;32;2
WireConnection;33;3;38;0
WireConnection;33;4;37;0
WireConnection;33;5;35;0
WireConnection;34;1;33;0
WireConnection;20;1;26;0
WireConnection;43;1;39;0
WireConnection;46;0;43;0
WireConnection;46;2;42;0
WireConnection;46;1;48;4
WireConnection;30;0;20;0
WireConnection;30;1;31;0
WireConnection;30;2;34;0
WireConnection;45;1;46;0
WireConnection;0;0;30;0
WireConnection;0;2;30;0
WireConnection;0;10;30;0
ASEEND*/
//CHKSM=0A2C4DF2F8238E380A968DBCC8177C6A976AF2B0