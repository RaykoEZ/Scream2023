using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InspectPoster : InspectionDisplay
{
    [Range(0f, 1f)]
    [SerializeField] float m_scareRate = 0.1f;
    int m_numScareTrigger = 0;
    public void TryScare() 
    {
        if (m_numScareTrigger > 0) return;

        float rand = Random.Range(0f, 1f);
        if (rand < m_scareRate) 
        {
            m_anim?.SetTrigger("scare");
            m_numScareTrigger++;
        }
    }
    void NormalState() 
    {
        m_anim?.SetBool("glitch", false);
    }
    void GlitchState() 
    {
        m_anim?.SetBool("glitch", true);
    }

    public override IEnumerator OnExit()
    {
        TryScare();
        yield return new WaitForSeconds(0.05f);
    }
}
public class InspectVent : InspectionDisplay
{
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}
public class InspectDoor : InspectionDisplay
{
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}
public class InspectClock : InspectionDisplay
{
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}
public class InspectDocument : InspectionDisplay
{
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}
public class InspectCans : InspectionDisplay
{
    public override IEnumerator OnExit()
    {
        yield return null;
    }
}