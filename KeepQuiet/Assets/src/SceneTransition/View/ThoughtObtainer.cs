using Curry.Events;
using UnityEngine;

public class ThoughtObtainer : MonoBehaviour
{
    [SerializeField] CurryGameEventTrigger m_obtainThought = default;
    public void ObtainThought(ThoughtDetail toObtain)
    {
        EventInfo info = new EventInfo
        (
            new System.Collections.Generic.
            Dictionary<string, object>
            { {"thought", toObtain } }
        );
        m_obtainThought?.TriggerEvent(info);
    }
}
