 Shader "Custom/FFT" {
 
    Properties{
        _FFTSize("FFTSize", Int) = 512
        _Height("Height", Range(1.0, 50.0)) = 10.0
    }
    
	SubShader {
	
	    Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }

        Cull Back
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
        	float _Height;
            
            StructuredBuffer<float> fftData;
            StructuredBuffer<uint> centroidData;

	        struct VSOut {
	            float4 pos : SV_POSITION;
	            float4 color : COLOR;
	        };

	       	VSOut vert (float4 vertex : POSITION, uint vid : SV_VertexID)
	       	{
	            VSOut output;
	            vertex.y = fftData[vid];	            
	            uint rippleId = vid / _FFTSize;
	            uint bin = vid % _FFTSize;
	            output.pos = vertex;
	            if(rippleId == bin)
	                output.color = float4(1, 0, 0, 1); 
                else
                    output.color = float4(0.2, 0.8, 1, 0.06);

	            return output;
	       	}
	       	
		   	[maxvertexcount(4)]
		   	void geom (point VSOut input[1], inout TriangleStream<VSOut> outStream)
		   	{
		     	VSOut output;
		      	float4 pos = input[0].pos; 
		      	float4 color = input[0].color;
                float4 left = float4(-pos.z * 0.005, pos.y, pos.x * 0.005, 0);	      	
		      	
				output.pos = pos + left;
				output.pos.y = pos.y * _Height ;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
			    output.color = color;
				outStream.Append (output);
			
				output.pos = pos - left;
				output.pos.y = pos.y  * _Height;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
			    output.color = color;
				outStream.Append (output);
				
				output.pos = pos + left;
				output.pos.y = -pos.y  * _Height;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
			    output.color = color;
				outStream.Append (output);
				
				output.pos = pos - left;
				output.pos.y = -pos.y * _Height;
			    output.pos = mul (UNITY_MATRIX_VP, output.pos);
			    output.color = color;
				outStream.Append (output);
				
		      	outStream.RestartStrip();
		   	}
			
	        fixed4 frag (VSOut input) : COLOR
	        {
	            return input.color;
	        }
	         
	        ENDCG
	     } 
     }
 }