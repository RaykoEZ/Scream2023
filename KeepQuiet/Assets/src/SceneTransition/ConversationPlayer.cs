using System.Collections.Generic;
using UnityEngine;

public class ConversationPlayer : SystemDialoguePlayer 
{
    [SerializeField] TalkDisplay m_talkDisplay = default;
    [SerializeField] ReplyPrompter m_optionPrompt = default;
    void OnEnable()
    {
        m_optionPrompt.OnChosen += OnOptionChosen;
        m_talkDisplay.OnPrompt += OnOptionPrompt;
    }
    void OnDisable()
    {
        m_optionPrompt.OnChosen -= OnOptionChosen;
        m_talkDisplay.OnPrompt -= OnOptionPrompt;
    }
    public void TriggerDialogue(DialogueNode talkingPoint) 
    {
        m_talkDisplay.CurrentDialogueRef = talkingPoint;
        StartDialogue(m_talkDisplay, true);
    }
    public void InterceptDialogue(DialogueNode newPoint) 
    {
        StopAllCoroutines();
        m_displayCall = null;
        m_optionPrompt.HideAll();
        m_talkDisplay?.OverrideDisplay(newPoint);
    }
    void OnOptionChosen(DialogueNode outcome) 
    {
        StopAllCoroutines();
        TriggerDialogue(outcome);
    }
    void OnOptionPrompt(List<ChatOption> options) 
    {
        // Stop Next Step Button, prompt options
        m_next?.Disable();
        m_optionPrompt?.PromptOption(options);
    }
    protected override void StartDialogue(StepDisplayHandler col, bool forceRepeat = false)
    {
        // Don't repeat the same tutorial in the same session if we don't need to
        if (m_displayCall != null) return;
        if ((!col.IsActive || col.HasTriggeredOnce) && !forceRepeat) return;
        m_current = col;
        m_current?.Begin();
        m_next?.Enable();
    }
}