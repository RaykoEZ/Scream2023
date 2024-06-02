using UnityEngine;
// Handles npc states and react to player inputs
public abstract class Npc : MonoBehaviour 
{
    [SerializeField] string m_phoneNumber = default;
    public string PhoneNumber => m_phoneNumber;
    public abstract void OnPlayerCallCanceled();
    public abstract void OnPlayerDialed();
    // npc can react to player choice on a decision
    public abstract void OnPlayerDecided(DialogueNode chosen, int choiceIndex);
}