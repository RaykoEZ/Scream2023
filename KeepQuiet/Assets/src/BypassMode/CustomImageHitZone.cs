using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomImageHitZone : MonoBehaviour 
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.05f;
    }
}
