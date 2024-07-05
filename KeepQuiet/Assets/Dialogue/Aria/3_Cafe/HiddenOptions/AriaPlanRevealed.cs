using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AriaPlanRevealed_", menuName = "GameState/Check for .../AriaPlanRevealed", order = 1)]
public class AriaPlanRevealed : GameStateCondition
{
    
    public override bool Validate(SaveData save) 
    {
    
        return true;
    }
}
