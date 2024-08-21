using Curry.Events;
using Curry.Explore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnChatUpdate();
// Contains and displays text message boxes for a NPC chat
public class ChatRoom : HideableUI
{
    [SerializeField] CurryGameEventTrigger m_onThoughtResolved = default;
    [SerializeField] CurryGameEventTrigger m_onThoughtDisable = default;
    [SerializeField] CurryGameEventTrigger m_thoughtsAvailble = default;
    [SerializeField] Transform m_messageParent = default;
    [SerializeField] ReplyPrompter m_optionPrompt = default;
    [SerializeField] MessageBox m_npcBoxPrefab = default;
    [SerializeField] MessageBox m_ariaBoxPrefab = default;
    [SerializeField] MessageBox m_playerBoxPrefab = default;
    List<MessageBox> m_spawnedMessages = new List<MessageBox>();
    ChatHistory m_history;
    DialogueNode m_currentNode;
    Coroutine m_chatting;
    public event OnChatUpdate OnEnd;
    bool m_isDirty = false;
    bool m_paused = false;
    public ChatHistory History => m_history;
    void OnEnable()
    {
        m_optionPrompt.OnChosen += OnReplyChosen;
    }
    void OnDisable()
    {
        Shutdown();
    }
    public void SetChatHistory(ChatHistory history)
    {
        if(m_history != history) 
        {
            ClearChat();
        }
        m_history = history;
        m_currentNode = m_history.LastDialogue;
    }
    public void SetPaused(bool paused)
    {
        m_paused = paused;
    }
    public void OnOverwriteChat(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        var payload = info.Payload;
        if (payload.TryGetValue("overwrite", out object result) &&
            result is DialogueNode node) 
        {
            Overwrite(node);
        }
    }
    // Overwrite history and reload chat
    public void Overwrite(DialogueNode newChat) 
    {
        if (newChat == null) 
        {
            Debug.LogWarning("Chat overwrite: null chat node in arg");
            return;
        }
        Shutdown();
        m_history.OverwriteLog(newChat);
        SetChatHistory(m_history);
        CheckForReplyOptions();
    }
    // When a thought is dropped into the conversation
    public void OnThoughtDrop(ThoughtBubble thought) 
    {
        if (thought.DetailRef == null) return;
        // Find a dialogue outcome from dropping the thought
        DialogueNode outcome = m_currentNode.FindThoughtOutcome(thought.DetailRef);
        if (outcome != null) 
        {
            thought.ReturnToBeforeDrag();
            // Stop current Dialogue and move to the new dialogue line
            StartCoroutine(ResolveThought(outcome, thought));
        }
    }
    public void Shutdown() 
    {
        m_optionPrompt.OnChosen -= OnReplyChosen;
        ClearChat();
    }
    void ClearChat() 
    {
        List<MessageBox> toDelete = new List<MessageBox>(m_spawnedMessages);
        foreach (var item in toDelete)
        {
            // Clear all previous messages
            m_spawnedMessages.Remove(item);
            item.Cleanup();
            Destroy(item.gameObject);
        }
    }
    void CheckForReplyOptions()
    {
        var options = m_currentNode.Options;
        if (options.Count > 0)
        {
            m_optionPrompt.PromptOption(options);
        }
        else 
        {
            // end display loop
            OnEnd?.Invoke();
        }
    }
    // A new dialogue is chosen for the current display
    void OnReplyChosen(DialogueNode chosen)
    {
        if (chosen == null) return;
        UpdateCurrentDialogue(chosen);
        StartChat();
    }
    // Display a new message
    MessageBox PrepareMessage(Dialogue toDisplay) 
    {
        // Instntiate a message box for the message
        MessageBox instance;
        string who = toDisplay.WhoSpoke;
        if(who == DialogueNode.s_player) 
        {
            instance = Instantiate(m_playerBoxPrefab, m_messageParent);
        }
        else if (who == DialogueNode.s_aria) 
        {
            instance = Instantiate(m_ariaBoxPrefab, m_messageParent);
        }
        else 
        {
            instance = Instantiate(m_npcBoxPrefab, m_messageParent);
        }
        instance.Init(toDisplay);
        m_spawnedMessages.Add(instance);
        return instance;
    }
    // Append a dialogue to history
    public void UpdateCurrentDialogue(DialogueNode result) 
    {
        m_currentNode = result;
        m_history.Append(m_currentNode);
        m_isDirty = true;
    }
    // start displaying dialogues of current node
    // and continue until the end of the dialogue tree
    public void StartChat() 
    {
        // disable all available thoughtbubbles
        m_onThoughtDisable?.TriggerEvent();
        if (m_isDirty) 
        {
            m_isDirty = false;
            m_chatting = StartCoroutine(ContinueChat(m_currentNode.Dialogues));
        }
        else 
        {
            // Called when reopening a static chatroom,
            // check for new reply override conditions
            CheckForReplyOptions();
        }
    }
    // Interrupt chat with
    IEnumerator ResolveThought(DialogueNode outcome, ThoughtBubble thought) 
    {
        m_optionPrompt?.HideAll();
        // Stop current Dialogue and move to the new dialogue line
        UpdateCurrentDialogue(outcome);
        // Wat until previous chat finish resolving last line
        yield return new WaitUntil(() => m_chatting == null);
        // trigger thought 
        m_onThoughtResolved?.TriggerEvent( new EventInfo(
        payload: new Dictionary<string, object>
        {
            { "thought", thought}
        }));
        yield return new WaitForSeconds(2f);
        StartChat();
    }
    IEnumerator ContinueChat(IReadOnlyList<Dialogue> dialogues)
    {
        bool isNpc;
        MessageBox msg;
        foreach (var line in dialogues)
        {
            //skip empty content
            if (line.Content == null || string.IsNullOrEmpty(line.Content)) continue;
            isNpc = line.WhoSpoke != DialogueNode.s_player;
            yield return new WaitUntil(() => !m_paused);
            yield return new WaitForSeconds(line.DelayBeforeTyping);
            msg = PrepareMessage(line);
            msg.Typing();
            yield return new WaitForSeconds(line.TypingDelay);
            msg.Show();
            // Break subroutine if we have interrupted the chat (thought bubble)
            if (m_isDirty == true)
            {
                m_chatting = null;
                yield break;
            }
            yield return new WaitForSeconds(0.25f);
            // Trigger any events after a dialogue is displayed
            TryTriggerAfterCurrentLine(line);
        }
        yield return new WaitForSeconds(0.5f);
        // prompt option at the end if there is any
        CheckForReplyOptions();
        // check for any thought that can be dropped
        CheckForThoughts();
        m_chatting = null;

    }
    void CheckForThoughts() 
    {
        List<ThoughtDetail> result = m_currentNode.ThoughtsToDrop();
        if (result.Count > 0) 
        {
            EventInfo info = new EventInfo(payload:
                new Dictionary<string, object> { {"toDrop", result } });
            m_thoughtsAvailble?.TriggerEvent(info);
        }
    }
    public static void TryTriggerAfterCurrentLine(Dialogue current)
    {
        current.TriggerAfterThisLine?.Trigger();
    }
}