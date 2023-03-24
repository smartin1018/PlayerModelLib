using System;
using System.Collections.Generic;
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
        // private EntityQuery ViewQuery;

        protected override void Initialise()
        {
            base.Initialise();
            PlayerQuery = GetEntityQuery(typeof(CPlayer));
            // ViewQuery = GetEntityQuery(typeof(CPlayerModelView));
        }

        /*public override void BeforeSaving(SaveSystemType system_type)
        {
            base.BeforeSaving(system_type);
            PlayerModelLib.LogInfo("Saving skins...");
            var views = ViewQuery.ToComponentDataArray<CPlayerModelView>(Allocator.Temp);
            foreach (var view in views)
            {
                CustomPlayerModelView.PersistentModelDict[view.PlayerID] = view.PlayerModelID;
            }
            PlayerModelLib.LogInfo(string.Join(Environment.NewLine, CustomPlayerModelView.PersistentModelDict));
        }*/

        public override void AfterLoading(SaveSystemType system_type)
        {
            base.AfterLoading(system_type);
            if (system_type != SaveSystemType.FullWorld) return;
            var playerArray = PlayerQuery.ToEntityArray(Allocator.Temp);
            var components = PlayerQuery.ToComponentDataArray<CPlayer>(Allocator.Temp);

            for (int i = 0; i < playerArray.Length; i++)
            {
                var player = playerArray[i];
                var component = components[i];

                Set(player, new CPlayerModelView()
                {
                    PlayerID = component.ID,
                    PlayerModelID = CustomPlayerModelView.PersistentModelDict[component.ID]
                });
            }
        }

        protected override void OnUpdate()
        {
            
        }
    }
}