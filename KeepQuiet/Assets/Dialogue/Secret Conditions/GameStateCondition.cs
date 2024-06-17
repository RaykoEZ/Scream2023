using UnityEngine;

public abstract class GameStateCondition : ScriptableObject , IStateValidator<SaveData>
{
    public abstract bool Validate(SaveData save);
}
