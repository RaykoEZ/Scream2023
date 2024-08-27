using Curry.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
public delegate void OnKeyActivate(WatchDisplay newDisplay);
// A watch button that pulls out a Hidden Key
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class WatchKey : ExternalDraggableObject
{
    [Range(0.1f, 5f)]
    [SerializeField] float m_unlockTime = default;
    [SerializeField] bool m_startInteractable = default;
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
    bool m_inserted = false;
    Rigidbody2D Rb2d => GetComponent<Rigidbody2D>();
    Collider2D Collider => GetComponent<Collider2D>();
    public bool Inserted { get { return m_inserted; } set { m_inserted = value; } } 
    public WatchDisplay KeyTrigger => m_keyTrigger;

    void Start()
    {
        Inserted = false;
        SetCollision(false);
        SetInteractable(m_startInteractable);
    }
    public void SetKeyActivity(EventInfo info)
    {
        if (info == null || info.Payload == null) return;
        if (info.Payload.TryGetValue("isOn", out object result) &&
            result is bool isOn)
        {
            SetCollision(isOn);
            SetInteractable(isOn);
        }
    }
    public void SetCollision(bool isOn) 
    {
        Rb2d.bodyType = isOn? 
            RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        Collider.isTrigger = !isOn;
    }
    public void SetInteractable(bool isInteractable)
    {
        SetDraggable(isInteractable);
        GetComponent<Animator>().cullingMode = isInteractable ?
            AnimatorCullingMode.CullUpdateTransforms :
            AnimatorCullingMode.AlwaysAnimate;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        SetCollision(true);
        base.OnBeginDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        SetCollision(false);
        FinishDragCallback();
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
        SetInteractable(false);
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
        SetInteractable(true);
    }
    protected override void SetDragPosition(PointerEventData e)
    {
        Vector2 worldPos = e.pressEventCamera.ScreenToWorldPoint(e.position - m_anchorOffset);      
        GetComponent<Rigidbody2D>()?.MovePosition(worldPos);
    }
}