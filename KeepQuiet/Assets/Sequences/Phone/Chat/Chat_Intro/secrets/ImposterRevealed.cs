using System;
using UnityEngine;



[CreateAssetMenu(fileName = "ImposterRevealed_", menuName = "GameState/Check for .../ImposterRevealed", order = 1)]
public class ImposterRevealed : GameStateCondition
{
    
    public override bool Validate(SaveData save) 
    {
    
        return true;
    }
}
