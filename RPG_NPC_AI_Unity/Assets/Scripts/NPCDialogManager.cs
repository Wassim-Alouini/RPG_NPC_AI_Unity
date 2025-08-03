using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPCDialogManager : MonoBehaviour
{
    public static NPCDialogManager Instance;
    [SerializeField] private DialogUI dialogUI;
    [SerializeField] private string path;
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
        OnFinishedDialog += AdvanceDialog;
    }

    public static event Action OnFinishedDialog;

    public static void RaiseFinishedDialog()
    {
        OnFinishedDialog.Invoke();
    }


    private Dictionary<string, DialogData> dialogMap;
    private DialogData currentData;

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

    void Start()
    {
        var myData = LoadDialog(path);
        currentData = myData;
        string fullText = myData.text;
        dialogUI.DisplayDialog(fullText);
    }

    void AdvanceDialog()
    {
        string nextDirectDialog = currentData.nextDialogId;
        if (nextDirectDialog is null)
        {
            //No Next Direct Dialog
            //Look for options
            var myOptions = currentData.options;
            if (myOptions.Count == 0)
            {
                //End of Dialog
                dialogUI.HideUI();
            }
            else
            {
                //Options
                foreach (var opt in myOptions)
                {
                    dialogUI.AddOption(opt);
                    //ONCLICK OF OPTION DO SOMETHING
                }

            }
        }
        else
        {
            ReadNewDialog(currentData.nextDialogId);
        }
    }

    public void OnSelectedOption(string nextId)
    {
        ReadNewDialog(nextId);
    }

    void ReadNewDialog(string id)
    {
        var newData = dialogMap[id];
        string fullText = newData.text;
        currentData = newData;
        dialogUI.DisplayDialog(fullText);
    }



}
