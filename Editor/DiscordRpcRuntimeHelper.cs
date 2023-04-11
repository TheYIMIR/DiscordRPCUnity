using UnityEditor.Callbacks;
using UnityEditor;

namespace DiscordRPCUnity
{
    [InitializeOnLoad]
    public static class DiscordRpcRuntimeHelper
    {
        static DiscordRpcRuntimeHelper()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }


        private static void LogPlayModeState(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredEditMode:
                    DiscordRPC.UpdateState(RpcState.EditorMode);
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    DiscordRPC.UpdateState(RpcState.PlayMode);
                    break;
                default:
                    break;
            }
        }
    }
}
