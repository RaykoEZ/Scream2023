using Curry.Explore;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// A class to display a text message
public class MessageBox : HideableUI
{
    [SerializeField] TextMeshProUGUI m_content = default;

    protected bool m_typing = false;
    GameObject m_spawned;
    // TODO: add image to display
    public void Init(Dialogue content)
    {
        Cleanup();
        m_content.text = content.Content;
        GameObject toSpawn = content.SentPrefabRef;
        if (toSpawn != null) 
        {
            m_spawned = Instantiate(toSpawn, m_content.transform.parent);
            m_spawned.SetActive(true);
        }
    }
    public void Typing() 
    {
        GetAnim?.SetBool("Show", false);
        GetAnim?.SetBool("Typing", true);
    }
    public override void Show()
    {
        GetAnim?.SetBool("Typing", false);
        base.Show();
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
