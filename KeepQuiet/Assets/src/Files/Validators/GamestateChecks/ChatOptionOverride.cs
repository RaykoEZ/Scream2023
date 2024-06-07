using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "optAlt_", menuName = "Chat/Reply Option Transformation")]
public class ChatOptionOverride : GameContentOverride<List<ChatOption>>
{
    // Check for all gamestate condtions to fullfil
    [SerializeField] List<ChatOption> m_newOptions = default;
    protected override List<ChatOption> ToOverride => new List<ChatOption>(m_newOptions);
}
