using Curry.Explore;
using TMPro;
using UnityEngine;
// A class to display a text message
public class MessageBox : HideableUI
{
    [SerializeField] protected TextMeshProUGUI m_content = default;
    [SerializeField] protected TextMeshProUGUI m_name = default;
    [SerializeField] AudioSource m_audio = default;
    [SerializeField] AttachmentEventHandler m_attachment = default;
    protected bool m_typing = false;
    // TODO: add image to display
    public virtual void Init(Dialogue content)
    {
        Cleanup();
        m_content.text = content.ChatLog.Content;
        m_name.text = content.ChatLog.WhoSpoke;
        //GameObject toSpawn = content.ChatLog.SentPrefabRef;
        m_audio.mute = !content.HasAudio;
        //bool hasAttachment = toSpawn != null;
        //m_attachment.enabled = hasAttachment;
        //if (hasAttachment) 
        //{
        //    m_attachment?.Init(toSpawn.name, toSpawn);
        //}
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
        m_attachment.Shutdown();
        m_attachment.enabled = false;
    }
}