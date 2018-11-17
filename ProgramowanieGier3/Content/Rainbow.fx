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
	float2 coords = input.TextureCoordinates.xy;
	float4 color = tex2D(screen, 1-coords) * input.Color;
	if (!any(color)) return color;

	float step = 1.0 / (7*6);
	
	if (coords.x < step )
		color = float4(1, 0, 0, 1);
	else if (coords.x < (step * 2)) 
		color = float4(1, .5, 0, 1);
	else if (coords.x < (step * 3)) 
		color = float4(1, 1, 0, 1);
	else if (coords.x < (step * 4)) 
		color = float4(0, 1, 0, 1);
	else if (coords.x < (step * 5)) 
		color = float4(0, 0, 1, 1);
	else if (coords.x < (step * 6)) 
		color = float4(.3, 0, .8, 1);
	else    
		color = float4(1, .8, 1, 1);
return color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
}