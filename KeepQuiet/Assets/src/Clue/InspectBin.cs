using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InspectBin : InspectionDisplay
{
    [SerializeField] Image m_watchInBin = default;
    [SerializeField] SnapshotWatch m_watchToInspect = default;
    public void SpawnBin()
    {
        m_watchInBin.enabled = false;
        Instantiate(m_watchToInspect, transform);
    }
}
