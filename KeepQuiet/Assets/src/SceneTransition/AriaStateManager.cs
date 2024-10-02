using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
// Handles Aria's internal states dialogue options
[Serializable]
public class AriaStateManager : MonoBehaviour
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] DialogueContainer m_dialogueContainer = default;
    [SerializeField] AriaDisplayController m_position = default;
    [SerializeField] ConversationPlayer m_conversationPlayer = default;
    AriaState m_current;
    DialogueNode m_currentTalkingPoint;
    AssetReference m_currentDialogueAssetRef;
    public AriaState Current => m_current;
    public void InitState(SaveData change) 
    {
        m_current = change.AriaStatus;
        m_position?.MoveTo(change.AriaStatus.CurrentLocation, AriaPosition.None);
    }
    public void OnThoughtDropOnTalk(ThoughtBubble dropped)
    {
        DialogueNode outcome = m_currentTalkingPoint?.FindThoughtOutcome(dropped.DetailRef);
        bool reject = dropped == null || dropped.DetailRef == null || outcome == null;
        // if dropped thought was not valid, cancel and return thought back
        if (reject) { return; }
        // Find a dialogue outcome from dropping the thought
        else if (outcome != null)
        {
            // Stop current Dialogue and move to the new dialogue line
            m_currentTalkingPoint = outcome;
            m_conversationPlayer?.InterceptDialogue(m_currentTalkingPoint);
        }
    }
    public void SetTalkingPoint(AssetReference newPoint) 
    {
        if (newPoint == null) return;
        m_currentDialogueAssetRef = 
            m_dialogueContainer.
            AllLoadedAsset.AssetRefs.Find(t => t.AssetGUID == newPoint.AssetGUID);
        m_currentTalkingPoint = m_currentDialogueAssetRef.Asset as DialogueNode;
    }
    public void UpdateSave() 
    {
        if (m_currentTalkingPoint != null) 
        {
            Current.CurrentTalkingPoint = m_currentDialogueAssetRef;
        }
        m_save.Current.AriaStatus = Current;
    }
    public void Talk()
    {
        SetTalkingPoint(Current.CurrentTalkingPoint);
        m_conversationPlayer?.TriggerDialogue(m_currentTalkingPoint);
    }
    // Change npc starting talking node, used when progressing through chatting
    public void ChangeTalkingState(DialogueNode newState) 
    {
        AssetReference newRef = m_dialogueContainer.AllLoadedAsset.
            AssetRefs.Find(t => t.Asset.name == newState.name);
        if (newRef == null) return;
        Current.CurrentTalkingPoint = newRef;
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
