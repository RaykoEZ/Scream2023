using UnityEngine;
using System.Collections.Generic;
using Curry.Events;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] CurryGameEventTrigger m_playRain = default;
    [SerializeField] CurryGameEventTrigger m_stopRain = default;
    [SerializeField] CurryGameEventTrigger m_playBgm = default;
    [SerializeField] CurryGameEventTrigger m_changeBgm = default;
    [SerializeField] CurryGameEventTrigger m_stopBgm = default;
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
