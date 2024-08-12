using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InspectBin : InspectionDisplay
{
    [SerializeField] Image m_watchInBin = default;
    [SerializeField] SnapshotWatch m_watchToInspect = default;
    public void GetWatch()
    {
        m_watchInBin.enabled = false;
        m_watchToInspect?.Show();
    }
}
