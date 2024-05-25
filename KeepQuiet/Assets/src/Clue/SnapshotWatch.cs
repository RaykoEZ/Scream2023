using UnityEngine;
using UnityEngine.Playables;
public enum GameTime 
{ 
    
}
[RequireComponent(typeof(CanvasGroup))]
public class SnapshotWatch : DraggableObject 
{
    [SerializeField] PlayableDirector m_director = default;
    
    [SerializeField] PlayableAsset m_presentTime = default;
    [SerializeField] PlayableAsset m_hoursAgo = default;
    [SerializeField] PlayableAsset m_yearAgo = default;
    [SerializeField] PlayableAsset m_glitch = default;
    [SerializeField] PlayableAsset m_reset = default;
    [SerializeField] PlayableAsset m_off = default;
    protected override Transform OnDragParent => transform.parent;
    protected override void OnEnable()
    {
    }
    public void Init(SaveData save)
    {
        if (save.WatchUnlocked) 
        {
            Show();
        }
        else 
        {
            Hide();
        }
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
}
