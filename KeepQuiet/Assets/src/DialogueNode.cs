using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue 
{
    public string WhoSpoke;
    public string Content;
}

[Serializable]
// A node class for storing NPC text message, player reply options, and the next npc dialogue
[CreateAssetMenu(fileName = "DialogueNode_", menuName = "Dialogue/Node", order = 0)]
public class DialogueNode : ScriptableObject, IEquatable<DialogueNode>
{
    [SerializeField] Dialogue m_dialogue = default;
    [SerializeField] List<DialogueNode> m_replyOptions = default;
    public static readonly string s_playerName = "You";
    public Dialogue Dialogue => m_dialogue;
    public string WhoSpoke => Dialogue.WhoSpoke;
    // NPC's message value
    public string Content => Dialogue.Content;
    // Leaves for possible next Dialogue Nodes and their reply text
    // If option count > 1, player chooses a reply
    public List<DialogueNode> Options => m_replyOptions;


    bool IEquatable<DialogueNode>.Equals(DialogueNode other)
    {
        if (other == null) return false;

        return
            WhoSpoke == other.WhoSpoke &&
            Content == other.Content;
    }
}
