using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "MindPrison/Question")]
public class PrisonerQuestion : ScriptableObject
{

    [TextArea(3, 6)]
    public string questionText;

    public string optionA;
    public string optionB;

    public EmotionType emotionA;
    public EmotionType emotionB;



}

public enum EmotionType
{
    Fear,
    Anger,
    Denial,
    Sadness,
    Regret,
    Shame,
    Hope,
    Guilt
}
