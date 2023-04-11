using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiscordRPCUnity.Editor
{
    [CreateAssetMenu(fileName = "RPCData", menuName = "DiscordRPCDataObject", order = 1)]
    public class DiscordRPCDataObject : ScriptableObject
    {
        public string ID = "your-id";
        public string LargeImageKey = "you-key";
        public string LargeImageText = "DiscordRPCUnity";
        public string SmallImageKey = null;
        public string SmallImageText = null;

        #region EditorMode
        public string EditorModeState = "Idle in EditorMode";
        #endregion
        #region PlayMode
        public string PlayModeState = "Testing in PlayMode";
        #endregion
    }
}
