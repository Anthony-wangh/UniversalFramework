using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Project.Editor
{
    public static class FindReferences
    {
        private static EditorApplication.CallbackFunction _updateDelegate;

        public delegate List<string> ThreadRun(ThreadPars par);

        private const int ThreadCount = 4;

        public class ThreadPars
        {
            public List<string> CheckList = new List<string>();
            public string AimGuid;
        }

        private static List<string> ThreadFind(ThreadPars par)
        {
            List<string> ret = new List<string>();
            if (par != null)
            {
                foreach (var file in par.CheckList)
                {
                    if (Regex.IsMatch(File.ReadAllText(file), par.AimGuid))
                    {
                        ret.Add(file);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// unity 2017版本之后 使用的方法
        /// </summary>
        public static void ClearConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type logEntries = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            if (clearConsoleMethod != null) clearConsoleMethod.Invoke(new object(), null);
        }

        [MenuItem("Assets/Find References Thread", false, 10)]
        public static void FindThread()
        {
            ClearConsole();
            EditorSettings.serializationMode = SerializationMode.ForceText;
            AssetDatabase.Refresh();
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path))
            {
                string guid = AssetDatabase.AssetPathToGUID(path);
                List<string> withoutExtensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset" };
                string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => withoutExtensions.Contains(Path.GetExtension(s)?.ToLower())).ToArray();

                ThreadPars[] threadParses = new ThreadPars[ThreadCount];
                for (int i = 0; i < ThreadCount; i++)
                {
                    threadParses[i] = new ThreadPars {AimGuid = guid};
                }

                for (int i = 0; i < files.Length; i++)
                {
                    int index = i % ThreadCount;
                    threadParses[index].CheckList.Add(files[i]);
                }

                ThreadRun[] tRun = new ThreadRun[ThreadCount];
                int finishedState = ThreadCount;

                IAsyncResult[] results = new IAsyncResult[ThreadCount];

                _updateDelegate = delegate
                {
                    var finishedCount = 0;
                    for (int i = 0; i < ThreadCount; i++)
                    {
                        if (results[i].IsCompleted) ++finishedCount;
                    }

                    EditorUtility.DisplayProgressBar("匹配资源中", $"进度：{finishedCount}", finishedCount * 1f / ThreadCount);

                    if (finishedCount >= finishedState)
                    {
                        List<string> re = new List<string>();
                        for (int i = 0; i < ThreadCount; i++)
                        {
                            re.AddRange(tRun[i].EndInvoke(results[i]));
                        }

                        foreach (var s in re)
                        {
                            Debug.Log(s, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(s)));
                        }
                        EditorUtility.ClearProgressBar();
                        EditorApplication.update -= _updateDelegate;
                    }
                };

                for (int i = 0; i < ThreadCount; i++)
                {
                    tRun[i] = ThreadFind;
                    results[i] = tRun[i].BeginInvoke(threadParses[i], null, null);
                }

                EditorApplication.update += _updateDelegate;
            }

        }

        private static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }
    }
}