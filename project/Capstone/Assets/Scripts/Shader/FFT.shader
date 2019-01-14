 Shader "Custom/FFT" {
 
    Properties{
        _FFTSize("FFTSize", Int) = 512
        _Offset("Offset", Range(0.0, 100.0)) = 15.0
        _Distance("Distance", Range(0.0, 100.0)) = 10.0
    }
    
	SubShader {
	    Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        ZWrite Off

	    Blend SrcAlpha OneMinusSrcAlpha

        Pass {
	        CGPROGRAM
	        
	        #pragma target 5.0
	        #pragma vertex vert
			#pragma geometry geom
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	        
	        int _FFTSize;
            float _Offset;
            float _Distance;
        
	        struct VSOut {
	            float4 pos : SV_POSITION;
	        };

	       	VSOut vert (float4 vertex : POSITION, uint vid : SV_VertexID)
	       	{
	            VSOut output;
	            int circle = (int)vid / _FFTSize;
	            float xPos = vertex.x * (_Offset + circle * _Distance);
	            float zPos = vertex.z * (_Offset + circle * _Distance);
	            output.pos = float4(xPos, vertex.y, zPos, vertex.w);
	            return output;
	       	}
	       	
		   	[maxvertexcount(4)]
		   	void geom (point VSOut input[1], inout TriangleStream<VSOut> outStream)
		   	{
		     	VSOut output;
		      	float4 pos = input[0].pos; 
                float4 left = float4(-pos.z * 0.005, pos.y, pos.x * 0.005, 0);	      	
		      	
				output.pos = pos + left;
				output.pos.y = 5;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
				outStream.Append (output);
			
				output.pos = pos - left;
				output.pos.y = 5;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
				outStream.Append (output);
				
				output.pos = pos + left;
				output.pos.y = -5;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
				outStream.Append (output);
				
				output.pos = pos - left;
				output.pos.y = -5;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
				outStream.Append (output);
				
		      	outStream.RestartStrip();
		   	}
			
	        fixed4 frag (VSOut i) : COLOR
	        {
	            return float4(0, 0, 1, 0.3);
	        }
	         
	        ENDCG
	     } 
     }
 }