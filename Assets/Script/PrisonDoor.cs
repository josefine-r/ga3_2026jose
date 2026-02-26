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


    private bool awaitingQuestion = false;
    private bool questionAsked = false;

    private int timesTalked = 0;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TalkOutside();
        }
    }

    void TalkOutside()
    {
        if (dialogue == null) return;

        // If puzzle solved and question not asked yet
        if (awaitingQuestion && !questionAsked)
        {
            questionAsked = true;
            awaitingQuestion = false;

            AskDoorQuestion();
            return;
        }

        timesTalked++;

        if (!unlocked)
        {
            if (timesTalked <= 2 && dialogue.outsideDoorLines.Length > 0)
            {
                int index = Random.Range(0, dialogue.outsideDoorLines.Length);
                DialogueManager.Instance.ShowLine(dialogue.outsideDoorLines[index]);
            }
            else if (dialogue.hintLines.Length > 0)
            {
                int index = Random.Range(0, dialogue.hintLines.Length);
                DialogueManager.Instance.ShowLine(dialogue.hintLines[index]);
            }
        }
        else
        {
            if (dialogue.afterSolvedLines.Length > 0)
            {
                int index = Random.Range(0, dialogue.afterSolvedLines.Length);
                DialogueManager.Instance.ShowLine(dialogue.afterSolvedLines[index]);
            }
        }
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
        {
            playerNearby = true;
            InteractionUI.Instance.Show("Press E to Talk");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            InteractionUI.Instance.Hide();
        }
    }

    public void CheckCode(string enteredCode)
    {
        if (unlocked) return;

        if (enteredCode == correctCode)
        {
            unlocked = true;
            awaitingQuestion = true;

            DialogueManager.Instance.ShowLine("...the lock disengages.");

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
