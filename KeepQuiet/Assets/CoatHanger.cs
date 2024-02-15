using Curry.Explore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ECoatHangerState 
{ 
    Normal = 0,
    Hook = 1,
    Unknown = 2
}
public class CoatHanger : HideableUI
{
    [SerializeField] DragRotationHandler m_hook = default;
    [SerializeField] DragRotationHandler m_left = default;
    ECoatHangerState m_state = ECoatHangerState.Normal;
    public ECoatHangerState ToolState => m_state;
    // Update tool drag box animator and shape in animator
    public void UpdateToolState(ECoatHangerState newState) 
    {
        m_state = newState;
    }
    // check hook & left rotation, transform into a new tool if both threshold reached
    void OnHangerModified() 
    {

    }
    bool CheckRotationThresholds() 
    {
        return false;
    }
    public void ResetHangerState() 
    { 
    
    }
}
