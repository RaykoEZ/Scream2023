using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
// A watch button that pulls out a Hidden Key
public class WatchKey : DraggableObject 
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_clickButton = default;
    [SerializeField] PlayableAsset m_unlockKey = default;
    protected override Transform OnDragParent => transform.parent;
    void Start()
    {
        //Draggable = false;
    }
    public void OnWatchButtonClick() 
    {
        m_director?.Play(m_clickButton);
    }
    // Key can be pulled out after this trigger
    public void SecretKeyTrigger() 
    {
        StartCoroutine(UnlockKey());
    }
    IEnumerator UnlockKey() 
    {
        m_director?.Play(m_unlockKey);
        yield return new WaitWhile(m_director.playableGraph.IsPlaying);
        Draggable = true;
    }
    protected override void SetDragPosition(PointerEventData e)
    {
        Vector2 worldPos = e.pressEventCamera.ScreenToWorldPoint(e.position - m_anchorOffset);
        
        GetComponent<Rigidbody2D>()?.MovePosition(worldPos);
    }
}
