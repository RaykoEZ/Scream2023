using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        m_background.enabled = col.BlockBackground;
        m_current = col;
        m_current?.Begin();
        m_next?.Enable();
    }
}
// Handles Aria's internal states dialogue options
[Serializable]
public class AriaStateManager : MonoBehaviour
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] DialogueAssetReferenceIndex m_talkingPointIndex = default;
    [SerializeField] AriaDisplayController m_position = default;
    [SerializeField] ConversationPlayer m_conversationPlayer = default;
    AriaState m_current;
    DialogueNode m_loadedTalkingPoint;
    AssetReference m_currentTalkingPoint;
    public AriaState Current => m_current;
    public void InitState(SaveData change) 
    {
        m_current = change.AriaStatus;
        m_position?.MoveTo(change.AriaStatus.CurrentLocation, AriaPosition.None);
        SetTalkingPoint(change.AriaStatus.CurrentTalkingPoint);
    }
    public void SetTalkingPoint(AssetReference newPoint) 
    {
        if (newPoint == null) return;
        m_currentTalkingPoint = newPoint;
        var op = m_currentTalkingPoint.LoadAssetAsync<DialogueNode>();
        op.Completed += 
            result =>
            {
                m_loadedTalkingPoint = result.Result;
            };
    }
    public void UpdateSave() 
    {
        if (m_loadedTalkingPoint != null) 
        {
            Current.CurrentTalkingPoint = m_currentTalkingPoint;
        }
        m_save.Current.AriaStatus = Current;
    }
    public void InitiateTalk()
    {
        if (m_loadedTalkingPoint == null) return;
        m_conversationPlayer?.TriggerDialogue(m_loadedTalkingPoint);
    }
    public void HideAria()
    {
        m_position.Hide();
        Current.CurrentLocation = AriaPosition.None;
    }
    public void MoveTo(AriaPosition newLocation)
    {
        if (Current.CurrentLocation == newLocation) { return; }
        AriaPosition prev = Current.CurrentLocation;
        Current.CurrentLocation = newLocation;
        m_position.MoveTo(newLocation, prev);
    }
    public void MoveTo(int newLocation)
    {
        AriaPosition newPos = (AriaPosition)newLocation;
        var prev = m_current.CurrentLocation;
        m_current.CurrentLocation = newPos;
        m_position.MoveTo(newPos, prev);
    }
}
