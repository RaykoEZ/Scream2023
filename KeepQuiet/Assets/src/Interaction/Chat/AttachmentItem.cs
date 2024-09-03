using System;
using UnityEngine;

[Serializable]
public class AttachmentItem 
{
    [SerializeField] string m_attachmentId = default;
    [SerializeField] GameObject m_attachmentPrefab = default;
    public string AttachmentId => m_attachmentId;
    public GameObject AttachmentPrefab => m_attachmentPrefab;
}
