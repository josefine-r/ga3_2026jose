using UnityEngine;
using TMPro;

public class EndingUI : MonoBehaviour
{
    public static EndingUI Instance;

    public GameObject panel;
    public TextMeshProUGUI endingText;

     void Awake()
    {
        Instance = this;
        panel.SetActive(false);
  
    }

    public void ShowEnding(string text)
    {
        panel.SetActive(true);
        endingText.text = text;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindFirstObjectByType<CharacterController>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           
        }
    }

}
