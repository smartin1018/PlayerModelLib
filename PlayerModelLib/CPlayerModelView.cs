using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace KitchenPlayerModelLib
{
    public struct CPlayerModelView : IModComponent
    {
        public int PlayerModelID;
        public int PlayerID;
    }
}