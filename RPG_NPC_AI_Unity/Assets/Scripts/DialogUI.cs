using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private GameObject optionContainer;
    private DialogTyper dialogTyper;

    public void DisplayDialog(string fullText)
    {

        dialogTyper.StartDialog(fullText);
    }

    public void NextPage()
    {
        dialogTyper?.ShowNextPage();
    }

    public void HideUI()
    {
        uiContainer.SetActive(false);
    }
    public void ShowUI()
    {
        uiContainer.SetActive(true);
    }
    public void HideOptions()
    {
        optionContainer.SetActive(false);
    }
    public void ShowOptions()
    {
        optionContainer.SetActive(true);
    }
    public void AddOption(DialogOptionData optionData)
    {
        var myOption = Instantiate(optionPrefab, optionContainer.transform);
        myOption.GetComponent<TextMeshProUGUI>().text = optionData.optionText;
    }
    void Awake()
    {
        dialogTyper = GetComponent<DialogTyper>();
    }


}
