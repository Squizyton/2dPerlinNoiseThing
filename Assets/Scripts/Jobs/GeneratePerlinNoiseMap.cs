using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Jobs
{
    public struct GeneratePerlinNoiseMap : IJobParallelFor
    {
        public NativeArray<float> _tilesPerlined;

        public int MapWidth;
        public int MapHeight;
        public float NoiseScale;
        public int Octaves;
        public float Persistance;
        public float Lacunarity;
        public float Frequency;
        [ReadOnly]
        public NativeArray<float2> OctaveOffsets;
        private float GenerateNoise(int index)
        {
            float amplitude = 1;
            //The Higher the frequency, the further apart the sample points will be which means height valeus will change more rapidally
            var frequency = Frequency;
            float noiseHeight = 0;
            
            if (NoiseScale <= 0)
            {
                NoiseScale = 0.0001f;
            }
            
            var halfWidth = MapWidth / 2f;
            var halfHeight = MapHeight / 2f;
            
            var x = index % MapWidth;
            var y = index / MapHeight;
            
            for (var i =0 ; i < Octaves; i++)
            {
                var sampleX = (x - halfWidth) / NoiseScale * frequency + OctaveOffsets[i].x;
                var sampleY = (y - halfHeight) / NoiseScale * frequency + OctaveOffsets[i].y;
            
                var pos = math.float2(sampleX, sampleY);
                
                
                //Todo: Change Mathf.PerlinNosie to Job equivalent when..it works
                var perlinedValue = noise.cnoise(new float2(sampleX, sampleY));
                
               // Debug.Log($"Value: {perlinedValue}");
                
                
                noiseHeight += perlinedValue * amplitude;

                //Decreases each octave
                amplitude *= Persistance;
                //Increases each octave
                frequency *= Lacunarity;
            }

            return noiseHeight;
        }
        public void Execute(int index)
        {
            _tilesPerlined[index] = GenerateNoise(index);
        }
    }
}