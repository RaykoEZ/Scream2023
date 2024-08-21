using Curry.Events;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ThoughtTriggerDisplay : MonoBehaviour
{
    [SerializeField] UnityEvent m_thoughtTriggers = default;
    [SerializeField] List<TextMeshProUGUI> m_labels = default;
    public void OnThoughtOutcome(EventInfo info) 
    {
        if (info == null || info.Payload == null) return;
        var payload = info.Payload;
        if (payload.TryGetValue("thought", out object result) &&
            result is ThoughtDetail detail)
        {
            SetThoughtText(detail.Description);
            m_thoughtTriggers?.Invoke();
        }
    }
    void SetThoughtText(string thought)
    {
        foreach (var item in m_labels)
        {
            item.text = thought;
        }
    }
}
