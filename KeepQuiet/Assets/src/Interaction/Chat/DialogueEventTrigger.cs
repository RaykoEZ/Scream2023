using Curry.Events;
using UnityEngine;
[CreateAssetMenu(fileName ="NewTriggerDialogue", menuName = "Chat/Trigger Dialogue")]
public class DialogueEventTrigger : ScriptableObject 
{
    [TextArea(5, 10)]
    [SerializeField] string m_monologue = default;
    [SerializeField] CurryGameEventTrigger m_trigger = default;
    public string Monologue { get => m_monologue; }
    public void Trigger()
    {
        m_trigger?.TriggerEvent(new TextInfo(m_monologue));
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