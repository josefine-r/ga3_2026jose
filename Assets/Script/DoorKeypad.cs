using UnityEngine;

public class DoorKeypad : MonoBehaviour
{
    public PrisonDoor connectedDoor;

    private string currentInput = "";
    private bool typing = false;

    void Update()
    {
        if (!typing) return;

        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                currentInput += c;
                Debug.Log("Code: " + currentInput);
            }

            if (c == '\b' && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }

            if (c == '\n' || c == '\r')
            {
                connectedDoor.CheckCode(currentInput);
                typing = false;
                currentInput = "";
            }
        }
    }

    public void StartTyping()
    {
        typing = true;
        currentInput = "";
        Debug.Log("Enter Code:");
    }

}
