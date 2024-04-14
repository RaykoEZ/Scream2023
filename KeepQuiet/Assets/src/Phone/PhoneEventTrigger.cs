using UnityEngine;

public class PhoneEventTrigger : MonoBehaviour 
{
    [SerializeField] PlayerCaller m_playerCaller = default;
    public virtual void CallPlayer(string callerNumber, DialEvent onAccept) 
    {
        m_playerCaller.Call(callerNumber, onAccept);
    }
    public virtual void MessagePlayer(DialogueNode newDialogue) 
    {
        m_playerCaller.Message(newDialogue);
    }
}
