using UnityEngine;

public class RoomDialogueTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // This trigger is now used only for ending events later
            Debug.Log("Player entered ending room");
        }
    }
}
