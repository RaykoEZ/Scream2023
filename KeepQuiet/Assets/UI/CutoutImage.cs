using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
// A image that inverts UI Mask effects
// (Mask will now hide parts of this image, overlapping the mask region) 
public class CutoutImage : Image
{
    public override Material materialForRendering
    {
        get 
        {
            Material ret = new Material(base.materialForRendering);
            ret.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return ret;
        }
    }

}
