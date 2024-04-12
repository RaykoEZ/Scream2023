using UnityEngine;
// Handles npc states and react to player inputs
public abstract class Npc : MonoBehaviour 
{
    [SerializeField] string m_phoneNumber = default;
    [SerializeField] NpcCaller m_caller = default;
    public string PhoneNumber => m_phoneNumber;
    public abstract void OnPlayerCallCanceled();
    public abstract void OnPlayerDialed();
    // npc can react to player choice on a decision
    public abstract void OnPlayerDecided(DialogueNode chosen, int choiceIndex);
    // NPC's interaction features with player
    protected virtual void CallPlayer(DialEvent onAccept)
    {
        m_caller?.CallPlayer(m_phoneNumber, onAccept);
    }
    protected virtual void MessagePlayer(DialogueNode newDialogue) 
    {
        m_caller?.MessagePlayer(newDialogue);
    }
}