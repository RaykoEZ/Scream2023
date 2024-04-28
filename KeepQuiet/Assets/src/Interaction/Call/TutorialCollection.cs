﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// handles a list of tutorial sequences
public class TutorialCollection : MonoBehaviour 
{
    [SerializeField] List<TutorialDisplay> m_tutorials = default;
    bool isActive = false;
    int m_current = 0;

    public bool IsActive { get => isActive; private set => isActive = value; }

    // Start tutorial from the beginning
    public void Begin() 
    {
        if (IsActive) return;
        IsActive = true;
        m_current = 0;
        m_tutorials[m_current]?.Begin();
    }
    // If player clicks to continue, load next tutorial dialogue
    // If tutorial sequence ends, start next tutorial in the list
    public IEnumerator NextTutorialStep() 
    {
        if (!m_tutorials[m_current].Next()) 
        {
            NextSequence();
        }
        yield return new WaitForSeconds(0.2f);
    }
    public void NextSequence() 
    {
        if (m_current + 1 < m_tutorials.Count)
        {
            m_tutorials[m_current]?.End();
            m_current++;
            m_tutorials[m_current]?.Begin();
        }
        else
        {
            // End tutorial
            EndTutorial();
        }
    }
    public void EndTutorial() 
    {
        m_tutorials[m_current]?.End();
        IsActive = false;
    }
}
