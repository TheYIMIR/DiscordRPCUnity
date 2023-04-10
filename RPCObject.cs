using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRPCUnity
{

    [System.Serializable]
    public class DiscordRPCData
    {
        public string id;
        public string largeImageKey;
        public string largeImageText;
        public string smallImageKey;
        public string smallImageText;
        public RPCData playMode;
        public RPCData editorMode;
    }

    [System.Serializable]
    public class RPCData
    {
        public string state;
    }
}
