sampler uImage0 : register(s0); // The base texture (the armor piece's texture)
sampler uImage1 : register(s1); // The noise texture (a procedural noise texture that will be used to create the noise effect on the armor)
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float2 uTargetPosition;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;

float4 main(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(uImage0, coords); // Sample the base texture
    color.rgb *= float3(1.0, 0.2, 0.0); // Fire color
    float2 noiseCoords = coords + float2(uTime * 0.5, uTime * 0.5); // Create animated noise coordinates
    float4 noise = tex2D(uImage1, noiseCoords); // Sample the noise

    return color * noise * sampleColor; // Modulate the sampled color with the input color
}
technique Technique1
{
    pass SomeBullshit
    {
        PixelShader = compile ps_2_0 main();
    }
}
