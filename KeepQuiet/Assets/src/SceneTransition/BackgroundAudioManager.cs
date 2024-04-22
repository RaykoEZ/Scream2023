using Curry.Events;
using System.Collections;
using UnityEngine;

public class AudioInfo : EventInfo 
{
    public AudioClip Audio;
    public bool PlayAudio;

    public AudioInfo(AudioClip audio, bool playAudio)
    {
        Audio = audio;
        PlayAudio = playAudio;
    }
}
    // Plays and changes background music clip
public class BackgroundAudioManager : MonoBehaviour 
{
    [SerializeField] AudioSource m_bgmSource = default;
    [SerializeField] float m_fadeInDuration = default;
    [SerializeField] float m_fadeOutDuration = default;
    [SerializeField] AnimationCurve m_fadeInCurve = default;
    [SerializeField] AnimationCurve m_fadeOutCurve = default;
    [SerializeField] CurryGameEventListener m_playTrigger = default;
    [SerializeField] CurryGameEventListener m_stopTrigger = default;
    [SerializeField] CurryGameEventListener m_changeTrack = default;
    private void Start()
    {
        m_playTrigger?.Init();
        m_stopTrigger?.Init();
        m_changeTrack?.Init();
    }
    public void Play()
    {
        StartCoroutine(FadeIn());
        m_bgmSource?.Play();
    }
    public void Stop()
    {
        StartCoroutine(FadeOut());
    }
    public void ChangeTrack(EventInfo info) 
    {
        if (info != null && info is AudioInfo audio)
        {
            ChangeTrack(audio.Audio, audio.PlayAudio);
        }
    }
    public void ChangeTrack(AudioClip clip, bool playNow = true) 
    {
        if (playNow) 
        {
            m_bgmSource.clip = clip;
        }
        else 
        {
            StartCoroutine(BgmTransition_Internal(clip));
        }
    }
    IEnumerator BgmTransition_Internal(AudioClip clip) 
    {
        // Fade out old bgm
        yield return StartCoroutine(FadeOut());
        yield return new WaitForEndOfFrame();
        // start new bgm
        m_bgmSource.clip = clip;
        m_bgmSource.Play();
        yield return StartCoroutine(FadeIn());
    }
    public IEnumerator FadeOut() 
    {
        float t = 0f;
        while (t < m_fadeInDuration)
        {
            t += Time.deltaTime;
            m_bgmSource.volume = m_fadeOutCurve.Evaluate(t / m_fadeOutDuration);
            yield return null;
        }
        m_bgmSource.Stop();
    }
    IEnumerator FadeIn() 
    {
        float t = 0f;
        while (t < m_fadeInDuration)
        {
            t += Time.deltaTime;
            m_bgmSource.volume = m_fadeInCurve.Evaluate(t/m_fadeInDuration);
            yield return null;
        }
    }
}