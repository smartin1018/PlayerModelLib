using System;
using System.Collections.Generic;
using System.Linq;
using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public class RestaurantModelRefresh : RestaurantSystem, IModSystem
    {

        private EntityQuery PlayerQuery;

        protected override void Initialise()
        {
            base.Initialise();
            PlayerQuery = GetEntityQuery(typeof(CPlayer));
        }

        public override void AfterLoading(SaveSystemType system_type)
        {
            base.AfterLoading(system_type);
            if (system_type != SaveSystemType.FullWorld) return;
            var playerArray = PlayerQuery.ToEntityArray(Allocator.Temp);
            var components = PlayerQuery.ToComponentDataArray<CPlayer>(Allocator.Temp);

            for (int i = 0; i < playerArray.Length; i++)
            {
                var player = playerArray[i];
                var id = components[i].ID;
                CustomPlayerModelView.PersistentModelDict.TryGetValue(id, out int modelId);

                Set(player, new CPlayerModelView()
                {
                    PlayerID = id,
                    PlayerModelID = modelId,
                });
            }
        }

        protected override void OnUpdate()
        {
            
        }
    }
}