using KitchenData;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public class PlayerModel : GameDataObject
    {
        public delegate void RenderManipulation(in SkinnedMeshRenderer skinnedMeshRenderer);
        public GameObject Prefab;

        public Mesh Mesh;
        
        // public RenderManipulation OnApply;

        public Material[] Materials;

        internal bool IsDefault;
        
        protected override void InitialiseDefaults()
        {
            
        }
    }
}