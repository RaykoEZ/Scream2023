using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue 
{
    public string WhoSpoke;
    public Texture2D SentPhoto;
    [TextArea]
    public string Content;
}
[Serializable]
public class ChatOption 
{
    [SerializeField] string m_description = default;
    [SerializeField] DialogueNode m_outcome = default;
    [SerializeField] CurryGameEventTrigger m_triggerAfterChoice = default;
    public DialogueNode Outcome { get => m_outcome; }
    public string Description { get => m_description; }
}
[Serializable]
// A node class for storing NPC text message, player reply options, and the next npc dialogue
[CreateAssetMenu(fileName = "Node_", menuName = "Dialogue/Node", order = 0)]
public class DialogueNode : ScriptableObject, IEquatable<DialogueNode>
{
    [SerializeField] string m_title = default;
    [SerializeField] List<Dialogue> m_dialogues = default;
    [SerializeField] List<ChatOption> m_replyOptions = default;
    [SerializeField] CurryGameEventTrigger m_triggerAfterDialogue = default;
    public static readonly string s_playerName = "You";
    public IReadOnlyList<Dialogue> Dialogues => m_dialogues;
    public string Title => m_title;
    // Leaves for possible next Dialogue Nodes and their reply text
    // If option count > 1, player chooses a reply
    public IReadOnlyList<ChatOption> Options => m_replyOptions;
    bool IEquatable<DialogueNode>.Equals(DialogueNode other)
    {
        if (other == null) return false;
        return
            Title == other.Title &&
            Dialogues == other.Dialogues;
    }
}
