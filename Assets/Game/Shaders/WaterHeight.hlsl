#define MAX_WAVE_ARRAY_SIZE 16
#define PI 3.141592653589793238462643383279502884
float4 _Waves[MAX_WAVE_ARRAY_SIZE];

void WaterHeight_float(
    float2 worldXZ,
    float time,
    float amp,
    float freq1, float speed1,
    float freq2, float speed2,
    float radiusFalloff,
    float waveHeight,
    float interactionWaveSpeed,
    out float height
){
    float baseWave1 = sin(worldXZ.x * freq1 + time * speed1);
    float baseWave2 = sin(worldXZ.y * freq2 + time * speed2);
    float baseWave = (baseWave1 + baseWave2) * 0.5;

    baseWave *= radiusFalloff;

    float baseHeight = baseWave * amp;

    float maxWaveHeight = 0;
    for (int i = 0; i < MAX_WAVE_ARRAY_SIZE; i++)
    {
        float currStartPointDistance = distance(float2(_Waves[i].x, _Waves[i].y), worldXZ);
        
        float startTimeOffset = 0.06;
        float currWaveTime = (time - _Waves[i].z) * interactionWaveSpeed + startTimeOffset;

        float currWaveHeight;
        if (currStartPointDistance < currWaveTime - 0.1 || currStartPointDistance > currWaveTime)
            currWaveHeight = 0;
        else
            currWaveHeight = (sin((PI * currStartPointDistance + 3 * PI / 8 - PI * currWaveTime) * 20) + 1) * _Waves[i].w * waveHeight;

        if (currWaveHeight > maxWaveHeight)
            maxWaveHeight = currWaveHeight;
    }

    height = baseHeight - maxWaveHeight;
}