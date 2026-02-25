using UnityEngine;

public class FinalDoorJudge : MonoBehaviour
{
    public PrisonDoor fearDoor;
    public PrisonDoor angerDoor;
    public PrisonDoor denialDoor;
    public PrisonDoor sadnessDoor;
    public PrisonDoor regretDoor;
    public PrisonDoor shameDoor;
    public PrisonDoor hopeDoor;
    public PrisonDoor guiltDoor;

    public void DecideDoor()
    {
        GameState g = GameState.Instance;

        int[] scores = {
        g.fear, g.anger, g.denial, g.sadness,
        g.regret, g.shame, g.hope, g.guilt
    };

        PrisonDoor[] doors = {
        fearDoor, angerDoor, denialDoor, sadnessDoor,
        regretDoor, shameDoor, hopeDoor, guiltDoor
    };

        int highest = 0;

        for (int i = 1; i < scores.Length; i++)
        {
            if (scores[i] > scores[highest])
                highest = i;
        }

        doors[highest].OpenDoor();
    }
}
