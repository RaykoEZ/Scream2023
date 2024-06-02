using UnityEngine;

public class TriggerAudio : TriggerInteraction
{
    [SerializeField] bool m_loop = default;
    [SerializeField] AudioSource m_audio = default;
    [SerializeField] AudioClip m_toTrigger = default;
    public override void Trigger()
    {
        m_audio.loop = m_loop;
        m_audio.clip = m_toTrigger;
        m_audio.Play();
    }
}
