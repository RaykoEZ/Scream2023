using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
// handles a list of tutorial sequences
public class GuideCollection : MonoBehaviour 
{
    [SerializeField] protected bool m_blockBackground = default;
    // Length of time to wait after finishing a tutorial sequence(in seconds)
    [Range(0f, 1000f)]
    [SerializeField] protected float m_pauseTimeAfterSequence = default;
    [SerializeField] protected UnityEvent m_triggerOnShow = default;
    [SerializeField] protected UnityEvent m_triggerOnFinish = default;
    [SerializeField] protected List<GuideDisplay> m_tutorials = default;
    protected bool isActive = false;
    protected int m_current = 0;
    protected bool m_hasTriggeredOnce = false;
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
        m_triggerOnShow?.Invoke();
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
        m_triggerOnFinish?.Invoke();
        m_tutorials[m_current]?.End();
        m_hasTriggeredOnce = true;
        IsActive = false;
    }
}
