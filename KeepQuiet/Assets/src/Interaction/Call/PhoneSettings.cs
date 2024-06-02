using System.Collections.Generic;
using UnityEngine;
public interface ISettingUpdateListener<T>
{
    void OnUpdate(T updated);
}
[CreateAssetMenu(fileName = "PhoneSetting_", menuName = "Phone Settings")]
public class PhoneSettings : ScriptableObject 
{
    [Range(0f, 1f)]
    [SerializeField] float m_volume = default;
    [SerializeField] AudioClip m_ringtone = default;
    [SerializeField] Sprite m_wallPaper = default;
    // set of listeners for updates
    HashSet<ISettingUpdateListener<PhoneSettings>> m_updateListeners = new HashSet<ISettingUpdateListener<PhoneSettings>>();
    public void Listen(ISettingUpdateListener<PhoneSettings> listener) 
    {
        m_updateListeners.Add(listener);
    }
    public void Unlisten(ISettingUpdateListener<PhoneSettings> listener) 
    {
        m_updateListeners.Remove(listener);
    }
    // Trgger update event for any setting holders
    void OnSettingUpdate()
    {
        foreach (var listener in m_updateListeners)
        {
            listener?.OnUpdate(this);
        }
    }
    public float GetVolume()
    {
        return m_volume;
    }
    public AudioClip GetRingtone()
    {
        return m_ringtone;
    }
    public Sprite GetWallPaper()
    {
        return m_wallPaper;
    }
    public void SetVolume(float value)
    {
        m_volume = value;
        OnSettingUpdate();
    }
    public void SetRingtone(AudioClip value)
    {
        m_ringtone = value;
        OnSettingUpdate();
    }
    public void SetWallPaper(Sprite value)
    {
        m_wallPaper = value;
        OnSettingUpdate();
    }     
}
