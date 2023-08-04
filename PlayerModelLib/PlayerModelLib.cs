using System.Linq;
using System.Reflection;
using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenMods;
using UnityEngine;

namespace KitchenPlayerModelLib
{
    public class PlayerModelLib : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "com.martoph.playermodellib";
        public const string MOD_NAME = "PlayerModelLIb";
        public const string MOD_VERSION = "0.1.1";
        public const string MOD_AUTHOR = "Martoph";
        public const string MOD_GAMEVERSION = ">=1.1.7";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until


        // Boolean constant whose value depends on whether you built with DEBUG or RELEASE mode, useful for testing
#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        public static AssetBundle Bundle { get; private set; } 
        public static SkinnedMeshRenderer DefaultMeshRenderer { get; private set; }
        public static PlayerModel[] PlayerModels { get; private set; }

        public PlayerModelLib() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            
            LogInfo("ON START RUNNING");
            DefaultMeshRenderer = AssetDirectory.ViewPrefabs[ViewType.Player].GetComponentInChildren<SkinnedMeshRenderer>();
            var playerModels = GameData.Main.Get<PlayerModel>().ToList();
            for (var i = 0; i < playerModels.Count; i++)
            {
                if (!playerModels[i].IsDefault) continue;
                var model = playerModels[i];
                playerModels.RemoveAt(i);
                playerModels.Insert(0, model);
            }

            PlayerModels = playerModels.ToArray();
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            AddGameDataObject<DefaultPlayerModel>();

            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // Register custom GDOs
            AddGameData();
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}