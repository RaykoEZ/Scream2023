using UnityEngine;

public class AudioSettingHandler : MonoBehaviour , ISettingUpdateListener<PhoneSettings>
{
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] AudioSource m_audio = default;
    void OnEnable()
    {
        m_setting?.Listen(this);
    }
    void OnDisable()
    {
        m_setting?.Unlisten(this);
    }
    void Start() 
    {
        m_audio.volume = m_setting.GetVolume();
    }
    public void OnUpdate(PhoneSettings updated)
    {
        m_audio.volume = updated.GetVolume();
    }
}
