using UnityEngine;
using System.Collections.Generic;
using Curry.Events;
using UnityEngine.Audio;

public class AudioSettingHandler : MonoBehaviour , ISettingUpdateListener<PhoneSettings>
{
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] List<AudioSource> m_audio = default;
    [SerializeField] CurryGameEventTrigger m_playRain = default;
    [SerializeField] CurryGameEventTrigger m_stopRain = default;
    [SerializeField] CurryGameEventTrigger m_playBgm = default;
    [SerializeField] CurryGameEventTrigger m_changeBgm = default;
    [SerializeField] CurryGameEventTrigger m_stopBgm = default;
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
    public void PlayBgm(AudioClip bgm)
    {
        AudioInfo info = new AudioInfo(bgm, true);
        m_changeBgm?.TriggerEvent(info);
    }
    public void StopBgm()
    {
        m_stopBgm?.TriggerEvent(new EventInfo());
    }
    public void PlayRain()
    {
        m_playRain?.TriggerEvent(new EventInfo());
    }
    public void StopRain()
    {
        m_stopRain?.TriggerEvent(new EventInfo());
    }
}
