using UnityEngine;
using static PrisonerDialouge;

public class PrisonDoor : MonoBehaviour
{
    [Header("Dialogue & Question")]
    public PrisonerDialogue dialogue;
    public PrisonerQuestion question;

    [Header("Door Code")]
    public string correctCode = "1234";

    private bool playerNearby = false;
    private bool unlocked = false;

    // Question flow
    private bool awaitingQuestion = false;
    private bool questionAsked = false;

    // Dialogue progression
    private int normalIndex = 0;
    private int hintIndex = 0;
    private int solvedIndex = 0;

    public Animator prisondoorAnimator;

    void Start()
    {
        // Reset runtime states
        unlocked = false;
        awaitingQuestion = false;
        questionAsked = false;

        normalIndex = 0;
        hintIndex = 0;
        solvedIndex = 0;
    }

    void Update()
    {
        // Talk to door
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !DialogueManager.Instance.isTalking)
        {
            TalkOutside();
        }
    }

    // ===================== TALKING =====================

    void TalkOutside()
    {
        if (dialogue == null) return;

        // After unlocking, the NEXT talk asks the question
        if (awaitingQuestion && !questionAsked)
        {
            questionAsked = true;
            awaitingQuestion = false;

            AskDoorQuestion();
            return;
        }

        // BEFORE unlocking
        if (!unlocked)
        {
            // Normal conversation lines
            if (normalIndex < dialogue.outsideDoorLines.Length)
            {
                DialogueManager.Instance.ShowLine(dialogue.outsideDoorLines[normalIndex]);
                normalIndex++;
            }
            // Hint lines
            else if (hintIndex < dialogue.hintLines.Length)
            {
                DialogueManager.Instance.ShowLine(dialogue.hintLines[hintIndex]);
                hintIndex++;
            }
        }
        // AFTER unlocking and question answered
        else
        {
            if (solvedIndex < dialogue.afterSolvedLines.Length)
            {
                DialogueManager.Instance.ShowLine(dialogue.afterSolvedLines[solvedIndex]);
                solvedIndex++;
            }
        }
    }

    // ===================== CODE SYSTEM =====================

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

    // ===================== QUESTION =====================

    public void AskDoorQuestion()
    {
        if (question != null)
            ChoiceManager.Instance.AskQuestion(question);
    }

    // ===================== ENDING OPEN =====================

    public void OpenDoor()
    {
        unlocked = true;

        // play animation
        if (prisondoorAnimator != null)
            prisondoorAnimator.SetTrigger("Open");

        // disable colliders so player can enter
        Collider[] cols = GetComponents<Collider>();
        foreach (Collider c in cols)
            c.enabled = false;

        DialogueManager.Instance.ShowLine("...one of the doors opens.");
    }

    // ===================== PLAYER DETECTION =====================

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
}
