using Curry.Events;
using UnityEngine;
[CreateAssetMenu(fileName ="NewTriggerDialogue", menuName = "Chat/Trigger Dialogue")]
public class TriggeredDialogueContent : ScriptableObject 
{
    [TextArea(5, 10)]
    [SerializeField] string m_content = default;
    [SerializeField] CurryGameEventTrigger m_trigger = default;
    public string Content { get => m_content; }
    public void Trigger()
    {
        m_trigger?.TriggerEvent(new TextInfo(m_content));
    }
}
public class TextInfo : EventInfo
{
    public string Content { get; private set; }
    public TextInfo(string content, bool pause = true)
    {
        Content = content;
    }
}