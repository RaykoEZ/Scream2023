using Curry.Explore;
using System.Collections;
using TMPro;
using UnityEngine;
// Displays aria talking & entrtance/exit
public class CloseupHandler : MonoBehaviour 
{
    [SerializeField] HideableUI m_message = default;
    [SerializeField] FaceAnimationHandler m_face = default;
    [SerializeField] TextMeshProUGUI m_content = default;
    public void EnterScene()
    {
        m_face?.EnterScene();
    }
    public void ExitScene()
    {
        m_face?.ExitScene();
    }
    // Start a line of speech
    public void StartTalk(string content, NpcEmotion emote) 
    {
        m_message?.Hide();
        StopAllCoroutines();
        StartCoroutine(Talk_Internal(content, emote));
    }
    IEnumerator Talk_Internal(string content, NpcEmotion emote) 
    {
        m_content.text = content;
        m_face?.SetEmotion(emote);
        m_face?.SetTalking(emote, true);
        yield return new WaitForSeconds(0.05f);
        m_message?.Show();
        yield return new WaitForSeconds((content.Length * 0.1f));
        m_face?.SetTalking(emote, false);
    }
}
