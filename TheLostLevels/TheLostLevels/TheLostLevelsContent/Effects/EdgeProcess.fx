float edgeWidth = 1;

float edgeIntensity = 1;

float normalSensitivity = 1;

float depthSensitivity = 10;

float normalThreshold = 0.5;

float depthThreshold = 0.1;

float2 screenResolution;

texture sceneTexture;

sampler SceneSampler : register(s0) = sampler_state

{

Texture = (sceneTexture);

MinFilter = Linear;

MagFilter = Linear;

AddressU = Clamp;

AddressV = Clamp;

};

texture normalDepthTexture;

sampler NormalDepthSampler : register(s1) = sampler_state

{

Texture = (normalDepthTexture);

MinFilter = Linear;

MagFilter = Linear;

AddressU = Clamp;

AddressV = Clamp;

};

float4 PixelShader(float2 texCoord : TEXCOORD0) : COLOR0

{

float3 scene = tex2D(SceneSampler, texCoord);

float2 edgeOffset = edgeWidth / screenResolution;

float4 n1 = tex2D(NormalDepthSampler, texCoord + float2(-1, -1) * edgeOffset);

float4 n2 = tex2D(NormalDepthSampler, texCoord + float2( 1, 1) * edgeOffset);

float4 n3 = tex2D(NormalDepthSampler, texCoord + float2(-1, 1) * edgeOffset);

float4 n4 = tex2D(NormalDepthSampler, texCoord + float2( 1, -1) * edgeOffset);

float4 diagonalDelta = abs(n1 - n2) + abs(n3 - n4);

float normalDelta = dot(diagonalDelta.xyz, 1);

float depthDelta = diagonalDelta.w;

normalDelta = saturate((normalDelta - normalThreshold) * normalSensitivity);

depthDelta = saturate((depthDelta - depthThreshold) * depthSensitivity);

float edgeAmount = saturate(normalDelta + depthDelta) * edgeIntensity;

scene *= (1 - edgeAmount);

return float4(scene, 1);

}

technique EdgeDetect

{

pass P0

{

PixelShader = compile ps_2_0 PixelShader();

}

}