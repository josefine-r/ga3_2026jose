using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;
    public float textSpeed = 0.03f;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowLine(string line)
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    System.Collections.IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}