using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// handles a list of tutorial sequences
public class GuideCollection : MonoBehaviour 
{
    [SerializeField] bool m_blockBackground = default;
    // Length of time to wait after finishing a tutorial sequence(in seconds)
    [Range(0f, 1000f)]
    [SerializeField] float m_pauseTimeAfterSequence = default;
    [SerializeField] List<GuideDisplay> m_tutorials = default;
    bool isActive = false;
    int m_current = 0;
    bool m_hasTriggeredOnce = false;
    public bool IsActive { get => isActive; private set => isActive = value; }
    public bool HasTriggeredOnce { get => m_hasTriggeredOnce; }
    public bool BlockBackground { get => m_blockBackground;}

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
            yield return new WaitForSeconds(m_pauseTimeAfterSequence);
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
        m_hasTriggeredOnce = true;
        IsActive = false;
    }
}
