using Curry.Explore;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// A class to display a text message
public class MessageBox : HideableUI
{
    [SerializeField] TextMeshProUGUI m_content = default;
    protected bool m_typing = false;
    GameObject m_spawned;
    public void Init(string content)
    {
        m_content.text = content;
    }
    // TODO: add image to display
    public void Init(Dialogue content)
    {
        Cleanup();
        m_content.text = content.Content;
        m_spawned = Instantiate(content.SentPrefabRef, m_content.transform.parent);
        m_spawned.SetActive(true);
    }
    public void Cleanup() 
    {
        m_content.text = "";
        if (m_spawned != null) 
        {
            Destroy(m_spawned);
        }
    }
}
