using Curry.Events;
using UnityEngine;
[RequireComponent(typeof(GuideDisplay))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected SystemDialoguePlayer m_playback = default;
    [SerializeField] CurryGameEventListener m_triggerListener = default;
    void Start()
    {
        m_triggerListener?.Init();
    }
    public void Trigger(EventInfo info)
    {
        var display = GetComponent<GuideDisplay>();
        if (display == null || info == null || display.IsActive) return;
        if (info is DialogueInfo dialogue)
        {
            display.ReplaceStep(dialogue.Content);
            m_playback?.TriggerDialogue(display, true);
        }
    }
}
