using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// handles a list of tutorial sequences
public class TutorialCollection : MonoBehaviour 
{
    [SerializeField] List<TutorialDisplay> m_tutorials = default;
    bool isActive = false;
    int m_current = 0;
    // Start tutorial from the beginning
    public void Begin() 
    {
        if (isActive) return;
        isActive = true;
        m_current = 0;
        m_tutorials[m_current]?.Begin();
    }
    // If player clicks to continue, load next tutorial dialogue
    // If tutorial sequence ends, start next tutorial in the list
    public void NextTutorialStep() 
    {
        if (!isActive) return;

        if (m_tutorials[m_current].Next()) 
        {
            NextSequence();
        }
    }
    public void NextSequence() 
    {
        StartCoroutine(NextSequence_Internal());
    }
    IEnumerator NextSequence_Internal() 
    {
        m_tutorials[m_current]?.End();
        yield return new WaitForSeconds(0.2f);
        m_current++;
        if (m_current < m_tutorials.Count)
        {
            m_tutorials[m_current]?.Begin();
        }
        else 
        {
            // End tutorial
            isActive = false;
            m_current = 0;
        }
    }
    public void EndTutorial() 
    {
        isActive = false;
        m_tutorials[m_current]?.End();
        m_current = 0;
    }
}
