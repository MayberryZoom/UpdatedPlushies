using BepInEx;
using CSync.Lib;
using LethalLib.Modules;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace UpdatedPlushies;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(LethalLib.Plugin.ModGUID)]
[BepInDependency("com.sigurd.csync")]
public class UpdatedPlushies : BaseUnityPlugin {
    public void Awake() {
        Logger.LogInfo($"Begin loading {PluginInfo.PLUGIN_GUID}");

        Logger.LogInfo($"Loading monster plushie asset bundle");
        // unmodified asset bundle from scintesto
        string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "monsterplushies");
        AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);
        (string, Item)[] plushies = [
            ("Bracken Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Bracken Plush/BrackenPlush.asset")),
            ("Bunker Spider Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Bunkspid/BunkspidItem.asset")),
            ("Coil-Head Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Coil head/CoilHeadPlush.asset")),
            ("Eyeless Dog Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Eyeless dog/EyelessDog.asset")),
            ("Forest Keeper Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Giant/Forest keeper plushie.asset")),
            ("Jester Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Jester/jesterplushie.asset")),
            ("Hoarding Bug Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Lootbug/lootbugPlush.asset")),
            ("Comedy Masked Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Masked/comedyplushitem.asset")),
            ("Tragedy Masked Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Masked/Tragedyplushitem.asset")),
            ("Nutcracker Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Nutcracker/Nutcracker.asset")),
            ("Thumper Plushie", bundle.LoadAsset<Item>("Assets/MY STUFF/Thumper/Thumper.asset")),
        ];
        Logger.LogInfo($"Finished loading monster plushie asset bundle");

        foreach ((string plushieName, Item plushieItem) in plushies) {
            Logger.LogInfo($"Registering {plushieName}");
            PlushieConfig plushieConfig = new(Config, plushieName);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(plushieItem.spawnPrefab);
            Utilities.FixMixerGroups(plushieItem.spawnPrefab);

            // We should use the user friendly representations of values in the
            // config rather than the true values, so some need to be converted here
            plushieItem.minValue = (int)(plushieConfig.MinValue / 0.4); // prefab value * 0.4 = in game value
            plushieItem.maxValue = (int)(plushieConfig.MaxValue / 0.4); // prefab value * 0.4 = in game value
            plushieItem.twoHanded = plushieConfig.IsTwoHanded;
            plushieItem.twoHandedAnimation = plushieConfig.IsTwoHanded;
            plushieItem.weight = plushieConfig.CarryWeight / 105 + 1; // (prefab value - 1) * 105 = in game value
            plushieItem.isConductiveMetal = plushieConfig.IsConductive;

            // Register to moons
            foreach ((Levels.LevelTypes levelId, SyncedEntry<int> spawnWeight) in plushieConfig.SpawnWeights) {
                if (spawnWeight > 0) {
                    Logger.LogInfo($"Registering {plushieName} on {levelId} with a spawn weight of {spawnWeight.Value}");
                    Items.RegisterScrap(plushieItem, spawnWeight.Value, levelId);
                }
            }
            Logger.LogInfo($"Finished registering {plushieName}");
        }

        Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} has loaded!");
    }
}
