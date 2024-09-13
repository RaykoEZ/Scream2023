using UnityEngine;
using UnityEngine.Playables;

// play sequence for title depending on route/game state
public class TitleSequenceManager : MonoBehaviour
{
    [SerializeField] PlayableAsset m_default = default;
    [SerializeField] PlayableAsset m_freedomRoute = default;
    [SerializeField] PlayableAsset m_freedomEnd = default;
    [SerializeField] PlayableAsset m_deadEnd = default;
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
                toPlay = m_freedomEnd;
                break;
            default:
                if (!save.Persistent.AriaGone) 
                {
                    toPlay = m_default;
                }
                else 
                {
                    // Dead End happens before Freedom End
                    toPlay = save.FreedomRoute ? m_freedomRoute : m_deadEnd;
                }
                break;
        }
        m_title.PlaySequence(toPlay);
    }
}