using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StepDisplayHandler : MonoBehaviour
{
    [SerializeField] protected bool m_blockBackground = default;
    [SerializeField] StepDisplayHandler m_nextDisplay = default;
    protected bool m_isActive = true;
    protected bool m_hasTriggeredOnce = false;
    protected int m_current = 0;
    protected abstract IReadOnlyList<IStepDisplayContent> Steps { get; }
    protected virtual float HoldAfterDisplay => Steps[m_current].DisplayContent.Length * 0.05f;
    protected Coroutine m_displaying;
    public bool IsActive { get => m_isActive; private set => m_isActive = value; }
    public bool HasTriggeredOnce { get => m_hasTriggeredOnce; }
    public bool BlockBackground { get => m_blockBackground; }
    public virtual StepDisplayHandler NextDisplay { get => m_nextDisplay; }
    public abstract void Begin();
    public abstract void End();
    protected abstract void Display();
    public virtual bool Next()
    {
        int next = ++m_current;
        //end this tutorial sequence if current index is at the end
        bool hasStepsLeft = next < Steps.Count;
        // ignore spamming
        if (!hasStepsLeft || m_displaying != null)
        {
            return hasStepsLeft;
        }
        // increment sequence
        else
        {
            // transition not needed if we show next step instantly
            m_displaying = StartCoroutine(Next_Internal());
        }
        return hasStepsLeft;
    }
    protected IEnumerator ShowCurrent()
    {
        var step = Steps[m_current];
        Display();
        yield return new WaitForSeconds(HoldAfterDisplay);
        OnStepFinish();
        m_displaying = null;
    }
    protected virtual void OnStepFinish() { }
    protected virtual IEnumerator Next_Internal()
    {
        yield return new WaitForSeconds(0.1f);
        if (m_current < Steps.Count)
        {
            yield return ShowCurrent();
        }
        m_displaying = null;
    }
}
