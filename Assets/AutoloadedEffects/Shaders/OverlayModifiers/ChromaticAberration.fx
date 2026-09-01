sampler baseTexture : register(s0);
sampler colorTexture : register(s1);

float splitIntensity;
float uOpacity;
float uTime;

// Creates a chromatic aberration effect by offsetting the red and blue channels to the left and right, respectively, while keeping the green channel centered (I hope).
// Lucille Karma is my goddess and savior.
float4 ChromaticAberration(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR
{
    float4 color = tex2D(baseTexture, coords);

    float splitDistance = 0.4; // Adjust the multiplier to control the intensity of the effect
    color.r = tex2D(baseTexture, coords + float2(-splitDistance, 0)).r; // Shift red channel left
    color.b = tex2D(baseTexture, coords + float2(splitDistance, 0)).b; // Shift blue channel right
    color.g = tex2D(baseTexture, coords).g; // Keep green channel centered
    // TODO: make the splitDistance essentially random (from certain values ofc) to make the effect look more "glitchy"

    return color;
}

technique Technique1
{
    pass ChromaticAberration
    {
        PixelShader = compile ps_2_0 ChromaticAberration();
    }
}