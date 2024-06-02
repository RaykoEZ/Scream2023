using System.Security.Cryptography;
using UnityEngine;

public class Bin : Clue 
{
    [SerializeField] InspectBin m_inspectBin = default;

    public override InspectionDisplay GetInspectionDisplay(SaveData state)
    {
        return m_inspectBin;
    }
}
