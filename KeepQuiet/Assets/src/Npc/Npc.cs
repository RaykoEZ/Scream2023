using UnityEngine;
// Handles npc states and react to player inputs
public abstract class Npc : MonoBehaviour 
{
    [SerializeField] string m_phoneNumber = default;
    public string PhoneNumber => m_phoneNumber;
}