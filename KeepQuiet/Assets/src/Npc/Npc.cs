using UnityEngine;
// Handles npc states and react to player inputs
public abstract class Npc : MonoBehaviour 
{
    [SerializeField] string m_phoneNumber = default;
    [SerializeField] NpcCaller m_caller = default;
    public string PhoneNumber => m_phoneNumber;
    public abstract void OnPlayerCallCanceled();
    public abstract void OnCallDenied();
    public abstract void OnCallAccepted();
    public abstract void OnPlayerDialed();
    public abstract void OnPlayerCallFinished();
    public abstract void OnPlayerChatFinished();
    public abstract void OnPlayerDecided(DialogueNode chosen);
}