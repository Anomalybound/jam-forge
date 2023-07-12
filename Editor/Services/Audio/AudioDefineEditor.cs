using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JamForge.Audio
{
    [CustomEditor(typeof(AudioDefine))]
    public class AudioDefineEditor : Editor
    {
        [MenuItem("Assets/Create/JamForge/AudioDefine/Create Collection", false, 0)]
        public static void CreateDefineCollection()
        {
            const BindingFlags flag = BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic;

            var define = CreateInstance<AudioDefine>();
            var clipsToAdd = new List<AudioClip>();
            var isFolder = Selection.activeObject is DefaultAsset;
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var defaultName = "";
            if (isFolder)
            {
                defaultName = Selection.activeObject.name;
                var assets = AssetDatabase.FindAssets("t:AudioClip", new[] { path });
                var audioClips = assets.Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<AudioClip>).ToArray();

                clipsToAdd.AddRange(audioClips);
            }
            else
            {
                clipsToAdd.AddRange(Selection.GetFiltered<AudioClip>(SelectionMode.Assets));
                defaultName = clipsToAdd.First().name;
            }

            define.GetType().GetField("clips", flag)?.SetValue(define, clipsToAdd.ToArray());
            var savePath = EditorUtility.SaveFilePanelInProject("Create AudioDefine",
                $"AD_{defaultName}",
                "asset",
                "Create AudioDefine",
                path
            );

            if (string.IsNullOrEmpty(savePath)) { return; }

            AssetDatabase.CreateAsset(define, savePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/JamForge/AudioDefine/Create Individually", false, 1)]
        public static void CreateDefineIndividual()
        {
            const BindingFlags flag = BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic;

            var clipsToAdd = new List<AudioClip>();
            var isFolder = Selection.activeObject is DefaultAsset;
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (isFolder)
            {
                var assets = AssetDatabase.FindAssets("t:AudioClip", new[] { path });
                var audioClips = assets.Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<AudioClip>).ToArray();

                clipsToAdd.AddRange(audioClips);
            }
            else
            {
                clipsToAdd.AddRange(Selection.GetFiltered<AudioClip>(SelectionMode.Assets));
            }

            var folderPath = Path.GetDirectoryName(path);
            var savePath = EditorUtility.OpenFolderPanel("Create AudioDefine Individual",
                folderPath,
                "Create AudioDefine Individual"
            );

            var relativePathLength = savePath.Length - Application.dataPath.Length;
            savePath = savePath.Substring(Application.dataPath.Length, relativePathLength);

            if (string.IsNullOrEmpty(savePath)) { return; }

            savePath = $"Assets{savePath}";

            for (var i = 0; i < clipsToAdd.Count; i++)
            {
                var clip = clipsToAdd[i];
                var newDefine = CreateInstance<AudioDefine>();
                newDefine.name = clipsToAdd[i].name;
                newDefine.GetType().GetField("clips", flag)?.SetValue(newDefine, new[] { clip });

                var saveClipPath = $"{savePath}/AD_{clip.name}.asset";

                AssetDatabase.CreateAsset(newDefine, saveClipPath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/JamForge/AudioDefine/Create Collection", true)]
        [MenuItem("Assets/Create/JamForge/AudioDefine/Create Individually", true)]
        public static bool CreateAudioDefineValidation()
        {
            var isFolder = Selection.activeObject is DefaultAsset;
            var allClips = Selection.objects.All(s => s is AudioClip);

            return isFolder || allClips;
        }

        private SerializedProperty _clips;
        private SerializedProperty _loop;
        private SerializedProperty _overrideVolume;
        private SerializedProperty _overridePitch;
        private SerializedProperty _selectType;

        private SerializedProperty _crossFade;
        private SerializedProperty _volume;
        private SerializedProperty _pitchMin;
        private SerializedProperty _pitchMax;

        private void OnEnable()
        {
            _clips = serializedObject.FindProperty("clips");
            _loop = serializedObject.FindProperty("loop");
            _overrideVolume = serializedObject.FindProperty("overrideVolume");
            _overridePitch = serializedObject.FindProperty("overridePitch");
            _selectType = serializedObject.FindProperty("selectType");

            _crossFade = serializedObject.FindProperty("crossFade");
            _volume = serializedObject.FindProperty("volume");
            _pitchMin = serializedObject.FindProperty("pitchMin");
            _pitchMax = serializedObject.FindProperty("pitchMax");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            using var check = new EditorGUI.ChangeCheckScope();
            EditorGUILayout.PropertyField(_clips, true);
            EditorGUILayout.PropertyField(_loop);
            EditorGUILayout.PropertyField(_crossFade);
            if (_crossFade.floatValue < 0) { _crossFade.floatValue = 0; }

            EditorGUILayout.PropertyField(_selectType);
            EditorGUILayout.PropertyField(_overrideVolume);
            if (_overrideVolume.boolValue)
            {
                using (new EditorGUI.IndentLevelScope(1))
                {
                    EditorGUILayout.PropertyField(_volume);
                }
            }

            EditorGUILayout.PropertyField(_overridePitch);
            if (_overridePitch.boolValue)
            {
                using (new EditorGUI.IndentLevelScope(1))
                {
                    EditorGUILayout.PropertyField(_pitchMin);
                    EditorGUILayout.PropertyField(_pitchMax);
                }
            }

            if (check.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
