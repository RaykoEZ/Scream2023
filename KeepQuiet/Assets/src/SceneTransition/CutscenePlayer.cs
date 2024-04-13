using System;
using UnityEngine;
using UnityEngine.Playables;
public class CutscenePlayer : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_intro = default;
    [SerializeField] PlayableAsset m_handshake_1 = default;
    public void PlayIntro() 
    {
        PlayTrack(m_intro);
    }
    void FirstHandshake()
    {
        PlayTrack(m_handshake_1);
    }
    void PlayTrack(PlayableAsset play) 
    {
        m_director.playableAsset = play;
        m_director.Play();
    }

}

