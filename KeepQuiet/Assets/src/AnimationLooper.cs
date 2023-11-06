using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLooper : MonoBehaviour
{
    [SerializeField] bool m_loopOnStart = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] AnimationClip m_toPlay = default;
    [SerializeField] float m_maxDelayPerLoop = default;
    [SerializeField] float m_minDelayPerLoop = default;
    bool m_isLooping = false;
    // Start is called before the first frame update
    void Start()
    {
        if (m_loopOnStart) 
        {
            m_isLooping = true;
            StartCoroutine(StartLoop());
        }
    }
    public void BeginLoop()
    {
        if (m_isLooping) return;
        m_isLooping = true;
        StartCoroutine(StartLoop());
    }
    public void StopLoop() 
    {
        StopAllCoroutines();
        m_isLooping = false;
    }
    IEnumerator StartLoop() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(m_minDelayPerLoop, m_maxDelayPerLoop));
            
            m_anim?.Play(m_toPlay.name);
            yield return new WaitForSeconds(m_toPlay.length);
        }
    }
}
