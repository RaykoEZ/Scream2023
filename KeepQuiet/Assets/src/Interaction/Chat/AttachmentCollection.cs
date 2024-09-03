using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "attachments_", menuName = "New list of attachment prefabs")]
public class AttachmentCollection : ScriptableObject 
{
    [SerializeField] List<AttachmentItem> m_attachments = default;
    public GameObject GetAttachmentPrefab(string id) 
    {
        AttachmentItem item = m_attachments.Find((x) => x.AttachmentId == id);
        return item.AttachmentPrefab;
    }
}
