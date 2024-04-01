using UnityEngine;
using UnityEngine.Playables;
public enum SecretEventFlag
{
    HiddenNodeRevealed,
    None
}
public delegate void OnSecretReveal(SecretEventFlag flag);
public abstract class SecretManager : MonoBehaviour 
{
    [SerializeField] protected PlayableDirector m_director = default;
    public event OnSecretUpdate SecretUnlocked;
    public event OnSecretReveal SecretRevealed;
    public abstract SecretEventFlag EventFlagToRaise { get; }
    public virtual void TriggerSecret() 
    {
        SecretUnlocked?.Invoke();
    }
    protected virtual void OnSecretReveal()
    {
        // play sequence
        SecretRevealed?.Invoke(EventFlagToRaise);
    }
}
