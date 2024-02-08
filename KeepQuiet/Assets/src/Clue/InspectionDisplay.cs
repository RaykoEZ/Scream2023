using UnityEngine;
// A script on a clue display to notify clue inspection display handler
public abstract class InspectionDisplay : MonoBehaviour 
{
    public abstract void Init(GameStateSaveData state);
}
