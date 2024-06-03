using UnityEngine;
using UnityEngine.UI;
// Allows an image's raycast surface to fit with its shape 
[RequireComponent(typeof(Image))]
public class CustomImageHitZone : MonoBehaviour 
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.05f;
    }
}
