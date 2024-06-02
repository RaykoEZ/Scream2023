using UnityEngine;
using UnityEngine.InputSystem;
// handles events when player insert/eject key
[RequireComponent(typeof(Collider2D))]
public class KeySlot : MonoBehaviour 
{
    [SerializeField] SnapshotWatch m_watch = default;
    WatchKey m_currentlyInserted = null;
    void Start()
    {
        var key = m_watch.transform.GetComponentInChildren<WatchKey>();
        if (key != null) 
        {
            SetupKey(key);
        }
    }
    void SetupKey(WatchKey key) 
    {
        m_currentlyInserted = key;
        m_currentlyInserted.OnActivate += OnKeyActivate;
        m_currentlyInserted.Inserted = true;
        m_currentlyInserted.transform.SetParent(m_watch.transform, true);
        m_currentlyInserted.transform.SetAsFirstSibling();
    }
    void ShutdownKey()
    {
        m_currentlyInserted.OnActivate -= OnKeyActivate;
        m_currentlyInserted.Inserted = false;
        m_currentlyInserted.transform.SetParent(m_watch.transform.parent, true);
        m_currentlyInserted.transform.SetAsFirstSibling();
        m_currentlyInserted = null;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_currentlyInserted != null) return;
        if (collision.gameObject.TryGetComponent(out WatchKey key)
            && !key.Inserted) 
        {
            SetupKey(key);
            m_currentlyInserted.OnKeyInsert();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out WatchKey key)
            && key == m_currentlyInserted && key.Inserted) 
        {
            ShutdownKey();
            m_watch.SetWatchState(WatchDisplay.Off);
        }
    }
    void OnKeyActivate(WatchDisplay newDisplay) 
    {
        m_watch.SetWatchState(newDisplay);
    }
}
