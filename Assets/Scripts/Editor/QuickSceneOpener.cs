using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;
using System.IO;

public class QuickSceneOpener : EditorWindow
{
    [MenuItem("Ninja/Tools/Quick Scene Opener")]
    public static void ShowWindow()
    {
        GetWindow<QuickSceneOpener>("Quick Scene Opener");
    }

    [MenuItem("Tools/New Window")]
    public static void OpenMyWindow()
    {
        var window = EditorWindow.CreateWindow<QuickSceneOpener>();
        window.titleContent = new GUIContent("OpenScene");
    }

    private void OnGUI()
    {
        GUILayout.Label("Open Common Scenes", EditorStyles.boldLabel);

        DrawSceneButton("Login");
        DrawSceneButton("Game");
        

        GUILayout.Space(10);
        GUILayout.Label("Run Game", EditorStyles.boldLabel);

        if (GUILayout.Button("Start Game (Login Scene)",GUILayout.Height(30)))
        {
            RunGameFromScene("Login");
        }

        GUILayout.Space(10);
        GUILayout.Label("Editor Control", EditorStyles.boldLabel);

        if (EditorApplication.isPlaying)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("■ Stop Play Mode", GUILayout.Height(30)))
            {
                EditorApplication.isPlaying = false;
            }

            GUILayout.EndHorizontal();
            if (EditorApplication.isPaused)
            {
                if (GUILayout.Button("▶ Resume"))
                {
                    EditorApplication.isPaused = false;
                }
            }
            else
            {
                if (GUILayout.Button("⏸ Pause"))
                {
                    EditorApplication.isPaused = true;
                }
            }
        }
        else
        {
            GUILayout.Label("Not in Play Mode", EditorStyles.helpBox);
        }

        GUILayout.Space(10);
        GUILayout.Label("Build Tools", EditorStyles.boldLabel);

        Rect totalRect = GUILayoutUtility.GetRect(150, 25); // Vùng tổng cho 2 nút
        Rect mainBtnRect = new Rect(totalRect.x, totalRect.y, totalRect.width - 25, totalRect.height);
        Rect dropdownRect = new Rect(mainBtnRect.xMax, totalRect.y, 25, totalRect.height);

        // Nút chính
        if (GUI.Button(mainBtnRect, "Build"))
        {
            BuildMain(); // hàm chính để build
        }

        // Nút phụ (dropdown)
        if (GUI.Button(dropdownRect, "▼"))
        {
            ShowBuildMenu(dropdownRect);
        }
    }
    private void ShowBuildMenu(Rect buttonRect)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Build Windows"), false, () => BuildForTarget(BuildTarget.StandaloneWindows64));
        menu.AddItem(new GUIContent("Build Android"), false, () => BuildForTarget(BuildTarget.Android));
        menu.AddItem(new GUIContent("Clean Build Cache"), false, CleanBuildCache);
        menu.ShowAsContext(); // hoặc .DropDown(buttonRect);
    }
    private void BuildForTarget(BuildTarget target)
    {
        // Hiện popup cho người dùng chọn thư mục lưu
        string folderPath = EditorUtility.SaveFolderPanel("Chọn thư mục lưu bản build", "", "");

        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogWarning("❌ Huỷ build: Không có thư mục được chọn.");
            return;
        }

        // Tên file build đầu ra
        string fileName = target == BuildTarget.Android ? "NinjaHuyenThoai.apk" : "NinjaHuyenThoai.exe";
        string fullPath = Path.Combine(folderPath, fileName);

        // Cảnh báo nếu ghi đè
        if (File.Exists(fullPath) && !EditorUtility.DisplayDialog("Xác nhận ghi đè",
            $"File {fileName} đã tồn tại. Ghi đè?", "Ghi đè", "Huỷ"))
        {
            Debug.Log("❌ Đã huỷ build.");
            return;
        }

        // Thiết lập build
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = new[]
            {
            "Assets/Scenes/LoadData.unity",
            "Assets/Scenes/Login.unity",
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/BackLogin.unity"
        },
            locationPathName = fullPath,
            target = target,
            targetGroup = BuildPipeline.GetBuildTargetGroup(target),
            options = BuildOptions.None
        };

        Debug.Log($"🔨 Bắt đầu build cho {target} → {fullPath}");

        // Tiến hành build
        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        BuildSummary summary = report.summary;

        // Thông báo kết quả
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ Build thành công ({summary.totalSize / 1024 / 1024} MB)");
            EditorUtility.RevealInFinder(fullPath); // Mở thư mục sau khi build xong
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("❌ Build thất bại!");
            EditorUtility.DisplayDialog("Build thất bại", "Xem log console để biết chi tiết.", "OK");
        }
    }
    private void BuildMain()
    {
        BuildForTarget(BuildTarget.StandaloneWindows64);
    }

    private void CleanBuildCache()
    {
        // Nếu bạn dùng Addressables
#if UNITY_ADDRESSABLES
    UnityEditor.AddressableAssets.Settings.AddressableAssetSettings.CleanPlayerContent();
    Debug.Log("🧹 Đã dọn sạch Addressables Build Cache.");
#else
        // Nếu dùng BuildPipeline cache
        string cachePath = "Library/ScriptAssemblies";
        if (Directory.Exists(cachePath))
        {
            Directory.Delete(cachePath, true);
            Debug.Log("🧹 Đã xoá Build Cache thủ công.");
        }
        else
        {
            Debug.Log("⚠️ Không tìm thấy cache để xoá.");
        }
#endif
    }
    private void DrawSceneButton(string sceneName)
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button($"Open {sceneName}"))
        {
            OpenSceneByName(sceneName);
        }

        if (GUILayout.Button($"Run {sceneName}", GUILayout.Width(100)))
        {
            RunGameFromScene(sceneName);
        }

        GUILayout.EndHorizontal();
    }

    private void OpenSceneByName(string sceneName)
    {
        string[] guids = AssetDatabase.FindAssets($"{sceneName} t:Scene");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == sceneName)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(path);
                }
                return;
            }
        }
        Debug.LogError($"Scene '{sceneName}' not found in project.");
    }

    private void RunGameFromScene(string sceneName)
    {
        string[] guids = AssetDatabase.FindAssets($"{sceneName} t:Scene");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == sceneName)
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                EditorApplication.EnterPlaymode();
                return;
            }
        }
        Debug.LogError($"Scene '{sceneName}' not found for Play Mode.");
    }
}
