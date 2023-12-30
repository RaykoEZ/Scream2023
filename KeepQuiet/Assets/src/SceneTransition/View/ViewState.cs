using UnityEngine;
// Stores info about a view (e.g. currently viewing? visible clues, clue/puzzle states)
public abstract class ViewState : MonoBehaviour
{
    [SerializeField] private Transform m_lighting = default;
    [SerializeField] private Transform m_vfx = default;
    [SerializeField] private Transform m_background = default;
    public abstract string Name { get; }
    public Transform Lighting => m_lighting;
    public Transform Vfx => m_vfx;
    public Transform Background => m_background;
    public virtual void SetVisual(bool isOn)
    {
        Lighting.gameObject.SetActive(isOn);
        Vfx.gameObject.SetActive(isOn);
        Background.gameObject.SetActive(isOn);
    }
}
