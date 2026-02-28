using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public DoorKeypad keypad;
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.R))
        {
            KeypadUI.Instance.StartKeypad(keypad.connectedDoor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            InteractionUI.Instance.Show("Press R to Enter Code");
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            InteractionUI.Instance.Hide();
        }
            
    }
}
