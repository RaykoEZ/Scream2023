using Curry.Events;
using UnityEngine;

public class BypassSecretManager : SecretManager
{
    [SerializeField] HiddenNodeHandller m_hiddenNodeSignal = default;
    private void Start()
    {
        m_hiddenNodeSignal.SecretUnlocked += OnNodeReveal;
    }
    public override void TriggerSecret() 
    {
        m_hiddenNodeSignal?.Init();
        base.TriggerSecret();
    }
    void OnNodeReveal() 
    { 
        // play sequence
    }
}
