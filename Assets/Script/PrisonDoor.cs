using JetBrains.Annotations;
using UnityEngine;
using static PrisonerDialouge;

public class PrisonDoor : MonoBehaviour
{
    public PrisonerDialogue dialogue;
    private bool playerNearby;
    private bool opened = false;

    private int outsideIndex = 0;
    private int insideIndex = 0;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TalkOutside();
        }
    }

    void TalkOutside()
    {
        if (dialogue.outsideDoorLines.Length == 0) return;

        DialogueManager.Instance.ShowLine(
            dialogue.outsideDoorLines[outsideIndex]
        );

        outsideIndex++;

        if (outsideIndex >= dialogue.outsideDoorLines.Length)
            outsideIndex = 0;
    }

    public void EnterRoom()
    {
        if (dialogue.insideRoomLines.Length == 0) return;

        DialogueManager.Instance.ShowLine(
            dialogue.insideRoomLines[insideIndex]
        );

        insideIndex++;

        if (insideIndex >= dialogue.insideRoomLines.Length)
            insideIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
