using UnityEngine;
using TMPro;
using Curry.Explore;
public class IncomingCall : HideableUI
{
    [SerializeField] TextMeshProUGUI m_incomingNumber = default;
    string m_currentCaller = "";
    public event OnIncomingNotify OnCallAccept;
    public event OnIncomingNotify OnCallDeny;
    public void Trigger(string callerNumber, string alias = "")
    {
        m_currentCaller = string.IsNullOrEmpty(alias) ? callerNumber : alias;
        m_incomingNumber.text = m_currentCaller;
        Show();
    }
    public void Accept()
    {
        OnCallAccept?.Invoke(m_currentCaller);
        Hide();
    }
    public void Deny()
    {
        OnCallDeny?.Invoke(m_currentCaller);
        Hide();
    }
}
