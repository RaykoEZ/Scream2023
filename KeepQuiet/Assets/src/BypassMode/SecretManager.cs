using UnityEngine;
using UnityEngine.Playables;

public abstract class SecretManager : MonoBehaviour 
{
    [SerializeField] protected PlayableDirector m_director = default;
    public event OnSecretUpdate SecretUnlocked;
    public virtual void TriggerSecret() 
    {
        SecretUnlocked?.Invoke();
    }
}
