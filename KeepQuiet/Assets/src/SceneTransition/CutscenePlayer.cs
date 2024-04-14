using System;
using UnityEngine;
using UnityEngine.Playables;
public class CutscenePlayer : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_director = default;
    public void PlayTrack(PlayableAsset play) 
    {
        m_director.playableAsset = play;
        m_director.Play();
    }

}

