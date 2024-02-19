using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class HangerTool : QuickTool
{
    public override void OnEndDrag(PointerEventData eventData)
    {
    }
    public void HookForm() 
    {
        // do animationto transform hanger to hook
        UnlockTool();
    }
}

