using Curry.Events;
using TMPro;
using UnityEngine;

public class AriaTalkReciever : MonoBehaviour
{
    [SerializeField] AriaCloseupHandler m_closeupHandle = default;
    bool m_isTalking = false;
    public bool IsTalking { get => m_isTalking; set => m_isTalking = value; }
    public void OnNewLine(string newLine)
    {
        if (!m_isTalking) return;
        m_closeupHandle?.StartTalk(newLine);
    }
}