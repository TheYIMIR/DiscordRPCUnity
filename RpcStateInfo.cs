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
                    return DiscordRPC.discordRPCData.editorMode.state;
                case RpcState.PlayMode:
                    return DiscordRPC.discordRPCData.playMode.state;
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
