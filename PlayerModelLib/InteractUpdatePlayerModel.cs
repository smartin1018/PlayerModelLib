using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace KitchenPlayerModelLib
{
    public class InteractUpdatePlayerModel : InteractionSystem, IModSystem
    {
        private int modelIdx = 0;
        //TODO: Create Custom Appliance
        protected override bool IsPossible(ref InteractionData data) => this.HasComponent<CPlayerModelStatue>(data.Target);

        protected override void Perform(ref InteractionData data)
        {
            var require = Require(data.Interactor, out CPlayer player);
            if (!require) return;
            var length = PlayerModelLib.PlayerModels.Length;
            var playerModel = PlayerModelLib.PlayerModels[modelIdx = ++modelIdx % length];
            CPlayerModelView component = new CPlayerModelView()
            {
                PlayerModelID = playerModel.ID,
                PlayerID = player.ID,
            };

            Set(data.Interactor, component);
        }
    }
}