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
[CreateAssetMenu(fileName = "DialogueNode_", menuName ="Dialogue/Node", order = 0)]
public class DialogueNode : ScriptableObject, IEquatable<DialogueNode>
{
    public string WhoSpoke => m_whoSpoke;
    // NPC's message value
    public string Content => m_content;
    // Leaves for possible next Dialogue Nodes and their reply text
    public List<Reply> ReplyOptions => m_replyOptions;
    [SerializeField] string m_whoSpoke = default;
    [SerializeField] string m_content = default;
    [SerializeField] List<Reply> m_replyOptions = default;
    bool IEquatable<DialogueNode>.Equals(DialogueNode other)
    {
        if (other == null) return false;

        return
            WhoSpoke == other.WhoSpoke &&
            Content == other.Content;
    }
}
