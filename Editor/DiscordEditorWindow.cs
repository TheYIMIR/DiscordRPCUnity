using DiscordRPCUnity.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace DiscordRPCUnity
{
    public class DiscordRPCDataEditor : EditorWindow
    {
        Vector2 scrollPos;

        [MenuItem("Window/DiscordRPC")]
        public static void ShowWindow()
        {
            DiscordRPCDataEditor window = GetWindow<DiscordRPCDataEditor>();
            window.titleContent = new GUIContent("Editor");
            window.minSize = new Vector2(400, 400);
            window.Show();
        }

        private void OnEnable()
        {
            DiscordRPC.discordRPCs = DiscordRPC.LoadDiscordRPCDataObjects();
        }

        private void OnGUI()
        {
            GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = 14;
            labelStyle.normal.textColor = Color.white;

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Editor/Discord/Resources/DiscordData")))
            {
                GUILayout.Label("Structure", labelStyle);
                if (GUILayout.Button("Setup"))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Editor/Discord/Resources/DiscordData"));
                    DiscordLogger.Log($"Setup complete! Folder created at Path: {Path.Combine(Directory.GetCurrentDirectory(), "Assets/Scripts/Editor/Discord/Resources/DiscordData")}");
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.Space(10);

                if (EditorPrefs.GetBool("discordRPC"))
                {
                    GUILayout.Label("DiscordRPC", labelStyle);
                    if (GUILayout.Button("Reload"))
                    {
                        DiscordRPC.Reload();
                    }
                    GUILayout.Space(20);
                }

                GUILayout.Label("Settings", labelStyle);

                if(DiscordRPC.discordRPCs.Length > 1)
                {
                    EditorGUI.BeginChangeCheck();
                    DiscordRPC.slectedRPC = EditorGUILayout.IntSlider("Selected RPCData:", DiscordRPC.slectedRPC, 0, DiscordRPC.discordRPCs.Length - 1);
                    if (EditorGUI.EndChangeCheck())
                    {
                        DiscordRPC.Update(false);
                    }
                    GUILayout.Space(20);
                }

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(100));

                for (int i = 0; i < DiscordRPC.discordRPCs.Length; i++)
                {
                    DiscordRPC.discordRPCs[i] = (DiscordRPCDataObject)EditorGUILayout.ObjectField($"{DiscordRPC.discordRPCs[i].name}:", DiscordRPC.discordRPCs[i], typeof(DiscordRPCDataObject), true);
                }

                EditorGUILayout.EndScrollView();

                GUILayout.Space(5);

                if (GUILayout.Button("Refresh", GUILayout.Width(90)))
                {
                    DiscordRPC.discordRPCs = DiscordRPC.LoadDiscordRPCDataObjects();
                    Repaint();
                }

                GUILayout.Space(10);
                GUILayout.EndVertical();
            }
        }
    }
}
