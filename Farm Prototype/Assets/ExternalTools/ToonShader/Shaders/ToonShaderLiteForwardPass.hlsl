﻿#ifndef TOON_SHADER_LITE_FORWARD_PASS
#define TOON_SHADER_LITE_FORWARD_PASS

struct appdata
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
    float4 tangentOS : TANGENT;
    float2 uv : TEXCOORD0;

    #ifdef _VERTEX_COLOR
    half3 vertexColor : COLOR;
    #endif

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 positionCS : SV_POSITION;

    float fogFactor : TEXCOORD1;

    #ifdef _TOON_VERTEX_LIT
    half4 mainLightColorAndBrightness : TEXCOORD2;
    #else
	half3 normalWS : TEXCOORD2;
    #endif

    #ifdef _VERTEX_COLOR
    half3 vertexColor : COLOR;
    #endif

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

#include "./ToonShaderUtils.hlsl"

inline half4 get_main_light_color_and_brightness(in const float4 position_cs, in const half3 normal_ws)
{
    const Light main_light = GetMainLight(float4(0, 0, 0, 0));
    const half3 light_direction_ws = normalize(main_light.direction);
    const half main_light_attenuation = main_light.shadowAttenuation * main_light.distanceAttenuation;
    const half brightness = get_brightness(position_cs, normal_ws, light_direction_ws,
                                           main_light_attenuation);

    return half4(main_light.color, brightness);
}

v2f vert(appdata input)
{
    v2f output;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);

    const float4 basemap_st = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap_ST);
    output.uv = apply_tiling_offset(input.uv, basemap_st);

    const VertexPositionInputs vertex_position_inputs = GetVertexPositionInputs(input.positionOS.xyz);
    const VertexNormalInputs vertex_normal_inputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);


    float4 position_cs = vertex_position_inputs.positionCS;
    output.positionCS = position_cs;

    output.fogFactor = get_fog_factor(position_cs.z);

    #ifdef _TOON_VERTEX_LIT
    output.mainLightColorAndBrightness =
        get_main_light_color_and_brightness(position_cs, vertex_normal_inputs.normalWS);
    #else
	output.normalWS = vertex_normal_inputs.normalWS;
    #endif

    #ifdef _VERTEX_COLOR
    output.vertexColor = input.vertexColor;
    #endif

    return output;
}

half4 frag(const v2f input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);

    half4 base_color = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);
    #ifdef _VERTEX_COLOR
    base_color.xyz *= input.vertexColor;
    #endif
    half3 sample_color = (SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * base_color).rgb;


    #ifdef _TOON_VERTEX_LIT
    const half4 main_light_color_and_brightness = input.mainLightColorAndBrightness;
    #else
	const half4 main_light_color_and_brightness = get_main_light_color_and_brightness(input.positionCS, input.normalWS);
    #endif

    const half4 shadow_tint = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _ShadowTint);
    const half3 shadow_color = lerp(sample_color, shadow_tint.xyz, shadow_tint.a);
    half3 fragment_color = lerp(shadow_color, sample_color, main_light_color_and_brightness.w);


    fragment_color *= main_light_color_and_brightness.xyz;

    #ifdef _FOG
    fragment_color = MixFog(fragment_color, input.fogFactor);
    #endif

    return half4(max(fragment_color, 0), 1);
}

#endif
