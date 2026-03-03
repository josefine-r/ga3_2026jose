using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public static EndingUI Instance;

    public GameObject panel;
    public TextMeshProUGUI endingText;


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");

    }

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
