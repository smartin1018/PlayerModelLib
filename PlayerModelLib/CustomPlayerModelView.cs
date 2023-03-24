using System;
using System.Linq;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using MessagePack;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public class CustomPlayerModelView : UpdatableObjectView<CustomPlayerModelView.CustomPlayerModelViewData>
    {
        public SkinnedMeshRenderer PlayerMeshedRenderer;

        private PlayerInfo PlayerInfo;
        
        private static readonly int Color0 = Shader.PropertyToID("_Color0");

        private CustomPlayerModelViewData Data;
        
        public class UpdateView : IncrementalViewSystemBase<CustomPlayerModelViewData>, IModSystem
        {
            private EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CPlayerModelView)));
            }

            protected override void OnUpdate()
            {
                using var views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var components = Views.ToComponentDataArray<CPlayerModelView>(Allocator.Temp);

                for (int i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var data = components[i];
                    
                    SendUpdate(view, new CustomPlayerModelViewData()
                    {
                        PlayerModelID = data.PlayerModelID,
                        PlayerID = data.PlayerID
                    });
                }

                EntityManager.RemoveComponent<CPlayerModelView>(Views);
            }
        }
        
        [MessagePackObject]
        public struct CustomPlayerModelViewData : ISpecificViewData, IViewData.ICheckForChanges<CustomPlayerModelViewData>
        {
            [Key(0)] public int PlayerModelID;
            [Key(1)] public int PlayerID;
            public IUpdatableObject GetRelevantSubview(IObjectView view)
            {
                return view.GetSubView<CustomPlayerModelView>();
            }

            public bool IsChangedFrom(CustomPlayerModelViewData check)
            {
                return PlayerModelID != check.PlayerModelID;
            }
        }

        protected override void UpdateData(CustomPlayerModelViewData new_data)
        {
            var data = this.Data;
            this.Data = new_data;
            if (this.Data.PlayerModelID == data.PlayerModelID)
            {
                return;
            }

            this.PlayerInfo = Players.Main.Get(this.Data.PlayerID);
            var playerModel = GameData.Main.Get<PlayerModel>(this.Data.PlayerModelID);
            var defaultMaterial = new Material(MaterialUtils.GetExistingMaterial("Player"));
            defaultMaterial.SetColor(Color0, PlayerInfo.Profile.Colour);
            if (playerModel != null)
            {
                var materials = new[] {defaultMaterial};
                if (playerModel.Materials != null && playerModel.Materials.Length > 0)
                {
                    materials = new Material[playerModel.Materials.Length];
                    Array.Copy(playerModel.Materials, materials, playerModel.Materials.Length);
                }
                
                for (var i = 0; i < materials.Length; i++)
                {
                    var material = materials[i];
                    if (material == null)
                    {
                        materials[i] = defaultMaterial;
                    }
                }
                
                PlayerMeshedRenderer.sharedMesh = playerModel.Mesh == null ? PlayerModelLib.DefaultMeshRenderer.sharedMesh : playerModel.Mesh;
                PlayerMeshedRenderer.materials = materials;
            }
            else
            {
                PlayerModelLib.LogInfo("Couldn't Find Model: " + this.Data.PlayerModelID);
            }
        }
    }
}