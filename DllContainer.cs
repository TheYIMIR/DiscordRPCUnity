using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace DiscordRPCUnity
{
    public class DllContainer
    {
        private string folderPath;

        public DllContainer(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public void SaveEmbeddedDll(string resourceName, string dllName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    throw new Exception($"Embedded resource '{resourceName}' not found.");
                }

                byte[] dllBytes = new byte[resourceStream.Length];
                resourceStream.Read(dllBytes, 0, dllBytes.Length);

                if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fullPath = Path.Combine(folderPath, dllName);
                File.WriteAllBytes(fullPath, dllBytes);
                AssetDatabase.Refresh();
            }
        }
    }
}
