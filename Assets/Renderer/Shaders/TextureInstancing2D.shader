Shader "Unlit/TextureInstancing2D"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TextureCellDim ("Texture Cell and Dimension", Vector) = (0,0,0,0)
		_TextureST ("Texture ST", Vector) = (1,1,0,0)
	}
	SubShader
	{
		Tags {
			"RenderType"="Opaque" 
			"Queue" = "Transparent+1" 
		}
		LOD 100

		Pass
		{
			Tags {
				"Queue"="Transparent" 
				"IgnoreProjector"="True" 
				"RenderType"="Transparent" 
				"PreviewType"="Plane"
				"CanUseSpriteAtlas"="True"
			}//"LightMode" = "ForwardBase"
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Off
     		Lighting Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			// Enable gpu instancing variants.
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			struct Vertex
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD00;
				float2 uv2 : TEXCOORD01;
				UNITY_VERTEX_INPUT_INSTANCE_ID // Need this for basic functionality.
			};

			struct Fragment
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD00;
				float2 uv2 : TEXCOORD01;
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

			Fragment vert (Vertex v)
			{
				Fragment o;

				// Setup.
				UNITY_SETUP_INSTANCE_ID(v);
				// Transfer to fragment shader.
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv2 = v.uv2;
				return o;
			}
			
			fixed4 frag (Fragment i) : SV_Target
			{
				// Setup.
				UNITY_SETUP_INSTANCE_ID(i);

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
