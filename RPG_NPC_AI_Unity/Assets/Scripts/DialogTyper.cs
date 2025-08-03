using System.Collections;
using TMPro;
using UnityEngine;

public class DialogTyper : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.03f;

    private string[] words;
    private int wordIndex;
    private bool isTyping;
    private bool isWaitingForNext;
    private string currentText = "";

    private const int MAX_LINES = 4;

    void Awake()
    {
        dialogText.maxVisibleLines = MAX_LINES;
        dialogText.text = "";

    }

    public void StartDialog(string fullText)
    {
        words = fullText.Split(' ');
        wordIndex = 0;
        currentText = "";
        dialogText.text = "";
        dialogText.maxVisibleCharacters = 0;
        isWaitingForNext = false;
        StartCoroutine(TypeDialogPage());
    }

    public void ShowNextPage()
    {
        if (wordIndex >= words.Length - 1)
        {
        }
        if (!isTyping && isWaitingForNext)
        {
            isWaitingForNext = false;
            currentText = "";
            dialogText.text = "";
            dialogText.maxVisibleCharacters = 0;
            StartCoroutine(TypeDialogPage());
        }
    }

    private IEnumerator TypeDialogPage()
    {
        isTyping = true;

        while (wordIndex < words.Length)
        {
            string nextWord = words[wordIndex] + " ";
            string testText = currentText + nextWord;

            // Apply the full text temporarily and remove line limit to measure
            dialogText.maxVisibleLines = 999;
            dialogText.text = testText;
            dialogText.ForceMeshUpdate();

            int lines = dialogText.textInfo.lineCount;

            Debug.Log(lines);
            if (lines >= MAX_LINES)
            {
                // If adding this word breaks the limit, don't add it
                dialogText.text = currentText;
                dialogText.maxVisibleLines = MAX_LINES;
                break;
            }

            // Accept this word
            currentText = testText;
            dialogText.text = currentText;
            dialogText.maxVisibleLines = MAX_LINES;

            // Typing character by character
            int startChar = dialogText.maxVisibleCharacters;
            for (int i = 0; i < nextWord.Length; i++)
            {
                dialogText.maxVisibleCharacters = startChar + i + 1;
                yield return new WaitForSeconds(typingSpeed);
            }

            wordIndex++;
        }

        isTyping = false;
        isWaitingForNext = true;
    }

    public bool CanAdvance => !isTyping && isWaitingForNext;
}
