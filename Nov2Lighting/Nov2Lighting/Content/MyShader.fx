#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
texture Texture;
float3 DiffuseColor = float3(0, 1, 0); //set to green

sampler BasicTextureSampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 UV : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 UV : TEXCOORD0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.UV = input.UV;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float3 output = DiffuseColor * tex2D(BasicTextureSampler, input.UV);
    if (output.x != 0)
    {
        //output.x = 1 / output.x;
    }
    if (output.y != 0)
    {
        //output.y = 1 / output.y;
    }
    if (output.z != 0)
    {
        //output.z = 1 / output.z;
    }
    //output.x = 1;
    //output.y = 0;
    //output.z = 1;
    //output.x = output.x * 2;
    //output.y = output.y;
    //output.z = 1 / output.z;
    return float4(output, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};