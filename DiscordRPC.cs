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

        public static DiscordRPCData discordRPCData = LoadDiscordRPCData();

        static DiscordRPC()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(Resources.Load<TextAsset>("DiscordRPCData"))).Replace("Resources/DiscordRPCData.json", "Plugins/");
            if (!File.Exists(path + "discord-rpc.dll"))
            {
                Log($"discord-rpc.dll does not Exist. Save it now to {path + "discord-rpc.dll"}!");
                DllContainer dllContainer = new DllContainer(path);
                dllContainer.SaveEmbeddedDll("DiscordRPCUnity.discord-rpc.dll", "discord-rpc.dll");
            }

            if (!EditorPrefs.HasKey("discordRPC"))
            {
                EditorPrefs.SetBool("discordRPC", true);
            }

            if (EditorPrefs.GetBool("discordRPC"))
            {
                Log("Starting Discord RPC");
                DiscordAPI.EventHandlers eventHandlers = default(DiscordAPI.EventHandlers);
                DiscordAPI.Initialize(discordRPCData.id, ref eventHandlers, false, string.Empty);
                UpdateDRPC();
            }
        }

        public static void UpdateDRPC()
        {
            Log("Updating everything");
            presence.details = string.Format("Project: {0}", GameName);
            if (rpcState.StateName() != null) presence.state = rpcState.StateName();
            presence.startTimestamp = timestamp;
            if (discordRPCData.largeImageKey != null) presence.largeImageKey = discordRPCData.largeImageKey;
            if (discordRPCData.largeImageText != null) presence.largeImageText = discordRPCData.largeImageText;
            if (discordRPCData.smallImageKey != null) presence.smallImageKey = discordRPCData.smallImageKey;
            if (discordRPCData.smallImageText != null) presence.smallImageText = discordRPCData.smallImageText;
            DiscordAPI.UpdatePresence(presence);
            ResetTime();
        }

        public static void UpdateState(RpcState state)
        {
            Log("Updating state to '" + state.StateName() + "'");
            rpcState = state;
            if (rpcState.StateName() != null) presence.state = rpcState.StateName();
            DiscordAPI.UpdatePresence(presence);
            ResetTime();
        }

        public static void ResetTime()
        {
            Log("Resetting timer");
            time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            timestamp = (long)time.TotalSeconds;
            presence.startTimestamp = timestamp;
            DiscordAPI.UpdatePresence(presence);
        }

        private static void Log(string message)
        {
            Debug.Log("[DiscordRPCUnity] " + message);
        }

        public static DiscordRPCData LoadDiscordRPCData()
        {
            TextAsset json = Resources.Load<TextAsset>("DiscordRPCData"); // Load the JSON file from Resources folder
            if (json != null)
            {
                DiscordRPCData data = JsonUtility.FromJson<DiscordRPCData>(json.ToString()); // Deserialize the JSON into DiscordRPCData object
                return data;
            }
            else
            {
                Debug.LogError("Failed to load DiscordRPCData.json from Resources folder!");
                return null;
            }
        }

        public static void SaveDiscordRPCData()
        {
            string json = JsonUtility.ToJson(discordRPCData, true);
            string path = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(Resources.Load<TextAsset>("DiscordRPCData")));
            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
            discordRPCData = LoadDiscordRPCData();
            Log("Discord RPC data saved to: " + path);
        }

    }
}
