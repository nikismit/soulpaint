// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FresnelNEW"
{
	Properties
	{
		_Color("Color", Color) = (0,0,0,0)
		_Glowcolor1("Glow color 1", Color) = (0,0,0,0)
		_Power1("Power 1", Range( -3 , 6)) = 2
		_SpecColor("Specular Color",Color)=(1,1,1,1)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf BlinnPhong keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _Color;
		uniform float4 _Glowcolor1;
		uniform float _Power1;

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV6 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode6 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV6, _Power1 ) );
			o.Emission = ( _Color + ( _Glowcolor1 * fresnelNode6 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
2253;400.6;1535;804;2083.248;569.3248;2.103687;True;False
Node;AmplifyShaderEditor.RangedFloatNode;7;-1023.176,287.1555;Float;False;Property;_Power1;Power 1;2;0;Create;True;0;0;0;False;0;False;2;2;-3;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;6;-766.5734,191.4335;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-767.7732,1.133362;Float;False;Property;_Glowcolor1;Glow color 1;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-510.0735,43.13342;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;3;-510.5732,-126.2666;Float;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.7830188,0.7645514,0.7645514,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;8;-1022.475,127.8328;Inherit;False;PerturbNormal;-1;;10;c8b64dd82fb09f542943a895dffb6c06;1,26,0;1;6;FLOAT3;0,0,0;False;4;FLOAT3;9;FLOAT;28;FLOAT;29;FLOAT;30
Node;AmplifyShaderEditor.SimpleAddOpNode;2;-256.6737,-0.3666401;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;BlinnPhong;FresnelNEW;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;3;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;3;7;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;2;0;3;0
WireConnection;2;1;4;0
WireConnection;0;2;2;0
ASEEND*/
//CHKSM=00E000C577AB584E4D1D480CA79E3B92A43CC385