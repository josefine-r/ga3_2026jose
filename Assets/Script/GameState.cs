using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
