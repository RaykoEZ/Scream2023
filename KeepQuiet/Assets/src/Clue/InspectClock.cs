using System;
using UnityEngine;
using UnityEngine.UI;

public class InspectClock : InspectionDisplay
{
    [SerializeField] Image m_clock = default;
    [SerializeField] Image m_hiddenClue = default;
    public override void Init(SaveData save) 
    {
        DateTime init = save.InitDate;
        m_hiddenClue.enabled = DateTime.Now == init;
    }
}
