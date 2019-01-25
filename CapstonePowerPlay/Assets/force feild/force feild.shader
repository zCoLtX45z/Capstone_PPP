// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32903,y:32564,varname:node_2865,prsc:2|emission-6390-OUT;n:type:ShaderForge.SFN_Tex2d,id:2833,x:31849,y:32734,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_2833,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a3899695b2341144a93ed85820c985cd,ntxv:0,isnm:False|UVIN-170-OUT;n:type:ShaderForge.SFN_Fresnel,id:3234,x:31849,y:32938,varname:node_3234,prsc:2|EXP-9416-OUT;n:type:ShaderForge.SFN_Multiply,id:7035,x:32167,y:32762,varname:node_7035,prsc:2|A-2833-RGB,B-3234-OUT;n:type:ShaderForge.SFN_Clamp01,id:2170,x:32316,y:32762,varname:node_2170,prsc:2|IN-7035-OUT;n:type:ShaderForge.SFN_Color,id:2671,x:31950,y:32597,ptovrint:False,ptlb:base color,ptin:_basecolor,varname:node_2671,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_SceneColor,id:7351,x:32200,y:32166,varname:node_7351,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:9553,x:32414,y:32223,varname:node_9553,prsc:2|IN-7351-RGB;n:type:ShaderForge.SFN_Lerp,id:6390,x:32464,y:32552,varname:node_6390,prsc:2|A-9553-OUT,B-4056-OUT,T-2170-OUT;n:type:ShaderForge.SFN_Panner,id:4435,x:31456,y:32534,varname:node_4435,prsc:2,spu:0,spv:0.3|UVIN-2941-UVOUT,DIST-1966-TSL;n:type:ShaderForge.SFN_Panner,id:29,x:31456,y:32701,varname:node_29,prsc:2,spu:0.3,spv:0|UVIN-2941-UVOUT,DIST-4065-OUT;n:type:ShaderForge.SFN_TexCoord,id:2941,x:30928,y:32461,varname:node_2941,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:1966,x:30960,y:32688,varname:node_1966,prsc:2;n:type:ShaderForge.SFN_Vector1,id:9416,x:31662,y:33006,varname:node_9416,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:170,x:31631,y:32620,varname:node_170,prsc:2|A-4435-UVOUT,B-29-UVOUT;n:type:ShaderForge.SFN_Color,id:4222,x:31962,y:32429,ptovrint:False,ptlb:base color_copy,ptin:_basecolor_copy,varname:_basecolor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.08357121,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:4056,x:32150,y:32547,varname:node_4056,prsc:2|A-4222-RGB,B-2671-RGB,C-5650-RGB;n:type:ShaderForge.SFN_Color,id:5650,x:31927,y:32245,ptovrint:False,ptlb:base color_copy_copy,ptin:_basecolor_copy_copy,varname:_basecolor_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Transform,id:1551,x:32089,y:32958,varname:node_1551,prsc:2,tffrom:0,tfto:1;n:type:ShaderForge.SFN_Sin,id:4065,x:31158,y:32688,varname:node_4065,prsc:2|IN-1966-TSL;proporder:2833-2671-4222-5650;pass:END;sub:END;*/

Shader "Shader Forge/force feild" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _Color ("base color", Color) = (0,1,1,1)
        _basecolor_copy ("base color_copy", Color) = (0,0.08357121,1,1)
        _basecolor_copy_copy ("base color_copy_copy", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
			Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float4 _Color;
            uniform float4 _basecolor_copy;
            uniform float4 _basecolor_copy_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 node_1966 = _Time;
                float2 node_170 = ((i.uv0+node_1966.r*float2(0,0.3))+(i.uv0+sin(node_1966.r)*float2(0.3,0)));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_170, _texture));
                float3 emissive = lerp(saturate(sceneColor.rgb),(_basecolor_copy.rgb*_Color.rgb*_basecolor_copy_copy.rgb),saturate((_texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),0.5))));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;


            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float4 _Color;
            uniform float4 _basecolor_copy;
            uniform float4 _basecolor_copy_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_1966 = _Time;
                float2 node_170 = ((i.uv0+node_1966.r*float2(0,0.3))+(i.uv0+sin(node_1966.r)*float2(0.3,0)));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_170, _texture));
                o.Emission = lerp(saturate(sceneColor.rgb),(_basecolor_copy.rgb*_Color.rgb*_basecolor_copy_copy.rgb),saturate((_texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),0.5))));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
