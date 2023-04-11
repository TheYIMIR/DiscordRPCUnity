using DiscordRPCUnity.Editor;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DiscordRPCUnity
{
    [InitializeOnLoad]
    public class DiscordRPC
    {
        private static readonly DiscordAPI.RichPresence presence = new DiscordAPI.RichPresence();

        private static TimeSpan time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        private static long timestamp = (long)time.TotalSeconds;

        private static RpcState rpcState = RpcState.EditorMode;
        private static string GameName = Application.productName;

        public static int slectedRPC = 0;

        public static DiscordRPCDataObject[] discordRPCs = LoadDiscordRPCDataObjects();

        static DiscordRPC()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Editor/Discord/Plugins/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
            if (!File.Exists(path + "discord-rpc.dll"))
            {
                DiscordLogger.Log($"discord-rpc.dll does not Exist. Save it now to {path + "discord-rpc.dll"}!");
                DllContainer dllContainer = new DllContainer(path);
                dllContainer.SaveEmbeddedDll("DiscordRPCUnity.Resources.discord-rpc.dll", "discord-rpc.dll");
            }

            if (discordRPCs.Length > 0)
            {
                Initialize();
            }
        }

        public static void Initialize()
        {
            if (discordRPCs.Length <= 0)
            {
                DiscordLogger.LogWarning("Not able to Initialize because there is no RPCData!");
                return;
            }
            if (!EditorPrefs.HasKey("discordRPC"))
            {
                EditorPrefs.SetBool("discordRPC", true);
            }

            if (EditorPrefs.GetBool("discordRPC"))
            {
                DiscordLogger.Log("Starting Discord RPC");
                DiscordAPI.EventHandlers eventHandlers = default(DiscordAPI.EventHandlers);
                DiscordAPI.Initialize(discordRPCs[slectedRPC].ID, ref eventHandlers, false, string.Empty);
                Update();
            }
        }

        public static void Reload()
        {
            DiscordAPI.Shutdown();
            Initialize();
        }

        public static void Update(bool reset = true)
        {
            if(!EditorPrefs.GetBool("discordRPC")) Initialize();
            DiscordLogger.Log("Updating everything");
            presence.details = string.Format("Project: {0}", GameName);
            if (rpcState.StateName() != null) presence.state = rpcState.StateName();
            presence.startTimestamp = timestamp;
            if (discordRPCs[slectedRPC].LargeImageKey != null) presence.largeImageKey = discordRPCs[slectedRPC].LargeImageKey;
            if (discordRPCs[slectedRPC].LargeImageText != null) presence.largeImageText = discordRPCs[slectedRPC].LargeImageText;
            if (discordRPCs[slectedRPC].SmallImageKey != null) presence.smallImageKey = discordRPCs[slectedRPC].SmallImageKey;
            if (discordRPCs[slectedRPC].SmallImageText != null) presence.smallImageText = discordRPCs[slectedRPC].SmallImageText;
            DiscordAPI.UpdatePresence(presence);
            if (reset) ResetTime();
        }

        public static void UpdateState(RpcState state, bool reset = true)
        {
            DiscordLogger.Log("Updating state to '" + state.StateName() + "'");
            rpcState = state;
            if (rpcState.StateName() != null) presence.state = rpcState.StateName();
            DiscordAPI.UpdatePresence(presence);
            if (reset) ResetTime();
        }

        public static void ResetTime()
        {
            DiscordLogger.Log("Resetting timer");
            time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            timestamp = (long)time.TotalSeconds;
            presence.startTimestamp = timestamp;
            DiscordAPI.UpdatePresence(presence);
        }

        public static DiscordRPCDataObject[] LoadDiscordRPCDataObjects()
        {
            return Resources.LoadAll<DiscordRPCDataObject>("DiscordData");
        }
    }
}
