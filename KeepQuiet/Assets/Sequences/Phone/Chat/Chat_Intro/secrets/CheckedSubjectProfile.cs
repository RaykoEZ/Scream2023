using UnityEngine;
[CreateAssetMenu(fileName = "CheckedSubjectProfile_", menuName = "GameState/Check for .../CheckedSubjectProfile", order = 1)]
public class CheckedSubjectProfile : GameStateCondition
{
    public override bool Validate(SaveData save)
    {
        return save.CheckedSubjectProfile;
    }
}
