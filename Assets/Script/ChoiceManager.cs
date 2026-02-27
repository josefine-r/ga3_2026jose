using UnityEngine;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;

    public TextMeshProUGUI choiceText;

    private bool waitingForChoice;
    private System.Action<int> callback;

    private bool endingChosen = false;

   
    void Awake()
    {
        Instance = this;
        choiceText.text = "";
    }

    void Update()
    {
        if (!waitingForChoice) return;

        if (Input.GetKeyDown(KeyCode.F))
            Choose(0);

        if (Input.GetKeyDown(KeyCode.G))
            Choose(1);
    }

    public void AskQuestion(PrisonerQuestion question)
    {
        DialogueManager.Instance.ShowLine(question.questionText);
        StartCoroutine(WaitForLine(question));
    }

    System.Collections.IEnumerator WaitForLine(PrisonerQuestion question)
    {
        while (DialogueManager.Instance.isTalking)
            yield return null;

        waitingForChoice = true;

        choiceText.text =
            "[F] " + question.optionA +
            "\n[G] " + question.optionB;

        callback = (answer) =>
        {
            AddEmotion(answer == 0 ? question.emotionA : question.emotionB);
        };
    }

    void Choose(int answer)
    {
        waitingForChoice = false;
        choiceText.text = "";
        callback?.Invoke(answer);
    }

    void AddEmotion(EmotionType emotion)
    {
        GameState g = GameState.Instance;

        switch (emotion)
        {
            case EmotionType.Fear: g.fear++; break;
            case EmotionType.Anger: g.anger++; break;
            case EmotionType.Denial: g.denial++; break;
            case EmotionType.Sadness: g.sadness++; break;
            case EmotionType.Regret: g.regret++; break;
            case EmotionType.Shame: g.shame++; break;
            case EmotionType.Hope: g.hope++; break;
            case EmotionType.Guilt: g.guilt++; break;
        }

        GameState.Instance.questionsAnswered++;
        CheckForEnding();
    }

    void CheckForEnding()
    {
        if (endingChosen) return;

        if (GameState.Instance.questionsAnswered >= GameState.Instance.totalPrisoners)
        {
            endingChosen = true;

            Debug.Log("All questions answered. Choosing ending...");

            FinalDoorJudge judge = FindFirstObjectByType<FinalDoorJudge>();
            if (judge != null)
                judge.DecideDoor();
        }
    }
}
