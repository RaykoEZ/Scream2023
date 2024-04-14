using UnityEngine;
using System.Collections.Generic;
public class AudioSettingHandler : MonoBehaviour , ISettingUpdateListener<PhoneSettings>
{
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] List<AudioSource> m_audio = default;
    void OnEnable()
    {
        m_setting?.Listen(this);
    }
    void OnDisable()
    {
        m_setting?.Unlisten(this);
    }
    void Start() 
    {
        foreach (var item in m_audio)
        {
            item.volume = m_setting.GetVolume();

        }
    }
    public void OnUpdate(PhoneSettings updated)
    {
        foreach (var item in m_audio)
        {
            item.volume = updated.GetVolume();

        }
    }
}
