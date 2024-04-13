using UnityEngine;

public class PhoneEventTrigger : MonoBehaviour 
{
    [SerializeField] PlayerCaller m_playerCaller = default;
    [SerializeField] CutscenePlayer m_cutscene = default;
    [SerializeField] DialogueNode m_ariaMessage1 = default;
    public virtual void CallPlayer(string callerNumber, DialEvent onAccept) 
    {
        m_playerCaller.Call(callerNumber, onAccept);
    }
    public virtual void MessagePlayer(DialogueNode newDialogue) 
    {
        m_playerCaller.Message(newDialogue);
    }
    public void GameIntro()
    {
        m_cutscene.PlayIntro();
    }
    public void FirstHandshake() 
    { 
    
    }
    public void AriaMessage_1() 
    {
        m_playerCaller.Message(m_ariaMessage1);
    }
}
