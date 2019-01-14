Shader "Unlit/ScaleGrid"
{
	Properties
	{
        _Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			float4 vert (float4 v:POSITION) : SV_POSITION
			{
	            
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				return fixed4(1,1,1,1);
			}
			ENDCG
		}
	}
}
