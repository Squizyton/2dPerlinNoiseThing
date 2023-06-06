using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using ItemData = Items.ItemData;
using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

namespace Structures
{
    public class StructureCreateWindow : OdinEditorWindow
    {

        private static StructureData _currenStructureData;
        private static bool makeANewData;


        protected static void BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            tree.AddAllAssetsAtPath("Building Materials", "Assets/ScriptableObjects/Items/Building Materials",
                typeof(ItemData));
            tree.DrawMenuTree();
            
        }

        public static void OpenWindow(StructureData data)
        {
            _currenStructureData = data;
            var window = GetWindow<StructureCreateWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.name = "Structure Building";
            window.Show();
            BuildMenuTree();
            
           
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_currenStructureData)
            {
                _currenStructureData = null;
                //DestroyImmediate(_creationClass);
            }

            _creationClass = null;
        }

        protected override void OnEnable()
        {
            StructureData nonStaticCopy = _currenStructureData;
            
            _creationClass = new StructureCreationClass(nonStaticCopy);
        }

        [SerializeField]
        private StructureCreationClass _creationClass;

    }
    
    public class StructureCreationClass
    {
        private StructureData _data;
        
        [Title("{Building}"),ReadOnly]
        public string buildingName;

        
        public ItemData testItemData;


        [Title("Building Materials")]
        public List<ItemData> buildingMaterials;

        [OdinSerialize,TableMatrix(HorizontalTitle = "Custom Cell Drawing",SquareCells = true, ResizableColumns = true,RowHeight = 1)]
        public Texture2D[,] CustomCellDrawing;
        // DrawElementMethod = "DrawColoredEnumElement"
    
#if UNITY_EDITOR // Editor-related code must be excluded from builds

#endif
        public StructureCreationClass()
        {
        }

        public StructureCreationClass(StructureData data) : base()
        {
            
            _data = data;
            
            buildingMaterials = new List<ItemData>();

            var materials = GetSOObjects.GetAllSOsAtFilePath($"/ScriptableObjects/Items/Building Materials");

            Debug.Log(materials.Length);
            
            foreach (var o in materials)
            {
                var matt = (ItemData) o;
                buildingMaterials.Add(matt);
            }

            if (!_data) return;
            
            CustomCellDrawing = new Texture2D[_data.structWidth, _data.structHeight];
            for(int x = 0; x < _data.structWidth;x++)
            for (int y = 0; y < _data.structHeight; y++)
                CustomCellDrawing[x, y] = null;
            Debug.Log(CustomCellDrawing.Length);
            buildingName = _data.buildingName;
        }
    }
}
