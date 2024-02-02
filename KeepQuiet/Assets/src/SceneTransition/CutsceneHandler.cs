using UnityEngine;
using UnityEngine.Playables;

public class CutsceneHandler : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_launchSequence = default;
    [SerializeField] PlayableAsset m_intro = default;
    public void LaunchSequence() 
    {
        PlayTrack(m_launchSequence);
    }
    public void PlayIntro() 
    {
        PlayTrack(m_intro);
    }
    void PlayTrack(PlayableAsset play) 
    {
        m_director.playableAsset = play;
        m_director.Play();
    }
}

