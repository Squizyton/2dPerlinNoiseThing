using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Jobs
{
    public struct FalloffGenerator : IJob
    {
        public NativeArray<float> FalloffMap;
        public bool useAnimationCurve;
        public uint MapSize;


        public float[,] GenerateFalloffMap(uint size)
        {
            var map = new float[size, size];
            
            for(var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
            {
                var x = i / (float) size * 2 - 1;
                var y = j / (float) size * 2 - 1;

                var value = math.max(math.abs(x), math.abs(y));
                if (!useAnimationCurve)
                    map[i, j] = Evaluate(value);
                else map[i, j] = EvaluateUsingCurve(x,y);
            }

            return map;
        }

        private float Evaluate(float value)
        {
            const int a = 3;
            const float b = 2.2f;

            return math.pow(value, a) / (math.pow(value, a) + math.pow(b - b * value, a));
        }

        private float EvaluateUsingCurve(float x, float y)
        {
           // return  -1 * ((curve.Evaluate(x) * curve.Evaluate(y) * 2) - 1);
           return 0;
        }

        public void Execute()
        {
            var array = GenerateFalloffMap(MapSize);
            var oneDArray = new float[MapSize*MapSize];
            
            for(var i = 0; i < MapSize; i++)
            for (var j = 0; j < MapSize; j++)
            {
                oneDArray[i * MapSize + j] = array[i, j];
            }

            for (var i = 0; i < oneDArray.Length; i++)
                FalloffMap[i] = oneDArray[i];
        }
    }
}
