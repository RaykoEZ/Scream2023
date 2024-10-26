using UnityEngine;
using UnityEngine.Playables;

// play sequence for title depending on route/game state
public class TitleSequenceManager : MonoBehaviour
{
    [SerializeField] PlayableAsset m_default = default;
    [SerializeField] PlayableAsset m_freedomRoute = default;
    [SerializeField] SequencePlayer m_title = default;
    public void InitTitleState(SaveData save) 
    {
        PlayableAsset toPlay;
        Ending ending = save.Persistent.CurrentEnding;
        // If player killed Aria, show dead sequence, no music
        // if on true end route, play special sequence
        // if true end reached, white aria close eye, calmer music
        switch (ending)
        {
            case Ending.Secret_Freedom:
                toPlay = m_freedomRoute;
                break;
            default:
                toPlay = m_default;
                break;
        }
        m_title.PlaySequence(toPlay);
    }
}