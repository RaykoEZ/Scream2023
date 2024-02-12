using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class CustomShapeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.05f;
    }
}
