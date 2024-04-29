using System.Collections;
using UnityEngine;
// A script on a clue display to notify clue inspection display handler
public abstract class InspectionDisplay : MonoBehaviour 
{
    [SerializeField] protected Animator m_anim = default;
    public abstract IEnumerator OnExit();
}
