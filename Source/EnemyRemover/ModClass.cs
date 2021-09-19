using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UObject = UnityEngine.Object;
using UnityEngine.SceneManagement;
using System.Reflection;
using HutongGames.PlayMaker.Actions;
using IL;
using Modding.Delegates;

namespace EnemyRemover
{
    public class EnemyRemover : Mod, ITogglableMod
    {
        internal static EnemyRemover Instance;

        //i'm not really sure what this means, but it gives the version from the .csproj file
        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;
            //adds the function to the modhook for when new enemies are spawned or else it wouldn't work
            ModHooks.OnEnableEnemyHook += removeEnemy;

            Log("Initialized");
        }
        public void Unload()
        {
            //removes the function from the modhook, because we don't want the enemies to disappear when the mod is turned off
            ModHooks.OnEnableEnemyHook -= removeEnemy;
        }
        private bool removeEnemy(GameObject enemy, bool isAlreadyDead)
        {
            //destroys the new enemy, and then tells it that it's dead
            UObject.Destroy(enemy);
            return true;
        }
    }
}