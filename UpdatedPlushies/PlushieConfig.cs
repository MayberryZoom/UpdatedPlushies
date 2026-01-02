using BepInEx.Configuration;
using System.Collections.Generic;
using CSync.Lib;
using System.Runtime.Serialization;
using CSync.Extensions;
using LethalLib.Modules;

namespace UpdatedPlushies {
    struct DefaultPlushieConfig {

    }
    class PlushieConfig : SyncedConfig2<PlushieConfig> {
        private static readonly (string, Levels.LevelTypes)[] moons = [
            ("Experimentation", Levels.LevelTypes.ExperimentationLevel),
            ("Assurance", Levels.LevelTypes.AssuranceLevel),
            ("Vow", Levels.LevelTypes.VowLevel),
            ("Offense", Levels.LevelTypes.OffenseLevel),
            ("March", Levels.LevelTypes.MarchLevel),
            ("Adamance", Levels.LevelTypes.AdamanceLevel),
            ("Rend", Levels.LevelTypes.RendLevel),
            ("Dine", Levels.LevelTypes.DineLevel),
            ("Titan", Levels.LevelTypes.TitanLevel),
            ("Artifice", Levels.LevelTypes.ArtificeLevel),
            ("Embrion", Levels.LevelTypes.EmbrionLevel),
        ];

        // not sure this is the best way to handle these, but I can't be bothered
        // Dictionary<plushieName, (defaultMin, defaultMax, isTwoHanded, carryWeight, isConductive, Dictionary<moonId, defaultSpawnWeight>)>
        private readonly Dictionary<string, (int, int, bool, float, bool, Dictionary<Levels.LevelTypes, int>)> defaultConfigs = new() {
            { "Bracken Plushie", (20, 70, false, 5.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 3 },
                { Levels.LevelTypes.AssuranceLevel, 2 },
                { Levels.LevelTypes.VowLevel, 12 },
                { Levels.LevelTypes.OffenseLevel, 1 },
                { Levels.LevelTypes.MarchLevel, 9 },
                { Levels.LevelTypes.AdamanceLevel, 4 },
                { Levels.LevelTypes.RendLevel, 6 },
                { Levels.LevelTypes.DineLevel, 4 },
                { Levels.LevelTypes.TitanLevel, 5 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 1 },
            }) },
            { "Bunker Spider Plushie", (30, 50, false, 7.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 12 },
                { Levels.LevelTypes.AssuranceLevel, 11 },
                { Levels.LevelTypes.VowLevel, 7 },
                { Levels.LevelTypes.OffenseLevel, 10 },
                { Levels.LevelTypes.MarchLevel, 10 },
                { Levels.LevelTypes.AdamanceLevel, 8 },
                { Levels.LevelTypes.RendLevel, 5 },
                { Levels.LevelTypes.DineLevel, 3 },
                { Levels.LevelTypes.TitanLevel, 5 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 4 },
            }) },
            { "Coil-Head Plushie", (50, 70, false, 8.0F, true, new() {
                { Levels.LevelTypes.VowLevel, 1 },
                { Levels.LevelTypes.OffenseLevel, 6 },
                { Levels.LevelTypes.MarchLevel, 2 },
                { Levels.LevelTypes.AdamanceLevel, 2 },
                { Levels.LevelTypes.RendLevel, 5 },
                { Levels.LevelTypes.DineLevel, 3 },
                { Levels.LevelTypes.TitanLevel, 5 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 4 },
            }) },
            { "Eyeless Dog Plushie", (30, 60, false, 7.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 12 },
                { Levels.LevelTypes.AssuranceLevel, 9 },
                { Levels.LevelTypes.VowLevel, 1 },
                { Levels.LevelTypes.OffenseLevel, 10 },
                { Levels.LevelTypes.MarchLevel, 5 },
                { Levels.LevelTypes.AdamanceLevel, 5 },
                { Levels.LevelTypes.RendLevel, 11 },
                { Levels.LevelTypes.DineLevel, 9 },
                { Levels.LevelTypes.TitanLevel, 12 },
                { Levels.LevelTypes.ArtificeLevel, 5 },
                { Levels.LevelTypes.EmbrionLevel, 1 },
            }) },
            { "Forest Keeper Plushie", (50, 80, true, 12.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 1 },
                { Levels.LevelTypes.AssuranceLevel, 1 },
                { Levels.LevelTypes.VowLevel, 12 },
                { Levels.LevelTypes.OffenseLevel, 2 },
                { Levels.LevelTypes.MarchLevel, 8 },
                { Levels.LevelTypes.AdamanceLevel, 4 },
                { Levels.LevelTypes.RendLevel, 9 },
                { Levels.LevelTypes.DineLevel, 11 },
                { Levels.LevelTypes.TitanLevel, 6 },
                { Levels.LevelTypes.ArtificeLevel, 6 },
                { Levels.LevelTypes.EmbrionLevel, 1 },
            }) },
            { "Jester Plushie", (50, 90, false, 9.0F, true, new() {
                { Levels.LevelTypes.MarchLevel, 1 },
                { Levels.LevelTypes.AdamanceLevel, 1 },
                { Levels.LevelTypes.RendLevel, 7 },
                { Levels.LevelTypes.DineLevel, 3 },
                { Levels.LevelTypes.TitanLevel, 6 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
            }) },
            { "Hoarding Bug Plushie", (10, 30, false, 4.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 6 },
                { Levels.LevelTypes.AssuranceLevel, 12 },
                { Levels.LevelTypes.VowLevel, 10 },
                { Levels.LevelTypes.OffenseLevel, 4 },
                { Levels.LevelTypes.MarchLevel, 6 },
                { Levels.LevelTypes.AdamanceLevel, 8 },
                { Levels.LevelTypes.DineLevel, 4 },
                { Levels.LevelTypes.TitanLevel, 3 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 15 },
            }) },
            { "Comedy Masked Plushie", (30, 60, false, 5.0F, false, new() {
                { Levels.LevelTypes.AssuranceLevel, 1 },
                { Levels.LevelTypes.AdamanceLevel, 1 },
                { Levels.LevelTypes.RendLevel, 5 },
                { Levels.LevelTypes.DineLevel, 1 },
                { Levels.LevelTypes.TitanLevel, 4 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
            }) },
            { "Tragedy Masked Plushie", (40, 50, false, 5.0F, false, new() {
                { Levels.LevelTypes.AssuranceLevel, 1 },
                { Levels.LevelTypes.AdamanceLevel, 1 },
                { Levels.LevelTypes.RendLevel, 3 },
                { Levels.LevelTypes.DineLevel, 3 },
                { Levels.LevelTypes.TitanLevel, 4 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
            }) },
            { "Nutcracker Plushie", (30, 80, false, 8.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 1 },
                { Levels.LevelTypes.AssuranceLevel, 1 },
                { Levels.LevelTypes.OffenseLevel, 1 },
                { Levels.LevelTypes.MarchLevel, 1 },
                { Levels.LevelTypes.AdamanceLevel, 1 },
                { Levels.LevelTypes.RendLevel, 12 },
                { Levels.LevelTypes.DineLevel, 3 },
                { Levels.LevelTypes.TitanLevel, 6 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 4 },
            }) },
            { "Thumper Plushie", (30, 60, true, 10.0F, false, new() {
                { Levels.LevelTypes.ExperimentationLevel, 3 },
                { Levels.LevelTypes.AssuranceLevel, 4 },
                { Levels.LevelTypes.VowLevel, 2 },
                { Levels.LevelTypes.OffenseLevel, 13 },
                { Levels.LevelTypes.MarchLevel, 12 },
                { Levels.LevelTypes.AdamanceLevel, 10 },
                { Levels.LevelTypes.DineLevel, 2 },
                { Levels.LevelTypes.TitanLevel, 5 },
                { Levels.LevelTypes.ArtificeLevel, 4 },
                { Levels.LevelTypes.EmbrionLevel, 5 },
            }) },
        };

        [DataMember] public SyncedEntry<int> MinValue { get; private set; }
        [DataMember] public SyncedEntry<int> MaxValue { get; private set; }
        [DataMember] public SyncedEntry<bool> IsTwoHanded { get; private set; }
        [DataMember] public SyncedEntry<float> CarryWeight { get; private set; }
        [DataMember] public SyncedEntry<bool> IsConductive { get; private set; }
        [DataMember] public Dictionary<Levels.LevelTypes, SyncedEntry<int>> SpawnWeights { get; private set; } = [];

        public PlushieConfig(ConfigFile configFile, string name) : base("Mayberry.UpdatedPlushies") {
            //ConfigManager.Register(this);
            string section = name + " Settings";
            var (defaultMin, defaultMax, defaultTwoHanded, defaultCarryWeight, defaultConductive, defaultMoonWeights) = defaultConfigs[name];

            MinValue = SyncedBindingExtensions.BindSyncedEntry(
                configFile,
                section,
                $"{name} Minimum Value",
                defaultMin,
                $"The lowest sell value the {name} can appear with, in $."
            );
            MaxValue = SyncedBindingExtensions.BindSyncedEntry(
                configFile,
                section,
                $"{name} Maximum Value",
                defaultMax,
                $"The highest sell value the {name} can appear with, in $."
            );
            IsTwoHanded = SyncedBindingExtensions.BindSyncedEntry(
                configFile,
                section,
                $"{name} Is Two-Handed",
                defaultTwoHanded,
                $"If true, {name} will require two hands to carry."
            );
            CarryWeight = SyncedBindingExtensions.BindSyncedEntry(
                configFile,
                section,
                $"{name} Carry Weight",
                defaultCarryWeight,
                $"The carry weight of {name}, in lb."
            );
            IsConductive = SyncedBindingExtensions.BindSyncedEntry(
                configFile,
                section,
                $"{name} Is Conductive",
                defaultConductive,
                $"If true, {name} will attract lightning."
            );
            foreach ((string moonName, Levels.LevelTypes levelId) in moons) {
                // int defaults to 0, so we only need to include the moons the scrap spawns on in the defaults thanks to this method
                defaultMoonWeights.TryGetValue(levelId, out int defaultSpawnWeight);
                SpawnWeights.Add(levelId, SyncedBindingExtensions.BindSyncedEntry(
                    configFile,
                    section,
                    $"{name} {moonName} Spawn Weight",
                    defaultSpawnWeight,
                    $"The weight for a {name} to spawn on {moonName}. The higher this value is, the more often the item will appear. Set to 0 to make the item never appear on {moonName}."
                ));
            }
        }
    }
}
