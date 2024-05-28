using Curry.Events;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] List<CanvasGroup> m_toHide = default;
    [SerializeField] SequencePlayer m_ariaBody = default;
    SequencePlayer m_currentCutscene;
    public void AriaBodySequence() 
    {
        StartCutscene(m_ariaBody);
    }
    void StartCutscene(SequencePlayer sequence) 
    {
        if (m_currentCutscene != null) return;
        foreach (var item in m_toHide)
        {
            item.alpha = 0f;
            item.blocksRaycasts = false;
        }
        m_currentCutscene = sequence;
        m_currentCutscene.OnFinish += OnCutsceneFinish;
        m_currentCutscene.PlaySequence();
    }
    void OnCutsceneFinish() 
    {
        foreach (var item in m_toHide)
        {
            item.alpha = 1f;
            item.blocksRaycasts = true;
        }
        m_currentCutscene = null;
        m_currentCutscene.OnFinish -= OnCutsceneFinish;
    }
}
