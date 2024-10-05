using UnityEngine;

public class EndingFlagHandler : MonoBehaviour 
{
    [SerializeField] GameSaveSource m_save = default;
    public void OnCaseClose() 
    {
        SetEndingFlag(Ending.Normal_CaseClosed);
    }
    public void OnFreedom()
    {
        SetEndingFlag(Ending.Secret_Freedom);
    }
    public void OnBadEnd() 
    {
        SetEndingFlag(Ending.Bad_Delusion);
    }
    public void SetEndingFlag(Ending flag) 
    {
        m_save.Current.Persistent.CurrentEnding = flag;
    }
}
