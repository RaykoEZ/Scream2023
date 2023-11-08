using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Reply
{
    public string ReplyValue => m_reply;
    // Points to the next Dialogue
    public DialogueNode Next => m_next;
    [TextArea]
    [SerializeField] string m_reply;
    [SerializeField] DialogueNode m_next;
}

// A node class for storing NPC text message, player reply options, and the next npc dialogue
[CreateAssetMenu(fileName = "DialogueNode_", menuName ="Dialogue Node", order = 0)]
public class DialogueNode : ScriptableObject
{
    // NPC's message value
    public string Value => m_value;
    // Leaves for possible next Dialogue Nodes and their reply text
    public List<Reply> ReplyOptions => m_replyOptions;
    [SerializeField] string m_value = "";
    [SerializeField] List<Reply> m_replyOptions = new List<Reply>();
}
