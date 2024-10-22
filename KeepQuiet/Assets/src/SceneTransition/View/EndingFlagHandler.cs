using UnityEngine;

public class EndingFlagHandler : MonoBehaviour 
{
    [SerializeField] GameSaveSource m_save = default;
    [SerializeField] EndingSequenceManager m_endingSequence = default;
    public void PlayRouteLaunchSequence() 
    {
        m_save.UpdateSave();
        m_endingSequence.PlayRouteSequence(m_save.Current.Persistent.CurrentEnding);
    }
    public void OnCaseClose() 
    {
        PlayEnding(Ending.Normal_CaseClosed);
    }
    public void OnFreedom()
    {
        PlayEnding(Ending.Secret_Freedom);
    }
    public void OnBadEnd() 
    {
        PlayEnding(Ending.Bad_Delusion);
    }
    public void FreedomRouteFlag() 
    {
        m_save.Current.FreedomRoute = true;
        m_save.UpdateSave();
    }
    public void PlayEnding(Ending flag) 
    {
        m_save.Current.Persistent.CurrentEnding = flag;
        // save game before ending credit
        m_save.UpdateSave();
        m_endingSequence?.PlayCredit();
    }
}
