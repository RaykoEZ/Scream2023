using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public struct DialEvent
{
    [SerializeField] PlayableAsset m_playThis;
    public PlayableAsset PlayThis => m_playThis;
}
[CreateAssetMenu(fileName = "Dial_", menuName = "Dial/Result", order = 0)]
public class DialResult : ScriptableObject 
{
    [SerializeField] string m_sequence = default;
    [SerializeField] DialEvent m_eventToTrigger = default;
    public string Sequence => m_sequence; 
    public DialEvent EventToTrigger => m_eventToTrigger;
}
