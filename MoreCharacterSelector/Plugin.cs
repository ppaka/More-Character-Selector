using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ModelReplacement;
using UnityEngine;

//using System.Numerics;

namespace MoreCharacterSelector
{




    [BepInPlugin("Humyo.MoreCharacterSelectorReplacement", "More Character Selector", "0.0.1")]
    [BepInDependency("meow.ModelReplacementAPI", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigFile config;

        // Universal config options 
        public static ConfigEntry<bool> enableForAllSuits { get; private set; }
        public static ConfigEntry<bool> enableAsDefault { get; private set; }
        public static ConfigEntry<string> suitNamesToEnable { get; private set; }
        
        //  model specific config options
        public static ConfigEntry<float> UpdateRate { get; private set; }
        public static ConfigEntry<float> distanceDisablePhysics { get; private set; }
        public static ConfigEntry<bool> disablePhysicsAtRange { get; private set; }

        private static void InitConfig()
        {
            enableForAllSuits = config.Bind<bool>("Suits to Replace Settings", "Enable all Suits", false, "Enable to replace every suit. Set to false to specify suits");
            enableAsDefault = config.Bind<bool>("Suits to Replace Settings", "Enable as default", false, "Enable to replace every suit that hasn't been otherwise registered with .");
            suitNamesToEnable = config.Bind<string>("Suits to Replace Settings", "Suits to enable", "Default,Orange suit,주황색슈트", "Enter a comma separated list of suit names.(Additionally, [Green suit,Pajama suit,Hazard suit]한국어 패치했을시[초록색슈트,파자마슈트,방호복슈트])");

            UpdateRate = config.Bind<float>("Dynamic Bone Settings", "Update rate", 60, "Refreshes dynamic bones more times per second the higher the number");
            disablePhysicsAtRange = config.Bind<bool>("Dynamic Bone Settings", "Disable physics at range", false, "Enable to disable physics past the specified range");
            distanceDisablePhysics = config.Bind<float>("Dynamic Bone Settings", "Distance to disable physics", 20, "If Disable physics at range is enabled, this is the range after which physics is disabled.");
            
        }
        private void Awake()
        {
            config = base.Config;
            InitConfig();
            Assets.PopulateAssets();

            // Plugin startup logic


            if (enableForAllSuits.Value)
            {
                ModelReplacementAPI.RegisterModelReplacementOverride(typeof(BodyReplacement));

            }
            if (enableAsDefault.Value)
            {
                ModelReplacementAPI.RegisterModelReplacementDefault(typeof(BodyReplacement));

            }

            var commaSepList = suitNamesToEnable.Value.Split(',');
            foreach (var item in commaSepList)
            {
                ModelReplacementAPI.RegisterSuitModelReplacement(item, typeof(BodyReplacement));
            }
                

            Harmony harmony = new Harmony("Humyo.MCSReplacement");
            harmony.PatchAll();
            Logger.LogInfo("Plugin Humyo.MCSReplacement is loaded!");
        }
    }
    public static class Assets
    {
        // Replace mbundle with the Asset Bundle Name from your unity project 
        public static string mainAssetBundleName = "akula";
        public static AssetBundle? MainAssetBundle = null;

        private static string GetAssemblyName() => Assembly.GetExecutingAssembly().GetName().Name;
        public static void PopulateAssets()
        {
            if (MainAssetBundle != null) return;
            Console.WriteLine(GetAssemblyName() + "." + mainAssetBundleName);
            using var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyName() + "." + mainAssetBundleName);
            MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
        }
    }

}