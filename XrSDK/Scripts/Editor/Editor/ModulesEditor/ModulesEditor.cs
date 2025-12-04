using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using System.Collections.Generic;

public class ModulesEditor : EditorWindow
{
    private Vector2 ad_ScrollPosition;//广告滑动列表
    private Vector2 attr_ScrollPosition;//归因滑动列表
    private float itemPaddiing = 2;//项目间的间距
    private GUIStyle titleBackground;

    private const string gitDownLoadURL = "https://raw.githubusercontent.com/yuhuaichong1/XrSdkTools";//git下载地址
    
    private const string versionTXT = "unitypackageVerMsg.txt";//版本说明

    private Dictionary<ModuleName, VesrionData> AdModuleMsgDict = new Dictionary<ModuleName, VesrionData>() 
    {
        { ModuleName.ApplovinMAX, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.Tradplus, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.KwaiNetwork, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.TopOn, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
    };
    private Dictionary<ModuleName, VesrionData> AttrMsgDict = new Dictionary<ModuleName, VesrionData>()
    {
        { ModuleName.Adjust, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.Appsflyer, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.SolarEngine, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
        { ModuleName.Tenjin, new VesrionData{ version = "loading...", sdkURL = "loading..." } },
    };


    private static bool ifGetVerTxt;

    [MenuItem("Tools/XrSdkEditor/Modules")]
    public static void ShowWindow()
    {
        ifGetVerTxt = true;
        GetWindow<ModulesEditor>("ModulesLoad");
    }

    void OnGUI()
    {
        #region GetMsg

        if (ifGetVerTxt)
        {
            EditorCoroutineUtility.StartCoroutine(PullVersionMsg(), this);
            ifGetVerTxt = false;
        }

        #endregion

        titleBackground = new GUIStyle(GUI.skin.label);
        titleBackground.normal.background = MakeTex(2, 2, new Color(0.16f, 0.16f, 0.16f, 0.5f));
        float itemWidth = Screen.width * 0.275f;

        #region Ad Modules

        GUILayout.Label("Ad Modules", new GUIStyle(EditorStyles.boldLabel){ fontSize = 24 });

        GUILayout.BeginHorizontal(titleBackground);
        GUILayout.Label("Name", GUILayout.Width(itemWidth));
        GUILayout.Label("LatestVersion", GUILayout.Width(itemWidth));
        GUILayout.Label("SdkURL", GUILayout.Width(itemWidth));
        GUILayout.Label("Operation", new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleRight });
        GUILayout.EndHorizontal();

        ad_ScrollPosition = GUILayout.BeginScrollView(ad_ScrollPosition, EditorStyles.helpBox);

        foreach (ModuleName mn in AdModuleMsgDict.Keys)
        {
            string name = VersionDataToString(mn);

            GUILayout.Space(itemPaddiing);

            GUILayout.BeginHorizontal();
            GUILayout.Label(name, GUILayout.Width(itemWidth));
            GUILayout.Label($"{AdModuleMsgDict[mn].version}", GUILayout.Width(itemWidth));
            //GUILayout.Label($"{AdModuleMsgDict[mn].sdkURL}", GUILayout.Width(itemWidth));
            DrawHyperLink(AdModuleMsgDict[mn].sdkURL, AdModuleMsgDict[mn].sdkURL, GUILayout.Width(itemWidth));
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Install", GUILayout.Width(60)))
            {
                //EditorCoroutineUtility.StartCoroutine(PullPackage("main", "AppsflyerTest.unitypackage"), this);
                EditorCoroutineUtility.StartCoroutine(PullPackage("packages", $"{name}.unitypackage"), this);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        #endregion

        GUILayout.Space(10);

        #region Attribution Modules

        GUILayout.Label("Attribution Modules", new GUIStyle(EditorStyles.boldLabel) { fontSize = 24 });

        GUILayout.BeginHorizontal(titleBackground);
        GUILayout.Label("Name", GUILayout.Width(itemWidth));
        GUILayout.Label("LatestVersion", GUILayout.Width(itemWidth));
        GUILayout.Label("SdkURL", GUILayout.Width(itemWidth));
        GUILayout.Label("Operation", new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleRight });
        GUILayout.EndHorizontal();

        attr_ScrollPosition = GUILayout.BeginScrollView(attr_ScrollPosition, EditorStyles.helpBox);

        foreach (ModuleName mn in AttrMsgDict.Keys)
        {
            string name = VersionDataToString(mn);

            GUILayout.Space(itemPaddiing);

            GUILayout.BeginHorizontal();
            GUILayout.Label(name, GUILayout.Width(itemWidth));
            GUILayout.Label($"{AttrMsgDict[mn].version}", GUILayout.Width(itemWidth));
            //GUILayout.Label($"{AttrMsgDict[mn].sdkURL}", GUILayout.Width(itemWidth));
            DrawHyperLink(AttrMsgDict[mn].sdkURL, AttrMsgDict[mn].sdkURL, GUILayout.Width(itemWidth));
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Install", GUILayout.Width(60)))
            {
                //EditorCoroutineUtility.StartCoroutine(PullPackage("main", "AppsflyerTest.unitypackage"), this);
                EditorCoroutineUtility.StartCoroutine(PullPackage("main", $"{name}.unitypackage"), this);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        #endregion
    }

    /// <summary>
    /// 超链接文本显示
    /// </summary>
    /// <param name="text">显示文本</param>
    /// <param name="url">超链接地址</param>
    /// <param name="options">其他选项</param>
    private void DrawHyperLink(string text, string url, params GUILayoutOption[] options)
    {
        // 1. 保存当前GUI状态（避免影响其他UI）
        var originalColor = GUI.color;
        var originalFontStyle = EditorStyles.label.fontStyle;

        // 2. 计算文本绘制区域（用于事件检测）
        Rect labelRect = GUILayoutUtility.GetRect(new GUIContent(text), EditorStyles.label, options);

        // 3. 事件检测（鼠标悬浮/点击）
        Event currentEvent = Event.current;
        bool isHover = labelRect.Contains(currentEvent.mousePosition);

        // 4. 设置hover视觉效果
        if (isHover)
        {
            GUI.color = Color.blue; // hover时变蓝色
            EditorStyles.label.fontStyle = FontStyle.Italic; // 下划线
            EditorGUIUtility.AddCursorRect(labelRect, MouseCursor.Link); // 鼠标变链接样式
        }
        else
        {
            GUI.color = originalColor; // 恢复默认颜色
            EditorStyles.label.fontStyle = originalFontStyle; // 恢复默认字体样式
        }

        // 5. 绘制文本
        EditorGUI.LabelField(labelRect, text, EditorStyles.label);

        // 6. 处理点击事件（跳转链接）
        if (isHover && currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
        {
            Application.OpenURL(url); // 打开默认浏览器访问链接
        }

        // 7. 恢复GUI状态
        GUI.color = originalColor;
        EditorStyles.label.fontStyle = originalFontStyle;
    }

    #region 其他

    /// <summary>
    /// 辅助方法：创建纯色纹理
    /// </summary>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <param name="col">颜色</param>
    /// <returns>纯色纹理</returns>
    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    /// <summary>
    /// 获取远端的unitypackage包并进行导入
    /// </summary>
    /// <param name="branch">分支名</param>
    /// <param name="unitypackageName">包名</param>
    /// <returns></returns>
    private IEnumerator PullPackage(string branch, string unitypackageName)
    {
        string wholeURL = Path.Combine(gitDownLoadURL, branch, unitypackageName);
        string outputDir = Path.Combine(Application.dataPath, "XrSDK");
        string outputPath = Path.Combine(outputDir, unitypackageName);

        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        using (UnityWebRequest request = UnityWebRequest.Get(wholeURL))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"下载失败：{request.error}");
                yield break;
            }

            File.WriteAllBytes(outputPath, request.downloadHandler.data);
            Debug.Log($"下载成功：{outputPath}");

            AssetDatabase.ImportPackage(outputPath, true);
            if (File.Exists(outputPath))
            {
                // 延迟一小段时间（避免导入未完全结束导致文件占用）
                yield return new WaitForEndOfFrame();

                File.Delete(outputPath);
                Debug.Log($"已自动删除本地资源包文件：{outputPath}");
            }
            else
            {
                Debug.LogWarning($"未找到要删除的文件：{outputPath}");
            }
            AssetDatabase.Refresh();
        }
    }

    /// <summary>
    /// 获取版本信息
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullVersionMsg()
    {
        string txtPath = Path.Combine(gitDownLoadURL, "packages", versionTXT);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(txtPath))
        {
            // 发送请求并等待响应
            yield return webRequest.SendWebRequest();

            // 处理响应结果
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // 成功：获取文本内容
                string textContent = webRequest.downloadHandler.text;
                Debug.Log("XrSdk版本文档读取成功!");

                string[] content = textContent.Split("\n");
                foreach(string verMsg in content)
                {
                    string[] temp = verMsg.Split(",");
                    if(Enum.TryParse<ModuleName>(temp[0], out ModuleName result))
                    {
                        int type = int.Parse(temp[1]);
                        VesrionData vesrionData = type == 0 ? AdModuleMsgDict[result] : AttrMsgDict[result];
                        vesrionData.version = $"{temp[2]}";
                        vesrionData.sdkURL = $"{temp[3]}";
                    }
                }
                Repaint();
            }
            else
            {
                // 失败：打印错误信息
                Debug.LogError($"XrSdk版本文档读取失败! 错误码: {webRequest.responseCode}, 原因: {webRequest.error}");
            }
        }
    }

    /// <summary>
    /// 将枚举ModuleName转为string
    /// </summary>
    /// <param name="moduleName">枚举</param>
    /// <returns>名称</returns>
    private string VersionDataToString(ModuleName moduleName)
    {
        return moduleName.ToString();
    }

    #endregion
}
