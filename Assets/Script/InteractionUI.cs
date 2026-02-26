using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    public TextMeshProUGUI interactText;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string message)
    {
        interactText.text = message;
        interactText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        interactText.gameObject.SetActive(false);
    }

}
