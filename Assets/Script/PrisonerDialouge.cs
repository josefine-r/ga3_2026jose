using UnityEngine;

public class PrisonerDialouge : MonoBehaviour
{
    [CreateAssetMenu(fileName = "New Prisoner Dialouge", menuName = "MindPrison/Prisoner Dialouge")]
    public class PrisonerDialogue : ScriptableObject
    {

        [TextArea(3, 8)]
        public string[] outsideDoorLines;

        [TextArea(3, 8)]
        public string[] hintLines;

        [TextArea(3, 8)]
        public string[] insideRoomLines;

        [TextArea(3, 8)]
        public string[] afterSolvedLines;
    }
}
