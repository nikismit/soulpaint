// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Gradient 2color"
{
	Properties
	{
		_color("color", Color) = (0,1,0.213793,0)
		_Color2("Color 2", Color) = (1,0,0,0)
		_Distribution("Distribution", Range( 0 , 1)) = 0.2
		_StartPoint("Start Point", Range( -1 , 1)) = 0.1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma exclude_renderers xboxseries playstation switch nomrt 
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color2;
		uniform float4 _color;
		uniform float _StartPoint;
		uniform float _Distribution;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult7 = lerp( _Color2 , _color , saturate( ( ( ( i.uv_texcoord.y + _StartPoint ) + ( _Distribution / 2.0 ) ) / _Distribution ) ));
			o.Albedo = lerpResult7.rgb;
			o.Emission = lerpResult7.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
2253;400.6;1535;804;1384.646;569.2889;1.066159;True;False
Node;AmplifyShaderEditor.RangedFloatNode;14;-1021.687,127.5646;Float;False;Property;_Distribution;Distribution;2;0;Create;True;0;0;0;False;0;False;0.2;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1019.008,35.46365;Float;False;Property;_StartPoint;Start Point;3;0;Create;True;0;0;0;False;0;False;0.1;-0.374;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-1085.491,-153.7254;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;18;-509.5872,4.909698;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-710.9615,-131.9681;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-415.9871,-126.3903;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;21;-447.9386,110.2116;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-513.6301,-511.5334;Float;False;Property;_Color2;Color 2;1;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,1,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;22;-294.7735,-3.659806;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-511.2159,-349.1476;Float;False;Property;_color;color;0;0;Create;True;0;0;0;False;0;False;0,1,0.213793,0;0.6603774,0.6603774,0.6603774,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;-203.2009,-387.0247;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;129.9141,-383.3432;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Gradient 2color;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;vulkan;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;18;0;14;0
WireConnection;19;0;25;2
WireConnection;19;1;13;0
WireConnection;20;0;19;0
WireConnection;20;1;18;0
WireConnection;21;0;20;0
WireConnection;21;1;14;0
WireConnection;22;0;21;0
WireConnection;7;0;8;0
WireConnection;7;1;9;0
WireConnection;7;2;22;0
WireConnection;24;0;7;0
WireConnection;24;2;7;0
ASEEND*/
//CHKSM=F1ACADFDF3F9E72D1AE1DA390E483F2E4BD36FC1