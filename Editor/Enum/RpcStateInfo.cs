using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRPCUnity
{
    public static class RpcStateInfo
    {
        public static string StateName(this RpcState state)
        {
            switch (state)
            {
                case RpcState.EditorMode:
                    return DiscordRPC.discordRPCs[DiscordRPC.slectedRPC].EditorModeState;
                case RpcState.PlayMode:
                    return DiscordRPC.discordRPCs[DiscordRPC.slectedRPC].PlayModeState;
                default:
                    return "Error";
            }
        }
    }

    public enum RpcState
    {
        EditorMode = 0,
        PlayMode = 1
    }
}
