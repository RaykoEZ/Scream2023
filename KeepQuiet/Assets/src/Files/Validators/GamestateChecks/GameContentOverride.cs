using System.Collections.Generic;
using UnityEngine;

public abstract class GameContentOverride<T> : ScriptableObject where T : class
{
    [SerializeField] protected List<GameStateCondition> m_conditions = default;
    protected abstract T ToOverride { get; }

    public bool CheckForOptions(SaveData save, out T result)
    {
        bool ret = false;
        foreach (var item in m_conditions)
        {
            ret &= item.Validate(save);
        }
        result = ret ? ToOverride : null;
        return ret;
    }
}
