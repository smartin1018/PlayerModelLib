using Kitchen;
using KitchenLib.References;
using KitchenMods;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public class SpawnStatue : FranchiseFirstFrameSystem, IModSystem
    {
        protected override void OnUpdate()
        {
            var entity = Create(ApplianceReferences.FancyStatue, new Vector3(6f, 0f, 7f), new Vector3(0f, 90f, 0f));
            Set(entity, new CPlayerModelStatue());
        }
    }
}