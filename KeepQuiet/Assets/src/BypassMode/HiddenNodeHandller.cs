using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public delegate void OnSecretUpdate();
public class HiddenNodeHandller : MonoBehaviour 
{
    [SerializeField] protected PlayableDirector m_director = default;
    [SerializeField] ExternalFileEventReceiver m_event = default;
    [SerializeField] List<BypassNode> m_toReveal = default;
    public event OnSecretUpdate SecretUnlocked;
    public void Init()
    {
        m_event.FileDropped += OnFileDropped;
        foreach (var item in m_toReveal)
        {
            item?.Hide();
        }
    }
    public void Shutdown() 
    {
        m_event.FileDropped -= OnFileDropped;
    }

    void OnFileDropped(ExternalFileDropInfo dropInfo) 
    {
        foreach (var item in m_toReveal)
        {
            item?.Show();
        }
        SecretUnlocked?.Invoke();
        Shutdown();
    }
}
