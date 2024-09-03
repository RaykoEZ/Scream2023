using System;
using System.Collections.Generic;
using UnityEngine;
// Triggers interactions in game: calls and messages
public class GameTriggerHandler : MonoBehaviour
{
    [SerializeField] PhoneNotificationHandler m_phone = default;
    public void Message(DialogueNode newMessage)
    {
        m_phone?.MessagePlayer(newMessage, newMessage.Dialogues[0].ChatLog.WhoSpoke);
    }
    public void Call(DialResult call) 
    {
        m_phone?.Call(call.Sequence, call.EventToTrigger);
    }
}