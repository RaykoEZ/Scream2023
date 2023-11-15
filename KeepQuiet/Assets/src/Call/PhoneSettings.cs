using UnityEngine;

[CreateAssetMenu(fileName = "PhoneSetting_", menuName = "Phone Settings")]
public class PhoneSettings : ScriptableObject 
{
    [Range(0f, 1f)]
    [SerializeField] float m_volume = default;
    [SerializeField] AudioClip m_ringtone = default;
    [SerializeField] Sprite m_wallPaper = default;
    public float Volume { get => m_volume; protected set => m_volume = value; }
    public AudioClip Ringtone { get => m_ringtone; protected set => m_ringtone = value; }
    public Sprite WallPaper { get => m_wallPaper; protected set => m_wallPaper = value; }
}
