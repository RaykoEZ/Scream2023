using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class HangerTool : QuickTool
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_transformSequence = default;
    public override void OnEndDrag(PointerEventData eventData)
    {
    }
    // When hanger transforms to a hook play a short sequence to unlock the hook
    public void OnHangerTransform() 
    {
        m_director.playableAsset = m_transformSequence;
        m_director.Play();
    }
}

