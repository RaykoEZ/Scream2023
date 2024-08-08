using Curry.Explore;
using System.Collections;
using TMPro;
using UnityEngine;
// Displays aria talking & entrtance/exit
public class AriaCloseupHandler : MonoBehaviour 
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
    public void StartTalk(string content) 
    {
        m_message?.Hide();
        StopAllCoroutines();
        StartCoroutine(Talk_Internal(content));
    }
    IEnumerator Talk_Internal(string content) 
    {
        m_content.text = content;
        m_face?.SetTalking(true);
        yield return new WaitForSeconds(0.05f);
        m_message?.Show();
        yield return new WaitForSeconds(1f + (content.Length * 0.1f));
        m_face?.SetTalking(false);
    }
}
