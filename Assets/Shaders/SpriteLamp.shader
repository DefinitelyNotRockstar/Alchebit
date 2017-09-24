// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader for Unity integration with SpriteLamp
// Copyright (c) 2014 Steve Karolewics & Indreams Studios
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

Shader "Custom/SpriteLamp"
{
    Properties
    {
        _MainTex ("Diffuse Texture", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Depth ("Depth", 2D) = "gray" {}
        _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
        _Shininess ("Shininess", Float) = 10
        _AmplifyDepth ("Amplify Depth", Float) = 1
        _CelShadingLevels ("Cel Shading Levels", Float) = 0
    }

    SubShader
    {
        AlphaTest NotEqual 0.0
        Pass
        {    
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag 

            #include "UnityCG.cginc"

            // User-specified properties
            uniform sampler2D _MainTex;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float4 uv : TEXCOORD0;    
            };

            struct VertexOutput
            {
                float4 pos : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            VertexOutput vert(VertexInput input) 
            {
                VertexOutput output;

                output.pos = UnityObjectToClipPos(input.vertex);
                output.color = input.color;
                output.uv = input.uv;
                return output;
            }

            float4 frag(VertexOutput input) : COLOR
            {
                float4 diffuseColor = tex2D(_MainTex, input.uv);


                float3 ambientFactor = UNITY_LIGHTMODEL_AMBIENT.xyz;
                float3 diffuseFactor = diffuseColor.xyz;
                float3 inputFactor = input.color.xyz;

                float3 ambientLighting = ambientFactor * diffuseFactor * inputFactor;
                return float4(ambientLighting, diffuseColor.a);
            }

            ENDCG
        }

        Pass
        {    
            Tags { "LightMode" = "ForwardAdd" }
            Blend One One // additive blending 

            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag 

            #include "UnityCG.cginc"

            // User-specified properties
            uniform sampler2D _MainTex;
            uniform sampler2D _Normal;
            uniform sampler2D _Depth;
            uniform float4 _SpecColor; 
            uniform float4 _LightColor0;
            uniform float _Shininess;
            uniform float _AmplifyDepth;
            uniform float _CelShadingLevels;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float4 uv : TEXCOORD0;
            };

            struct VertexOutput
            {
                float4 pos : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };

            VertexOutput vert(VertexInput input)
            {
                VertexOutput output;

                output.pos = UnityObjectToClipPos(input.vertex);
                output.posWorld = mul(unity_ObjectToWorld, input.vertex);

                output.uv = input.uv;
                output.color = input.color;
                return output;
            }

            float4 frag(VertexOutput input) : COLOR
            {
                float4 diffuseColor = tex2D(_MainTex, input.uv);

                // To compute the correct normal: 
                //   1) Get the pixel value from the normal map
                //   2) Subtract 0.5 and multiply by 2 to convert from the range 0...1 to -1...1
                //   3) Multiply by world to object matrix, to handle rotation, etc
                //   4) Negate Z so that lighting works as expected (sprites further away from the camera than
                //      a light are lit, etc.)
                //   5) Normalize
                float3 normalDirection = (tex2D(_Normal, input.uv).xyz - 0.5f) * 2.0f;
                normalDirection = (mul(float4(normalDirection, 1.0f), unity_WorldToObject));
                normalDirection.z *= -1;
                normalDirection = normalize(normalDirection);

                // To adjust depth:
                //   1) Get the depth value from the depth map
                //   2) Subtract 0.5 and multiply by 2 to convert from the range 0...1 to -1...1
                //   3) Multiply by the amplify depth value, and subtract from the fragment's z position
                float depthColor = (tex2D(_Depth, input.uv).x - 0.5f) * 2.0f;
                float3 posWorld = (input.posWorld);
                posWorld.z -= depthColor * _AmplifyDepth;
                float3 vertexToLightSource = (_WorldSpaceLightPos0) - posWorld;
                float distance = length(vertexToLightSource);

                // The values for attenuation and lightDirection are assuming point lights
                float attenuation = 1.0 / distance; // Linear attenuation is good enough for now
                float3 lightDirection = normalize(vertexToLightSource);

                // Compute diffuse part of lighting
                float normalDotLight = dot(normalDirection, lightDirection);
                float diffuseLevel = attenuation * max(0.0f, normalDotLight);

                // Compute specular part of lighting
                float specularLevel;
                if (normalDotLight < 0.0f)
                {
                    // Light is on the wrong side, no specular reflection
                    specularLevel = 0.0f;
                }
                else
                {
                    // For orthographic cameras, the view direction is always known
                    float3 viewDirection = float3(0.0f, 0.0f, -1.0f);
                    specularLevel = attenuation * pow(max(0.0, dot(reflect(-lightDirection, normalDirection),
                        viewDirection)), _Shininess);
                }

                // Add cel-shading if enough levels were specified
                if (_CelShadingLevels >= 2)
                {
                    diffuseLevel = floor(diffuseLevel * _CelShadingLevels) / (_CelShadingLevels - 0.5f);
                    specularLevel = floor(specularLevel * _CelShadingLevels) / (_CelShadingLevels - 0.5f);
                }

                float3 diffuseReflection = (diffuseColor) * input.color *
                    (_LightColor0) * diffuseLevel;
                float3 specularReflection = (_LightColor0) * (_SpecColor) *
                    input.color * specularLevel;
                return float4(diffuseReflection + specularReflection, diffuseColor.a);
             }

             ENDCG
        }
    }
    // The definition of a fallback shader should be commented out 
    // during development:
    // Fallback "Transparent/Diffuse"
}
