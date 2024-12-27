using TMPro;
using UnityEngine;

public class PlayerNameLabelSetter: MonoBehaviour
{
    [SerializeField] string m_playerNamePrefix = default;
    [SerializeField] TextMeshProUGUI m_playerIdLabel = default;
    public void Init(SaveData save)
    {
        m_playerIdLabel.text = $"{m_playerNamePrefix}_{save.Persistent.PlayerID}";
    }
}
