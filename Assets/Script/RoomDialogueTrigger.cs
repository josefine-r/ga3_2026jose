using UnityEngine;

public class RoomDialogueTrigger : MonoBehaviour
{

    public PrisonDoor connectedDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            connectedDoor.EnterRoom();
        }
    }
}
