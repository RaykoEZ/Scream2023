﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
// Stores info about a view (e.g. currently viewing? visible clues, clue/puzzle states)
public abstract class ViewState : MonoBehaviour
{
    [SerializeField] protected ViewNavigationHandler m_nav = default;
    [SerializeField] protected CanvasGroup m_clueGroup = default;
    [SerializeField] private Transform m_lighting = default;
    [SerializeField] private Transform m_vfx = default;
    [SerializeField] private Transform m_background = default;
    [SerializeField] VolumeProfile m_postProcessVolumeProfile = default;
    public abstract string Name { get; }
    public Transform Lighting => m_lighting;
    public Transform Vfx => m_vfx;
    public Transform Background => m_background;
    public VolumeProfile PostProcessVolumeProfile => m_postProcessVolumeProfile;
    public void InitState(SaveData saveData) 
    {
        InitStateInternal(saveData);
    }
    protected virtual void InitStateInternal(SaveData gamestate)
    {
    }
    public virtual void OnAriaEnter() { }
    public virtual void OnAriaExit() { }
    public virtual void SetVisual(bool isOn)
    {
        Lighting?.gameObject.SetActive(isOn);
        Vfx?.gameObject.SetActive(isOn);
        Background?.gameObject.SetActive(isOn);
    }
    public virtual void OnUsingTool(EToolType usingTool) 
    {
        m_clueGroup.interactable = false;
    }
    public virtual void OnReturningTool(EToolType returningTool) 
    {
        m_clueGroup.interactable = true;
    }
}
