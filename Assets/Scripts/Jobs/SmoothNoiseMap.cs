using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jobs
{
    [BurstCompile]
    public struct SmoothNoiseMap : IJob
    {
        public int2 Dimensions;
        public NativeArray<float> result;

      

        private void SmoothMapOut(NativeArray<float> array)
        {
            var tempArray = new NativeArray<float>(array.Length,Allocator.Temp);
            
            array.CopyTo(tempArray);
            
            var maxNoiseHeight = float.MinValue;
            var minNoiseHeight = float.MaxValue;
            
            for (var y = 0; y < Dimensions.y; y++)
            for (var x = 0; x < Dimensions.x; x++)
            {
                var noiseHeight = tempArray[y * Dimensions.x + x];

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                tempArray[y * Dimensions.x + x] = noiseHeight;
            }
            
            for (var y = 0; y < Dimensions.y; y++)
            {
                for (var x = 0; x < Dimensions.x; x++)
                {
                   tempArray[y * Dimensions.x + x] = math.unlerp(minNoiseHeight, maxNoiseHeight, tempArray[y * Dimensions.x + x]);
                }
            }

            result = tempArray;
            tempArray.Dispose();
        }

        public void Execute()
        {
            SmoothMapOut(result);
        }
    }
}