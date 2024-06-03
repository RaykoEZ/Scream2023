using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct AnimationTriggerPair
{
    public Animator AnimatorRef;
    public string AnimationTriggerName;
}
public class NodeSignalHandler: MonoBehaviour 
{
    [SerializeField] List<AnimationTriggerPair> m_signalTriggers = default;

    public void StartSignal()
    {
        foreach (var item in m_signalTriggers)
        {
            item.AnimatorRef?.SetTrigger(item.AnimationTriggerName);
        }
    }
    private void Start()
    {
        StartSignal();
    }
}
