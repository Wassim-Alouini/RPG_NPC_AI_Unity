using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private Transform optionContainer;
    private DialogData dialogData;
    private DialogTyper dialogTyper;

    public void DisplayDialog()
    {
        dialogData = NPCDialogManager.Instance.LoadDialog(path);
        string currentFullText = dialogData.text;
        dialogTyper.StartDialog(currentFullText);
    }

    public void NextPage()
    {
        dialogTyper?.ShowNextPage();
    }

    void Awake()
    {
        dialogTyper = GetComponent<DialogTyper>();
    }

    void Start()
    {
        DisplayDialog();
    }
}
