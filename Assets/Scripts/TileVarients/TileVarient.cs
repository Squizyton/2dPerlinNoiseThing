using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace TileVarients
{
    [CreateAssetMenu(fileName = "New Tile Variant", menuName = "Scriptable Objects/New Variant")]
    public class TileVarient : SerializedScriptableObject
    {
        public EnvironmentType type;


        public List<Tile> variants;


        [Space(20)] public bool AutomateTheProcess;

        [ShowIf("@AutomateTheProcess"), MinMaxSlider(0, 1, true)]
        public Vector2 baseWeights;

        [ShowIf("@AutomateTheProcess"), Required]
        public Sprite TileSheet;

        [ShowIf("@AutomateTheProcess"), Button("Automate Adding Tiles")]
        public void AutomateProcess()
        {
            Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(TileSheet));
            if (data == null) return;
            foreach (var obj in data)
            {
                if (obj is not Sprite sprite) continue;
                var newVariant = new Tile()
                {
                    sprite = sprite,
                    weight = Random.Range(baseWeights.x, baseWeights.y)
                };
                    
                variants.Add(newVariant);
            }
        }

        [System.Serializable]
        public struct Tile
        {
            [PreviewField] public Sprite sprite;

            public float weight;
        }

        public enum EnvironmentType
        {
            Grass,
            Sand,
            Water,
            Mountain,
            Nothing
        }
    }
}