﻿Shader "Unlit/TextureInstancing"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TextureCellDim ("Texture Cell and Dimension", Vector) = (0,0,0,0)
		_TextureST ("Texture ST", Vector) = (1,1,0,0)
		//_TexturePadding ("Texture Padding", Vector) = (2,2)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Tags {"RenderType" = "Transparent"}//"LightMode" = "ForwardBase"
			Blend SrcAlpha OneMinusSrcAlpha
			//ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			// Enable gpu instancing variants.
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD01;
				UNITY_VERTEX_INPUT_INSTANCE_ID // Need this for basic functionality.
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD01;
				float3 normal : TEXCOORD02;
				float3 worldPos : TEXCOORD03;
				UNITY_VERTEX_INPUT_INSTANCE_ID // Need this to be able to get property in fragment shader.				
			};

			// Per instance properties must be declared in this block.
			UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _TextureCellDim)
                UNITY_DEFINE_INSTANCED_PROP(float4, _TextureST)
            UNITY_INSTANCING_BUFFER_END(Props)

			sampler2D _MainTex; float4 _MainTex_ST;

			v2f vert (appdata v)
			{
				v2f o;

				// Setup.
				UNITY_SETUP_INSTANCE_ID(v);
				// Transfer to fragment shader.
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Setup.
				UNITY_SETUP_INSTANCE_ID(i);

				//float3 normalDir = normalize(i.normal);
				//float3 lightDir = _WorldSpaceLightPos0;
				// Simple light interaction.
				//float3 diffuse = clamp(dot(normalDir, lightDir), 0.5, 1);
				//float2 padding = _TexturePadding;
				// Get per instance property values.
				float4 texCellDim = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureCellDim);
				float4 texST = UNITY_ACCESS_INSTANCED_PROP(Props, _TextureST);
				
				// Apply tiling and offset, and compute uv for cell specified. 
				float2 uv = (texCellDim.xy + frac(i.uv * texST.xy + texST.zw))/texCellDim.zw; 
				
				fixed4 texColor = tex2D(_MainTex, uv);
				//return fixed4(texColor * diffuse, 1);
				return texColor;
			}
			ENDCG
		}
	}
}
