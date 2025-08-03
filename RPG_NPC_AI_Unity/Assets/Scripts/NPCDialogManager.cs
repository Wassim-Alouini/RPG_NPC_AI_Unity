using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPCDialogManager : MonoBehaviour
{
    public static NPCDialogManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Action OnFinishedDialog;
    

    private Dictionary<string, DialogData> dialogMap;

    public DialogData LoadDialog(string path)
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, path);
        if (!File.Exists(fullPath))
        {
            Debug.LogError($"Dialog file not found at path: {fullPath}");
            return null;
        }

        string json = File.ReadAllText(fullPath);
        DialogTree tree = JsonUtility.FromJson<DialogTree>(json);

        dialogMap = new Dictionary<string, DialogData>();
        foreach (var dialog in tree.dialogs)
        {
            dialogMap[dialog.id] = dialog;
        }
        return dialogMap.ContainsKey("intro") ? dialogMap["intro"] : null;
    }

    public DialogData GetDialogById(string id)
    {
        dialogMap.TryGetValue(id, out var dialog);
        return dialog;
    }
}
