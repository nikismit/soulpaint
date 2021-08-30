// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Gradient 3colorNEW"
{
	Properties
	{
		_Color1("Color 1", Color) = (1,0.6882353,0.2205882,0)
		_Color2("Color 2", Color) = (1,0.4941176,0.945098,0)
		_Color3("Color 3", Color) = (0.4352941,1,0.9529412,0)
		_Distribution1("Distribution 1", Range( -1 , 1)) = 0.2
		_Distribution2("Distribution 2", Range( -1 , 1)) = 0.2
		_StartPoint2("Start Point 2", Range( -1 , 1)) = 0.1
		_StartPoint1("Start Point 1", Range( -1 , 1)) = -0.3069279
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color3;
		uniform float4 _Color2;
		uniform float _StartPoint1;
		uniform float _Distribution1;
		uniform float4 _Color1;
		uniform float _StartPoint2;
		uniform float _Distribution2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult7 = lerp( _Color3 , _Color2 , saturate( ( ( ( i.uv_texcoord.y + _StartPoint1 ) + ( _Distribution1 / 2.0 ) ) / _Distribution1 ) ));
			float4 lerpResult18 = lerp( lerpResult7 , _Color1 , saturate( ( ( ( i.uv_texcoord.y + _StartPoint2 ) + ( _Distribution2 / 2.0 ) ) / _Distribution2 ) ));
			o.Emission = lerpResult18.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
375;180;1135;809;1817.957;619.1952;1.6;True;False
Node;AmplifyShaderEditor.RangedFloatNode;27;-1019.556,35.22836;Float;False;Property;_StartPoint1;Start Point 1;6;0;Create;True;0;0;0;False;0;False;-0.3069279;-0.06;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1098.235,127.3293;Float;False;Property;_Distribution1;Distribution 1;3;0;Create;True;0;0;0;False;0;False;0.2;0.2;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-1145.016,-302.1259;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;33;-771.5364,419.135;Float;False;Property;_StartPoint2;Start Point 2;5;0;Create;True;0;0;0;False;0;False;0.1;-0.45;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-768.4154,511.5366;Float;False;Property;_Distribution2;Distribution 2;4;0;Create;True;0;0;0;False;0;False;0.2;0.06;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;29;-705.673,-7.545001;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-737.0472,-127.4228;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-1118.968,288.3172;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-545.0729,-127.845;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;36;-351.158,382.7484;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-483.2272,256.7838;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;37;-577.0244,108.7569;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-285.0735,259.5548;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;39;-423.8594,-5.114507;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-822.6608,-506.2414;Float;False;Property;_Color2;Color 2;1;0;Create;True;0;0;0;False;0;False;1,0.4941176,0.945098,0;0,1,0.1375736,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;40;-321.9048,491.6632;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-823.4964,-663.7341;Float;False;Property;_Color3;Color 3;2;0;Create;True;0;0;0;False;0;False;0.4352941,1,0.9529412,0;0.03579378,0,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;-822.7049,-348.2635;Float;False;Property;_Color1;Color 1;0;0;Create;True;0;0;0;False;0;False;1,0.6882353,0.2205882,0;1,0.2196077,0.2350503,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;-379.9096,-635.7334;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;41;-170.0392,379.0921;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;18;-132.3355,-413.5639;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PosVertexDataNode;31;-1321.121,285.7737;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;28;-1247.724,-157.4768;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;129.9141,-383.3432;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Gradient 3colorNEW;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;29;0;26;0
WireConnection;32;0;43;2
WireConnection;32;1;27;0
WireConnection;34;0;32;0
WireConnection;34;1;29;0
WireConnection;36;0;30;0
WireConnection;35;0;42;2
WireConnection;35;1;33;0
WireConnection;37;0;34;0
WireConnection;37;1;26;0
WireConnection;38;0;35;0
WireConnection;38;1;36;0
WireConnection;39;0;37;0
WireConnection;40;0;38;0
WireConnection;40;1;30;0
WireConnection;7;0;8;0
WireConnection;7;1;9;0
WireConnection;7;2;39;0
WireConnection;41;0;40;0
WireConnection;18;0;7;0
WireConnection;18;1;19;0
WireConnection;18;2;41;0
WireConnection;0;2;18;0
ASEEND*/
//CHKSM=AF5BCCE44EEA651298308E3FD44383FD1B0BDAFD