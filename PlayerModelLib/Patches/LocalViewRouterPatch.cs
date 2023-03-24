using System;
using HarmonyLib;
using Kitchen;
using UnityEngine;

namespace KitchenPlayerModelLib.Patches
{
    [HarmonyPatch]
    internal static class LocalViewRouterPatch
    {
        [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab", new Type[] { typeof(ViewType) })]
        [HarmonyPostfix]
        static void GetPrefab_Postfix(ref GameObject __result, ViewType view_type)
        {
            if (view_type == ViewType.Player && __result.GetComponent<CustomPlayerModelView>() == null)
            {
                CustomPlayerModelView view = __result.AddComponent<CustomPlayerModelView>();
                view.PlayerMeshedRenderer = __result.GetComponentInChildren<SkinnedMeshRenderer>();
            }
        }
    }
}