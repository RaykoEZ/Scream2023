using System;
using UnityEngine;
using UnityEngine.Playables;
[Serializable]
public struct ToolUnlockSequenceCollection 
{
    [SerializeField] PlayableAsset m_bat;
    [SerializeField] PlayableAsset m_torch;
    [SerializeField] PlayableAsset m_specialTorch;
    [SerializeField] PlayableAsset m_hanger;
    [SerializeField] PlayableAsset m_hook;

    public PlayableAsset Bat => m_bat;
    public PlayableAsset Torch => m_torch;
    public PlayableAsset SpecialTorch => m_specialTorch;
    public PlayableAsset Hanger => m_hanger;
    public PlayableAsset Hook => m_hook;
}
public class CutsceneHandler : MonoBehaviour 
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] PlayableAsset m_launchSequence = default;
    [SerializeField] PlayableAsset m_intro = default;
    [SerializeField] ToolUnlockSequenceCollection m_toolUnlocks = default;
    public void LaunchSequence() 
    {
        PlayTrack(m_launchSequence);
    }
    public void PlayIntro() 
    {
        PlayTrack(m_intro);
    }
    public void UnlockSequence(QuickTool toUnlock) 
    {
        switch (toUnlock.ToolName)
        {
            case EToolType.Bat:
                break;
            case EToolType.Torch:
                break;
            case EToolType.SpecialTorch:
                break;
            case EToolType.CoatHanger:
                break;
            case EToolType.Hook:
                break;
            default:
                break;
        }
    }
    void PlayTrack(PlayableAsset play) 
    {
        m_director.playableAsset = play;
        m_director.Play();
    }
}

