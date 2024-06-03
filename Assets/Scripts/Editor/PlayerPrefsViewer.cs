using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsViewer : EditorWindow
{
    private Vector2 scrollPosition;
    private string newKey = "";
    private string newValue = "";
    private int selectedTypeIndex = 0;
    private string[] typeOptions = { "String", "Int" };
    private Dictionary<string, (string Value, PlayerPrefsManager.ValueType Type)> playerPrefsCache = new Dictionary<string, (string, PlayerPrefsManager.ValueType)>();
    private List<string> keysToRemove = new List<string>(); // ������ Ű���� ������ ����Ʈ

    [MenuItem("Tools/Player Prefs Viewer")]
    public static void ShowWindow()
    {
        var window = GetWindow<PlayerPrefsViewer>("Player Prefs Viewer");
        window.maxSize = new Vector2(495, window.maxSize.y);
        window.minSize = new Vector2(495, 300);
        window.Show();
    }

    void OnEnable() // â�� �����ų� ��Ŀ���� ���� �� �̺�Ʈ ����
    {
        PlayerPrefsManager.OnPreferencesUpdated += RefreshPlayerPrefsCache;
    }

    void OnDisable() // â�� �����ų� ��Ŀ���� ���� �� �޸� ������ �����ϱ� ���� �̺�Ʈ ���� ����
    {
        PlayerPrefsManager.OnPreferencesUpdated -= RefreshPlayerPrefsCache;
    }

    void OnGUI()
    {
        GUILayout.Label("Ver. 1.0.0", EditorStyles.boldLabel);
        DrawLine();

        if (GUILayout.Button("Refresh List"))
        {
            RefreshPlayerPrefsCache();
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var kvp in playerPrefsCache)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(kvp.Key, GUILayout.Width(100));
            GUILayout.Label(kvp.Value.Value, GUILayout.Width(200));
            GUILayout.Label(kvp.Value.Type.ToString(), GUILayout.Width(50));

            if (GUILayout.Button("Modify", GUILayout.Width(60)))
            {
                PlayerPrefsEditWindow.Open(kvp.Key, kvp.Value.Value, kvp.Value.Type);
            }

            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                keysToRemove.Add(kvp.Key); // ������ Ű �߰�
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        DrawAddKeyValueSection();

        if (GUILayout.Button("Remove All"))
        {
            PlayerPrefsManager.Instance.ClearAll();
            RefreshPlayerPrefsCache();
        }

        ProcessRemovals(); // ������ Ű���� ����Ʈ�� ���� ����, GUI �̺�Ʈ ó���� ��� ���� �Ŀ� �� ���� ������ ó��
    }

    private void RefreshPlayerPrefsCache()
    {
        playerPrefsCache.Clear();
        var allKeys = PlayerPrefsManager.Instance.GetAllKeys();
        foreach (var key in allKeys)
        {
            var type = PlayerPrefsManager.Instance.GetInt(key, int.MinValue) != int.MinValue ? PlayerPrefsManager.ValueType.Int : PlayerPrefsManager.ValueType.String;
            var value = type == PlayerPrefsManager.ValueType.Int ? PlayerPrefs.GetInt(key).ToString() : PlayerPrefs.GetString(key);
            playerPrefsCache[key] = (value, type);
        }
    }

    private void DrawAddKeyValueSection()
    {
        GUILayout.Space(10);
        DrawLine();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Key", GUILayout.Width(100));
        GUILayout.Label("Value", GUILayout.Width(200));
        GUILayout.Label("Type", GUILayout.Width(200));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        newKey = GUILayout.TextField(newKey, GUILayout.Width(100));
        newValue = GUILayout.TextField(newValue, GUILayout.Width(200));
        selectedTypeIndex = EditorGUILayout.Popup(selectedTypeIndex, typeOptions, GUILayout.Width(70));

        if (GUILayout.Button("Add", GUILayout.Width(105)))
        {
            AddKeyValue();
        }
        GUILayout.EndHorizontal();
    }

    private void AddKeyValue()
    {
        if (!string.IsNullOrEmpty(newKey))
        {
            PlayerPrefsManager.ValueType selectedType = (PlayerPrefsManager.ValueType)selectedTypeIndex;
            if (selectedType == PlayerPrefsManager.ValueType.Int)
            {
                if (int.TryParse(newValue, out int intValue))
                {
                    PlayerPrefsManager.Instance.SetInt(newKey, intValue);
                }
                else
                {
                    EditorUtility.DisplayDialog("Invalid Value", "For an integer value, please enter a valid integer.", "OK");
                    return;
                }
            }
            else
            {
                PlayerPrefsManager.Instance.SetString(newKey, newValue);
            }

            newKey = "";
            newValue = "";
            RefreshPlayerPrefsCache();
        }
    }

    private void ProcessRemovals()
    {
        foreach (var key in keysToRemove)
        {
            PlayerPrefsManager.Instance.RemoveKey(key);
        }
        if (keysToRemove.Count > 0)
        {
            RefreshPlayerPrefsCache();
            keysToRemove.Clear();
        }
    }

    private void DrawLine()
    {
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
    }
}
