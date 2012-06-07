float4x4 world;

float4x4 view;

float4x4 projection;

float3 lightDirection = normalize(float3(1, 1, 1));
float toonThresholds[2] = { 0.8, 0.4 };

float toonBrightnessLevels[3] = { 1.3, 0.9, 0.5 };

bool textureEnabled;

texture Texture;

sampler Sampler = sampler_state

{

Texture = (Texture);

MinFilter = Linear;

MagFilter = Linear;

MipFilter = Linear;

AddressU = Wrap;

AddressV = Wrap;

};
struct VertexShaderInput

{

float4 Position : POSITION0;

float3 Normal : NORMAL0;

float2 TextureCoordinate : TEXCOORD0;

};

struct LightingPixelShaderInput

{

float2 TextureCoordinate : TEXCOORD0;

float LightAmount : TEXCOORD1;

};

struct LightingVertexShaderOutput

{

float4 Position : POSITION0;

float2 TextureCoordinate : TEXCOORD0;

float LightAmount : TEXCOORD1;

};

struct NormalDepthVertexShaderOutput

{

float4 Position : POSITION0;

float4 Color : COLOR0;

};
LightingVertexShaderOutput LightingVertexShader(VertexShaderInput input)

{

LightingVertexShaderOutput output;

output.Position = mul(mul(mul(input.Position, world), view), projection);

output.TextureCoordinate = input.TextureCoordinate;

float3 worldNormal = mul(input.Normal, world);

output.LightAmount = dot(worldNormal, lightDirection);

return output;

}

float4 ToonPixelShader(LightingPixelShaderInput input) : COLOR0

{

float4 color = TextureEnabled ? tex2D(Sampler, input.TextureCoordinate) : 0;

float light;

if (input.LightAmount > toonThresholds[0])

light = toonBrightnessLevels[0];

else if (input.LightAmount > toonThresholds[1])

light = toonBrightnessLevels[1];

else

light = toonBrightnessLevels[2];

color.rgb *= light;

return color;

}

NormalDepthVertexShaderOutput NormalDepthVertexShader(VertexShaderInput input)

{

NormalDepthVertexShaderOutput output;

// Apply camera matrices to the input position.

output.Position = mul(mul(mul(input.Position, world), view), projection);

float3 worldNormal = mul(input.Normal, world);

// The output color holds the normal, scaled to fit into a 0 to 1 range.

output.Color.rgb = (worldNormal + 1) / 2;

// The output alpha holds the depth, scaled to fit into a 0 to 1 range.

output.Color.a = output.Position.z / output.Position.w;

return output;

}

float4 NormalDepthPixelShader(float4 color : COLOR0) : COLOR0

{

return color;

}

technique Toon

{

pass P0

{

VertexShader = compile vs_1_1 LightingVertexShader();

PixelShader = compile ps_2_0 ToonPixelShader();

}

}

technique NormalDepth

{

pass P0

{

VertexShader = compile vs_1_1 NormalDepthVertexShader();

PixelShader = compile ps_1_1 NormalDepthPixelShader();

}

}