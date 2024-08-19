using Curry.Explore;
using System;
using System.Collections;
using UnityEngine;

public class InspectPoster : InspectionDisplay
{
    [Range(0f, 1f)]
    [SerializeField] float m_scareRate = 0.1f;
    [SerializeField] WatchKey m_hiddenKey = default;
    int m_numScareTrigger = 0;
    void Start()
    {
        m_hiddenKey?.gameObject?.SetActive(false);
    }
    public override void Init(SaveData save) 
    {
        //show hidden key
        m_hiddenKey?.gameObject?.SetActive(true);
        if (save.Persistent.AriaDead) 
        {
            GlitchState();
        }
        else 
        {
            NormalState();
        }
    }
    public void TryScare() 
    {
        if (m_numScareTrigger > 0) return;

        float rand = UnityEngine.Random.Range(0f, 1f);
        if (rand < m_scareRate) 
        {
            m_anim?.SetTrigger("scare");
            m_numScareTrigger++;
        }
    }
    public void NormalState() 
    {
        m_anim?.SetBool("glitch", false);
    }
    public void GlitchState() 
    {
        m_anim?.SetBool("glitch", true);
    }
    public override IEnumerator OnExit()
    {
        TryScare();
        //hide hidden key
        m_hiddenKey?.gameObject?.SetActive(false);
        yield return new WaitForSeconds(0.05f);
    }
}