//Copyright (c) 2014 Kyle Halladay
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.


Shader "FresnelPack/Transparent Rim Unlit" 
{
	Properties 
	{
		_InnerRimColor("Inner Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_OuterRimColor("Outer Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Scale("Fresnel Scale", Range(0.0, 1.0)) = 1.0
	}
	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

		Pass
		{ 
   			ZWrite On
   			Blend SrcAlpha OneMinusSrcAlpha 

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
            #include "AutoLight.cginc"

			uniform float4 _Tint;
			uniform float4 _OuterRimColor;
			uniform float4 _InnerRimColor;
			uniform float _Scale;

			uniform float4 _LightColor0;

			struct vIN
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vOUT
			{
				float4 pos : SV_POSITION;
				float3 posWorld : TEXCOORD0;
				float3 normWorld : TEXCOORD1;
				LIGHTING_COORDS(3,4)
				float3 lightDir : TEXCOORD5;
				float3 vertexLighting : COLOR;
				
			};

			vOUT vert(vIN v)
			{
				vOUT o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				o.posWorld = mul(_Object2World, v.vertex);
				o.normWorld = normalize(mul( (float3x3)_Object2World, v.normal));
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				o.lightDir = ObjSpaceLightDir(v.vertex);

				return o;
			}

			float4 frag(vOUT i) : COLOR
			{
				float3 lightDir = i.lightDir;
				fixed atten = LIGHT_ATTENUATION(i);
				if ( _WorldSpaceLightPos0.w == 0.0)
				{
					atten = 1.0;
				 	lightDir = _WorldSpaceLightPos0.xyz;
				}
				
				
				float3 diff = (atten*2.0) * max(0.1, dot(lightDir, i.normWorld));

				float3 I = normalize(i.posWorld - _WorldSpaceCameraPos.xyz);

				float refFactor = max(-1.0, min(1.0,_Scale * pow(1.0 + dot(I, i.normWorld), 1.4)));
				float4 col =  lerp(_InnerRimColor,_OuterRimColor,refFactor);
				return col * (float4(diff + i.vertexLighting,0.0)+ UNITY_LIGHTMODEL_AMBIENT*1.0); 
			}

			ENDCG
		}
	}

	FallBack "Diffuse"
}
