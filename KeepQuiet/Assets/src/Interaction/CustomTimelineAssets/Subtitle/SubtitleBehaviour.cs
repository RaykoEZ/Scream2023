using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleBehaviour : PlayableBehaviour 
{
    public Color TextColour;
    public string SubtitleText;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        text.text = SubtitleText;
        text.color = new Color(TextColour.r, TextColour.g, TextColour.b, info.weight);
    }
}
