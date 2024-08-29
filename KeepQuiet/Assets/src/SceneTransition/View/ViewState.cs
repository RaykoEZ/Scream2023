using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Rendering;
// Stores info about a view (e.g. currently viewing? visible clues, clue/puzzle states)
public abstract class ViewState : MonoBehaviour
{
    [SerializeField] bool m_activeOnStart = default;
    [SerializeField] private Transform m_vfx = default;
    [SerializeField] private Transform m_background = default;
    [SerializeField] VolumeProfile m_postProcessVolumeProfile = default;
    [SerializeField] UnityEvent m_onShow = default;
    public abstract string Name { get; }
    public Transform Vfx => m_vfx;
    public Transform Background => m_background;
    public VolumeProfile PostProcessVolumeProfile => m_postProcessVolumeProfile;
    public void InitState(SaveData saveData) 
    {
        InitStateInternal(saveData);
    }
    void Start()
    {
        // Disable all views by default
        if (!m_activeOnStart) 
        {
            ResetState();
        }
#if UNITY_EDITOR
        else 
        {
            SetVisual(true);
        }
#endif    
    }
    public virtual void ResetState() 
    {
        SetVisual(false);
    }
    protected virtual void InitStateInternal(SaveData gamestate)
    {
        SetVisual(true);
    }
    public virtual void OnAriaEnter() { }
    public virtual void OnAriaExit() { }
    protected virtual void SetVisual(bool isOn)
    {
        Vfx?.gameObject.SetActive(isOn);
        Background?.gameObject.SetActive(isOn);
        if (isOn) 
        {
            m_onShow?.Invoke();
        }
    }
}
