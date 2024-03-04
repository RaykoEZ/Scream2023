using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InspectBin : InspectionDisplay
{
    [SerializeField] Image m_hanger = default;
    [SerializeField] Sprite m_hangerSprite = default;
    public override void Init(SaveData state)
    {

    }

    public override IEnumerator OnExit()
    {
        yield return null;
    }

    public void TakeHanger()
    { 
    
    }
}
