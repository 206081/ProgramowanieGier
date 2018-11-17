#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler s0;

uniform extern texture ScreenTexture;
sampler screen = sampler_state
{
	Texture = <ScreenTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(screen, input.TextureCoordinates);
	if (color.a)
		color.rgb = 1 - color.rgb;
	return color;
}

technique Technique
{
	pass Pass
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
}