using System.Linq;
using KitchenData;
using KitchenLib.Customs;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public abstract class CustomPlayerModel : CustomGameDataObject<PlayerModel>, ICustomHasPrefab
    {

        public virtual GameObject Prefab { get; protected set; }
        
        public virtual Mesh Mesh { get; protected set; }

        // public PlayerModel.RenderManipulation OnApply => ApplySkin;
        
        public virtual Material[] Materials { get; protected set; }

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            PlayerModel result = ScriptableObject.CreateInstance<PlayerModel>();

            if (BaseGameDataObjectID != -1)
            {
                result = UnityEngine.Object.Instantiate(gameData.Get<PlayerModel>()
                    .FirstOrDefault(model => model.ID == BaseGameDataObjectID));
            }

            result.ID = ID;
            result.Prefab = Prefab;
            result.Mesh = Mesh;
            result.Materials = Materials;
            // result.OnApply = OnApply;

            gameDataObject = result;
        }

        public virtual void SetupPrefab(GameObject prefab) { }
        
        public virtual void ApplySkin(in SkinnedMeshRenderer skinnedMeshRenderer) { }
    }
}