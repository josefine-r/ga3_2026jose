using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    void Start()
    {
        fear = 0;
        anger = 0;
        denial = 0;
        sadness = 0;
        regret = 0;
        shame = 0;
        hope = 0;
        guilt = 0;

        questionsAnswered = 0;
        doorsUnlocked = 0;
    }

    // emotion scores
    public int fear;
    public int anger;
    public int denial;
    public int sadness;
    public int regret;
    public int shame;
    public int hope;
    public int guilt;

    // progress tracking
    public int doorsUnlocked;
    public int questionsAnswered = 0;
    public int totalPrisoners = 8;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
