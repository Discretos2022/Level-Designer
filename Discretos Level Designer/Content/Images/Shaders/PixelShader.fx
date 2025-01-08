#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 col = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;

	//float4 avgColor = (col.r + col.g + col.b) / 3;

	/// effet ombre
	col.rgba = col.rgba * (input.TextureCoordinates.y * 1 - 0.4f);

	return col;


	/// pixelisation ////////////////////////////////////////////////////////////////////////
	/*float pixels = 64.0f;
	float pixelation = 0.1f;
	float mx = input.TextureCoordinates.x * pixels;
	float my = input.TextureCoordinates.y * pixels;

	float x = round(mx / pixelation) * pixelation;
	float y = round(my / pixelation) * pixelation;
	float2 coord = float2(x / pixels, y / pixels);

	return tex2D(SpriteTextureSampler, coord);*/
	////////////////////////////////////////////////////////////////////////////////////////


}

float4 RDT(VertexShaderOutput input) : COLOR
{
	float4 col = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;

	return col;



}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL RDT();
	}

	pass P1
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};