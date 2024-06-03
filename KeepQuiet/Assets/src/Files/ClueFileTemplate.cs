using UnityEngine;
// stores details to writing a file to the player's destop when triggering a secret event
[CreateAssetMenu(fileName = "ClueFile_", menuName = "File to write to player's desktop", order = 0)]
public class ClueFileTemplate : ScriptableObject 
{
    [SerializeField] bool m_isEncoded = default;
    [SerializeField] string m_filename = default;
    [TextArea]
    [SerializeField] string content = default;
    public bool IsEncoded { get => m_isEncoded; }
    public string Filename { get => m_filename; }
    public string Content { get => content; }
}
