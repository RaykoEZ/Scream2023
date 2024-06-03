using Curry.Events;
using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public enum WatchDisplay 
{ 
    Present,
    HoursAgo,
    YearAgo,
    Error,
    None,
    Off
}

[RequireComponent(typeof(CanvasGroup))]
public class SnapshotWatch : ExternalDraggableObject
{
    [SerializeField] PlayableDirector m_director = default; 
    [SerializeField] PlayableAsset m_presentTime = default;
    [SerializeField] PlayableAsset m_hoursAgo = default;
    [SerializeField] PlayableAsset m_yearAgo = default;
    [SerializeField] PlayableAsset m_glitch = default;
    [SerializeField] PlayableAsset m_none = default;
    [SerializeField] PlayableAsset m_off = default;
    WatchDisplay m_currentDisplay = WatchDisplay.Off;
    protected override Transform OnDragParent => transform.parent;
    public WatchDisplay CurrentDisplay { get => m_currentDisplay;}
    protected override void OnEnable()
    {
    }
    public void Init(SaveData save)
    {
        SetWatchState(save.WatchState);
    }
    public void Show() 
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void SetWatchState(WatchDisplay state) 
    {
        if (state == m_currentDisplay) return;
        m_currentDisplay = state;
        switch (m_currentDisplay)
        {
            case WatchDisplay.Present:
                m_director.Play(m_presentTime);
                break;
            case WatchDisplay.HoursAgo:
                m_director.Play(m_hoursAgo);
                break;
            case WatchDisplay.YearAgo:
                m_director.Play(m_yearAgo);
                break;
            case WatchDisplay.Error:
                m_director.Play(m_glitch);
                break;
            case WatchDisplay.None:
                m_director.Play(m_none);
                break;
            case WatchDisplay.Off:
                m_director.Play(m_off);
                break;
            default:
                break;
        }
    }
}
