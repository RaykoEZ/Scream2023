using TMPro;
using UnityEngine;
public class ThoughtBubble : DraggableObject
{
    [SerializeField] TextMeshProUGUI m_label = default;
    string m_id = "";
    public string Id => m_id;
    public string Description => m_label.text;
    public void Init(string id, string description)
    {
        m_id = id;
        m_label.text = description;
    }
    public override void DropObject(Transform parent, int siblingIndex = 0)
    {
        base.DropObject(parent, siblingIndex);
    }
}
