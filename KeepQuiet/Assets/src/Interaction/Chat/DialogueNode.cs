using Curry.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue 
{
    public float DelayBeforeTyping;
    // how much time to stay typing
    public float TypingDelay;
    public string WhoSpoke;
    // Can be image, clue, etc
    public GameObject SentPrefabRef;
    [TextArea]
    public string Content;
}
public class ReplyInfo : EventInfo
{
    private ChatOption m_optionTriggered;
    public ChatOption OptionTrigger => m_optionTriggered;
    public ReplyInfo(ChatOption option,
        Dictionary<string, object> payload = null, Action onFinishCallback = null) :
        base(payload, onFinishCallback)
    {
        m_optionTriggered = option;
    }
}
[Serializable]
public class ChatOption 
{
    [SerializeField] string m_description = default;
    [SerializeField] DialogueNode m_outcome = default;
    [SerializeField] CurryGameEventTrigger m_triggerAfterChoice = default;
    public DialogueNode Outcome { get => m_outcome; }
    public string Description { get => m_description; }
    // Trigger a game event if player chose this option
    public void TriggerChoiceEvent() 
    {
        m_triggerAfterChoice?.TriggerEvent(new ReplyInfo(this));
    }
}
[Serializable]
// A node class for storing NPC text message, player reply options, and the next npc dialogue
[CreateAssetMenu(fileName = "Node_", menuName = "Dialogue/Node", order = 0)]
public class DialogueNode : ScriptableObject, IEquatable<DialogueNode>
{
    [SerializeField] List<Dialogue> m_dialogues = default;
    [SerializeField] List<ChatOption> m_replyOptions = default;
    public static readonly string s_playerName = "You";
    public IReadOnlyList<Dialogue> Dialogues => m_dialogues;
    // Leaves for possible next Dialogue Nodes and their reply text
    // If option count > 1, player chooses a reply
    public IReadOnlyList<ChatOption> Options => m_replyOptions;
    bool IEquatable<DialogueNode>.Equals(DialogueNode other)
    {
        if (other == null) return false;
        return
            Dialogues == other.Dialogues;
    }
}
