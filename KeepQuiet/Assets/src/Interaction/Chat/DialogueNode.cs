using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Dialogue 
{
    public bool HasAudio;
    public float DelayBeforeTyping;
    // how much time to stay typing
    public float TypingDelay;
    public string WhoSpoke;
    // Can be image, clue, etc
    public GameObject SentPrefabRef;
    [TextArea(5, 10)]
    public string Content;
    public ThoughtBubble ObtainThought;
    public DialogueEventTrigger TriggerAfterThisLine;
}
[Serializable]
public class ChatOption 
{
    [SerializeField] string m_description = default;
    [SerializeField] DialogueNode m_outcome = default;
    [SerializeField] DialogueEventTrigger m_triggerAfterChoice = default;
    public DialogueNode Outcome { get => m_outcome; }
    public string Description { get => m_description; }
    // Trigger a game event if player chose this option
    public void TriggerChoiceEvent() 
    {
        m_triggerAfterChoice?.Trigger();
    }
}
[Serializable]
// A node class for storing NPC text message, player reply options, and the next npc dialogue
[CreateAssetMenu(fileName = "Node_", menuName = "Chat/New Dialogue", order = 0)]
public class DialogueNode : ScriptableObject, IEquatable<DialogueNode>
{
    [SerializeField] List<Dialogue> m_dialogues = default;
    [SerializeField] List<ChatOption> m_replyOptions = default;
    [SerializeField] List<ThoughtDropResult> m_thoughtOutcomes = default;
    [SerializeField] ChatOptionOverride m_hiddenOptions = default;
    public static readonly string s_player = "You";
    public static readonly string s_aria = "Aria";
    public static readonly string s_alm = "Al.";
    public static readonly string s_elia = "Elia";
    public static readonly string s_handler = "Your Handler";
    public IReadOnlyList<Dialogue> Dialogues => m_dialogues;
    // Leaves for possible next Dialogue Nodes and their reply text
    // If option count > 1, player chooses a reply
    public IReadOnlyList<ChatOption> Options => m_replyOptions;
    public ChatOptionOverride HiddenOptions { get => m_hiddenOptions; }
    public List<ThoughtDetail> ThoughtsToDrop() 
    {
        var ret = new List<ThoughtDetail>();
        foreach (var item in m_thoughtOutcomes)
        {
            ret.Add(item.ToDrop);
        }
        return ret;
    }
    public DialogueNode FindThoughtOutcome(ThoughtDetail detail) 
    {
        var result = m_thoughtOutcomes.Find(i => i.ToDrop.Id == detail.Id);
        return result.Outcome;
    }
    bool IEquatable<DialogueNode>.Equals(DialogueNode other)
    {
        if (other == null) return false;
        return
            Dialogues == other.Dialogues;
    }
}
