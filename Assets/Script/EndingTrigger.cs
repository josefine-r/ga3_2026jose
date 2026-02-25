using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public FinalDoorJudge judge;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            judge.DecideDoor();
        }
    }
}
