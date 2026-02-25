using JetBrains.Annotations;
using UnityEngine;
using static PrisonerDialouge;

public class PrisonDoor : MonoBehaviour
{
    public PrisonerQuestion question;

    public PrisonerDialogue dialogue;
    private bool playerNearby;
    private bool opened = false;

    [Header("Code Lock")]
    public string correctCode = "1234";
    private bool unlocked = false;

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

    public void AskDoorQuestion()
    {
        if (question != null)
            ChoiceManager.Instance.AskQuestion(question);
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

    public void CheckCode(string enteredCode)
    {
        if (unlocked) return;

        if (enteredCode == correctCode)
        {
            unlocked = true;

            DialogueManager.Instance.ShowLine("...the lock clicks open.");

            AskDoorQuestion(); // ? THIS triggers the question

            GameState.Instance.doorsUnlocked++;
        }
        else
        {
            DialogueManager.Instance.ShowLine("The keypad flashes red.");
        }
    }

    public void OpenDoor()
    {
        unlocked = true;

        // stop the player from interacting again
        GetComponent<Collider>().enabled = false;

        DialogueManager.Instance.ShowLine("The door slowly opens...");
    }



}
