using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
public delegate void OnKeyActivate(WatchDisplay newDisplay);
// A watch button that pulls out a Hidden Key
public class WatchKey : DraggableObject 
{
    [Range(0.1f, 10f)]
    [SerializeField] float m_unlockTime = default;
    // Key to change time line of the game
    [SerializeField] WatchDisplay m_keyTrigger = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_clickButton = default;
    [SerializeField] PlayableAsset m_unlockKey = default;
    [SerializeField] PlayableAsset m_up = default;
    [SerializeField] PlayableAsset m_down = default;
    public event OnKeyActivate OnActivate;
    protected override Transform OnDragParent => transform.parent;
    Coroutine m_unlocking;
    bool m_inserted = true;
    public bool Inserted { get { return m_inserted; } set { m_inserted = value; } }
    public WatchDisplay KeyTrigger => m_keyTrigger;
    void Start()
    {
        OnLock();
    }
    void OnLock()
    {
        Movable = false;
        GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
    }
    public override void DropObject(Transform parent, int siblingIndex = 0)
    {
    }
    public override void ReturnToBeforeDrag()
    {
    }
    // Interrupt unlock if trying to unlock
    public void OnPointerRelease() 
    {
        if (!m_inserted) return;
        m_director?.Play(m_up);
        if (m_unlocking != null) 
        {
            StopCoroutine(m_unlocking);
            m_unlocking = null;
        }
    }
    public void OnWatchButtonClick() 
    {
        if (!m_inserted) return;
        OnActivate?.Invoke(KeyTrigger);
    }
    public void OnKeyInsert() 
    {
        m_director?.Play(m_clickButton);
        // lock key transform movement to animator
        OnLock();
    }
    // Key can be pulled out after this trigger
    public void TryUnlockKey()
    {
        if (!m_inserted) return;

        if (m_inserted) 
        {
            m_director?.Play(m_down);
            m_unlocking = StartCoroutine(GameUtil.Countdown(m_unlockTime, Unlock));
        }
    }
    void Unlock() 
    {
        m_unlocking = null;
        StartCoroutine(UnlockKey());
    }
    IEnumerator UnlockKey() 
    {
        m_director?.Play(m_unlockKey);
        yield return new WaitForSeconds((float)m_unlockKey.duration);
        yield return new WaitForEndOfFrame();
        // Don't let animator override transform to allow dragging
        GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        Movable = true;
    }
    protected override void SetDragPosition(PointerEventData e)
    {
        Vector2 worldPos = e.pressEventCamera.ScreenToWorldPoint(e.position - m_anchorOffset);      
        GetComponent<Rigidbody2D>()?.MovePosition(worldPos);
    }
}
