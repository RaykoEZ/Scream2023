using System.Collections.Generic;
using UnityEngine;

public class ToolUnlockHandler : MonoBehaviour 
{
    public void Init(List<QuickTool> tools) 
    {
        foreach (var item in tools)
        {
            item.OnLock += OnToolLock;
            item.OnUnlock += OnToolUnlock;
        }
    }
    public void OnToolLock(QuickTool toUnlock)
    {
        // game state may need states for tool unlocks
    }    
    // When hanger transforms to a hook play a short sequence to unlock the hook
    public void OnToolUnlock(QuickTool toUnlock)
    {
    }
}

