using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace DiscordRPCUnity
{
    public class DiscordRPCDataEditor : EditorWindow
    {
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
            DiscordRPC.discordRPCData = DiscordRPC.LoadDiscordRPCData();
        }

        private void OnGUI()
        {
            GUILayout.Space(20);

            GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.fontSize = 18;
            titleStyle.normal.textColor = Color.white;
            titleStyle.margin = new RectOffset(0, 0, 0, 20);

            GUIStyle groupStyle = new GUIStyle(GUI.skin.box);
            groupStyle.padding = new RectOffset(10, 10, 10, 10);

            GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.fontSize = 14;
            labelStyle.normal.textColor = Color.white;

            GUIStyle textFieldStyle = new GUIStyle(EditorStyles.textField);
            textFieldStyle.fontSize = 14;
            textFieldStyle.normal.textColor = Color.white;
            textFieldStyle.margin = new RectOffset(0, 0, 5, 5);

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 16;
            buttonStyle.fixedHeight = 35;
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn.png") as Texture2D;
            buttonStyle.hover.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn on.png") as Texture2D;
            buttonStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn on.png") as Texture2D;

            GUILayout.Label("DiscordRPC Editor", titleStyle);

            GUILayout.Space(20);

            GUILayout.BeginVertical(groupStyle);

            GUILayout.Label("Image Settings", labelStyle);
            DiscordRPC.discordRPCData.largeImageKey = EditorGUILayout.TextField("Large Image Key", DiscordRPC.discordRPCData.largeImageKey, textFieldStyle);
            DiscordRPC.discordRPCData.largeImageText = EditorGUILayout.TextField("Large Image Text", DiscordRPC.discordRPCData.largeImageText, textFieldStyle);
            DiscordRPC.discordRPCData.smallImageKey = EditorGUILayout.TextField("Small Image Key", DiscordRPC.discordRPCData.smallImageKey, textFieldStyle);
            DiscordRPC.discordRPCData.smallImageText = EditorGUILayout.TextField("Small Image Text", DiscordRPC.discordRPCData.smallImageText, textFieldStyle);

            GUILayout.Space(10);

            GUILayout.Label("Play Mode Settings", labelStyle);
            DiscordRPC.discordRPCData.playMode.state = EditorGUILayout.TextField("Play Mode State", DiscordRPC.discordRPCData.playMode.state, textFieldStyle);

            GUILayout.Space(10);

            GUILayout.Label("Editor Mode Settings", labelStyle);
            DiscordRPC.discordRPCData.editorMode.state = EditorGUILayout.TextField("Editor Mode State", DiscordRPC.discordRPCData.editorMode.state, textFieldStyle);

            GUILayout.EndVertical();

            GUILayout.Space(20);

            // Save button
            if (GUILayout.Button("Save", buttonStyle))
            {
                DiscordRPC.SaveDiscordRPCData();
            }
        }
    }
}
