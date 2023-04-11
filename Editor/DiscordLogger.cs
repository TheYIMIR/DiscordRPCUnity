using System;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace DiscordRPCUnity.Editor
{
    internal class DiscordLogger
    {
        public static void Log(Object message)
        {
            Debug.Log($"[DiscordRPCUnity] {message}");
        }

        public static void LogError(Object message)
        {
            Debug.LogError($"[DiscordRPCUnity] {message}");
        }

        public static void LogWarning(Object message)
        {
            Debug.LogWarning($"[DiscordRPCUnity] {message}");
        }
    }
}
