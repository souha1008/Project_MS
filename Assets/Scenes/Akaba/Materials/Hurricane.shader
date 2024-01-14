// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32975,y:32703,varname:node_2865,prsc:2|emission-5133-OUT,clip-8147-OUT,voffset-2322-OUT;n:type:ShaderForge.SFN_Tex2d,id:5354,x:32146,y:32985,ptovrint:False,ptlb:NoiseTex,ptin:_NoiseTex,varname:node_5354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-5819-OUT;n:type:ShaderForge.SFN_Panner,id:9792,x:31757,y:32822,varname:node_9792,prsc:2,spu:-1,spv:0|UVIN-737-OUT,DIST-9473-OUT;n:type:ShaderForge.SFN_TexCoord,id:4229,x:31208,y:32689,varname:node_4229,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:6304,x:31341,y:32874,ptovrint:False,ptlb:ScrewSpeedX,ptin:_ScrewSpeedX,varname:node_6304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9473,x:31610,y:32893,varname:node_9473,prsc:2|A-6304-OUT,B-600-T;n:type:ShaderForge.SFN_Time,id:600,x:31341,y:32960,varname:node_600,prsc:2;n:type:ShaderForge.SFN_Panner,id:4517,x:31757,y:33033,varname:node_4517,prsc:2,spu:0,spv:-1|UVIN-9338-OUT,DIST-9235-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7046,x:31341,y:33129,ptovrint:False,ptlb:ScrewSpeedY,ptin:_ScrewSpeedY,varname:_SpeedX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Set,id:9106,x:31407,y:32689,varname:UV0,prsc:2|IN-4229-UVOUT;n:type:ShaderForge.SFN_Get,id:737,x:31576,y:32822,varname:node_737,prsc:2|IN-9106-OUT;n:type:ShaderForge.SFN_Get,id:9338,x:31576,y:33033,varname:node_9338,prsc:2|IN-9106-OUT;n:type:ShaderForge.SFN_Add,id:5819,x:31916,y:32856,varname:node_5819,prsc:2|A-9792-UVOUT,B-4517-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9235,x:31543,y:33105,varname:node_9235,prsc:2|A-600-T,B-7046-OUT;n:type:ShaderForge.SFN_Color,id:9423,x:32146,y:32819,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_9423,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.7535872,c3:0.3443396,c4:1;n:type:ShaderForge.SFN_Multiply,id:5112,x:32337,y:32792,varname:node_5112,prsc:2|A-9423-RGB,B-5354-RGB;n:type:ShaderForge.SFN_Fresnel,id:1011,x:31963,y:32630,varname:node_1011,prsc:2|EXP-9211-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9211,x:31735,y:32664,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_9211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:1553,x:31963,y:32485,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:node_1553,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9798835,c2:1,c3:0.6745283,c4:1;n:type:ShaderForge.SFN_Multiply,id:640,x:32168,y:32604,varname:node_640,prsc:2|A-1553-RGB,B-1011-OUT;n:type:ShaderForge.SFN_Add,id:5133,x:32763,y:32787,varname:node_5133,prsc:2|A-640-OUT,B-5779-OUT;n:type:ShaderForge.SFN_Multiply,id:2322,x:32452,y:33182,varname:node_2322,prsc:2|A-1191-RGB,B-7844-OUT,C-8995-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8995,x:32257,y:33266,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_8995,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_NormalVector,id:7844,x:32131,y:33190,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:1191,x:31965,y:33085,ptovrint:False,ptlb:NoiseTex_copy,ptin:_NoiseTex_copy,varname:_NoiseTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-5819-OUT;n:type:ShaderForge.SFN_Color,id:6850,x:32337,y:32625,ptovrint:False,ptlb:EmissionColor,ptin:_EmissionColor,varname:node_6850,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7169812,c2:0.5006413,c3:0.1927732,c4:1;n:type:ShaderForge.SFN_Add,id:5779,x:32565,y:32839,varname:node_5779,prsc:2|A-5112-OUT,B-6850-RGB;n:type:ShaderForge.SFN_Step,id:8147,x:32680,y:33030,varname:node_8147,prsc:2|A-278-OUT,B-2816-OUT;n:type:ShaderForge.SFN_Slider,id:2816,x:32413,y:33344,ptovrint:False,ptlb:DissolveValue,ptin:_DissolveValue,varname:node_2816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7383908,max:1;n:type:ShaderForge.SFN_TexCoord,id:4819,x:31685,y:33308,varname:node_4819,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:278,x:32414,y:33019,varname:node_278,prsc:2|A-5354-B,B-3869-OUT;n:type:ShaderForge.SFN_Slider,id:3393,x:31771,y:33729,ptovrint:False,ptlb:GroundDissolve,ptin:_GroundDissolve,varname:node_3393,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_RemapRange,id:4459,x:31875,y:33301,varname:node_4459,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4819-V;n:type:ShaderForge.SFN_Length,id:8377,x:32038,y:33476,varname:node_8377,prsc:2|IN-4459-OUT;n:type:ShaderForge.SFN_Power,id:3869,x:32212,y:33476,varname:node_3869,prsc:2|VAL-8377-OUT,EXP-3393-OUT;proporder:5354-6304-7046-9423-9211-1553-8995-1191-6850-2816-3393;pass:END;sub:END;*/

Shader "Shader Forge/Hurricane" {
    Properties {
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _ScrewSpeedX ("ScrewSpeedX", Float ) = 1
        _ScrewSpeedY ("ScrewSpeedY", Float ) = 1
        _Color ("Color", Color) = (1,0.7535872,0.3443396,1)
        _FresnelPower ("FresnelPower", Float ) = 2
        _FresnelColor ("FresnelColor", Color) = (0.9798835,1,0.6745283,1)
        _Offset ("Offset", Float ) = 0.1
        _NoiseTex_copy ("NoiseTex_copy", 2D) = "white" {}
        _EmissionColor ("EmissionColor", Color) = (0.7169812,0.5006413,0.1927732,1)
        _DissolveValue ("DissolveValue", Range(0, 1)) = 0.7383908
        _GroundDissolve ("GroundDissolve", Range(0, 10)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform float _ScrewSpeedX;
            uniform float _ScrewSpeedY;
            uniform float4 _Color;
            uniform float _FresnelPower;
            uniform float4 _FresnelColor;
            uniform float _Offset;
            uniform sampler2D _NoiseTex_copy; uniform float4 _NoiseTex_copy_ST;
            uniform float4 _EmissionColor;
            uniform float _DissolveValue;
            uniform float _GroundDissolve;
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
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_600 = _Time;
                float2 UV0 = o.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_copy_var = tex2Dlod(_NoiseTex_copy,float4(TRANSFORM_TEX(node_5819, _NoiseTex_copy),0.0,0));
                v.vertex.xyz += (_NoiseTex_copy_var.rgb*v.normal*_Offset);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_600 = _Time;
                float2 UV0 = i.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(node_5819, _NoiseTex));
                clip(step((_NoiseTex_var.b+pow(length((i.uv0.g*1.0+-0.5)),_GroundDissolve)),_DissolveValue) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = ((_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower))+((_Color.rgb*_NoiseTex_var.rgb)+_EmissionColor.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform float _ScrewSpeedX;
            uniform float _ScrewSpeedY;
            uniform float _Offset;
            uniform sampler2D _NoiseTex_copy; uniform float4 _NoiseTex_copy_ST;
            uniform float _DissolveValue;
            uniform float _GroundDissolve;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_600 = _Time;
                float2 UV0 = o.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_copy_var = tex2Dlod(_NoiseTex_copy,float4(TRANSFORM_TEX(node_5819, _NoiseTex_copy),0.0,0));
                v.vertex.xyz += (_NoiseTex_copy_var.rgb*v.normal*_Offset);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_600 = _Time;
                float2 UV0 = i.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(node_5819, _NoiseTex));
                clip(step((_NoiseTex_var.b+pow(length((i.uv0.g*1.0+-0.5)),_GroundDissolve)),_DissolveValue) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
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
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform float _ScrewSpeedX;
            uniform float _ScrewSpeedY;
            uniform float4 _Color;
            uniform float _FresnelPower;
            uniform float4 _FresnelColor;
            uniform float _Offset;
            uniform sampler2D _NoiseTex_copy; uniform float4 _NoiseTex_copy_ST;
            uniform float4 _EmissionColor;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_600 = _Time;
                float2 UV0 = o.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_copy_var = tex2Dlod(_NoiseTex_copy,float4(TRANSFORM_TEX(node_5819, _NoiseTex_copy),0.0,0));
                v.vertex.xyz += (_NoiseTex_copy_var.rgb*v.normal*_Offset);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_600 = _Time;
                float2 UV0 = i.uv0;
                float2 node_5819 = ((UV0+(_ScrewSpeedX*node_600.g)*float2(-1,0))+(UV0+(node_600.g*_ScrewSpeedY)*float2(0,-1)));
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(node_5819, _NoiseTex));
                o.Emission = ((_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower))+((_Color.rgb*_NoiseTex_var.rgb)+_EmissionColor.rgb));
                
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
