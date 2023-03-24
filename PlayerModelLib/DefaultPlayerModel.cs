using KitchenData;
using KitchenMods;

namespace KitchenPlayerModelLib
{
    public class DefaultPlayerModel : CustomPlayerModel
    {
        public override string UniqueNameID => "DefaultPlayerModel";

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            base.Convert(gameData, out gameDataObject);
            ((PlayerModel)gameDataObject).IsDefault = true;
        }
    }
}