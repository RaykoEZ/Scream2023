using System;
using UnityEngine;
using UnityEngine.UI;

public class InspectClock : InspectionDisplay
{
    public override void Init(SaveData save) 
    {
        DateTime init = save.InitDate;
        bool timeCheck = DateTime.Now == init;
        // set clock state
        if (timeCheck && save.Persistent.AriaDead) 
        {
            m_anim?.SetTrigger("dead");
        }
        else if (timeCheck && !save.Persistent.AriaDead) 
        {
            m_anim?.SetTrigger("dateSync");
        }
        else 
        {
            m_anim?.SetTrigger("normal");
        }
    }
}
