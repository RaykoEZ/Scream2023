using Curry.Events;
using UnityEngine;
[RequireComponent(typeof(GuideCollection))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected SystemDialoguePlayer m_playback = default;
    [SerializeField] protected GuideDisplay m_display = default;
    [SerializeField] CurryGameEventListener m_triggerListener = default;
    void Start()
    {
        m_triggerListener?.Init();
    }
    public void Trigger(EventInfo info)
    {
        var collection = GetComponent<GuideCollection>();
        if (collection == null || info == null || collection.IsActive) return;
        if (info is DialogueInfo dialogue)
        {
            m_display.ReplaceStep(dialogue.Content);
            m_playback?.TriggerTutorial(collection, true);
        }
    }
}
