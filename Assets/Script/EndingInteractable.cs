using System.Runtime.CompilerServices;
using UnityEngine;

public class EndingInteractable : MonoBehaviour
{
    public string endingText;
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            TriggerEnding();
        }
    }

    void TriggerEnding()
    {
        InteractionUI.Instance.Hide();
        EndingUI.Instance.ShowEnding(endingText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            InteractionUI.Instance.Show("Press E to Read");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected at note");
            playerNear = true;
            InteractionUI.Instance.Show("Press E to Read");
        }
    }
}
