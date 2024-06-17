using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HasSecretKey_", menuName = "GameState/Check for .../HasSecretKey", order = 1)]
public class HasSecretKey : GameStateCondition
{
    public override bool Validate(SaveData save) 
    {
    
        return save.Persistent.HasSecretKey;
    }
}
