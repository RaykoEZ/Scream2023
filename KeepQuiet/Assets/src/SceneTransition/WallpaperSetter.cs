using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WallpaperSetter : MonoBehaviour, ISettingUpdateListener<PhoneSettings>
{
    [SerializeField] PhoneSettings m_setting = default;
    [SerializeField] Image m_wallPaper = default;
    // Use this for initialization
    void Start()
    {
        m_setting.Listen(this);
        SetWallPaper(m_setting.GetWallPaper());
    }

    public void SetWallPaper(Sprite val) 
    {
        m_wallPaper.sprite = val;
    }
    public void OnUpdate(PhoneSettings setting) 
    {
        SetWallPaper(setting.GetWallPaper());
    }
}
