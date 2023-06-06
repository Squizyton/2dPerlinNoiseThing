using Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Structures
{
    
    [CreateAssetMenu(fileName = "New Structure", menuName = "Structures/New Structure")]
    public class StructureData : SerializedScriptableObject
    {

        
        [Title("Structure Important Settings"),InfoBox("These cannot be 0"),OnValueChanged("SetArrayLength")]
        [Required] public int structWidth;
        [Required] public int structHeight;


        [Title("Misc Settings")] public string buildingName;
        public float weight;

        [Space(40)]
        [Space(20)]
        private bool fakeVariable;
        [EnableIf("@structWidth != 0 && structHeight != 0"), Button("Make A Structure")]
        public void DebugFunction()
        {
           StructureCreateWindow.OpenWindow(this);
        }


        private ItemData[,] structure;

        private void SetArrayLength()
        {
            if (structure != null)
            {
                var temp = structure;

                structure = null;

                structure = new ItemData[structWidth, structHeight];

                for (var x = 0; x < structWidth; x++)
                for (var y = 0; x < structHeight; x++)
                {
                    if (temp[x, y] != null)
                    {
                        structure[x,y] = temp[x, y];
                    }
                }
            }
            else structure = new ItemData[structWidth, structHeight];
        }



        public void SetStructureMapData(Vector2 pos, ItemData material)
        {
            structure[(int)pos.x, (int)pos.y] = material;
        }


    }
}
