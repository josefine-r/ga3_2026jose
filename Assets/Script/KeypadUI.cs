using UnityEngine;
using TMPro;

public class KeypadUI : MonoBehaviour
{
    public static KeypadUI Instance;

    public GameObject panel;
    public TextMeshProUGUI codeText;

    private string currentInput = "";
    private PrisonDoor activeDoor;
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (!isTyping) return;

        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c) && currentInput.Length < 4)
            {
                currentInput += c;
                UpdateDisplay();
            }

            if (c == '\b' && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                UpdateDisplay();
            }

            if (c == '\n' || c == '\r')
            {
                SubmitCode();
            }
        }
    }

    public void StartKeypad(PrisonDoor door)
    {
        activeDoor = door;
        currentInput = "";
        isTyping = true;
        panel.SetActive(true);
        UpdateDisplay();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SubmitCode()
    {
        isTyping = false;
        panel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (activeDoor != null)
            activeDoor.CheckCode(currentInput);
    }

    void UpdateDisplay()
    {
        string display = "";
        for (int i = 0; i < 4; i++)
        {
            if (i < currentInput.Length)
                display += currentInput[i] + " ";
            else
                display += "_ ";
        }
        codeText.text = display;
    }
}