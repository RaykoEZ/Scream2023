using UnityEngine;

namespace Curry.Explore
{
    [RequireComponent(typeof(Animator))]
    public class HideableUI : MonoBehaviour 
    {
        public virtual void Show() 
        {
            GetComponent<Animator>()?.SetBool("Show", true);
        }
        public virtual void Hide() 
        {
            GetComponent<Animator>()?.SetBool("Show", false);
        }
        public virtual void Toggle() 
        {
            bool current = GetComponent<Animator>().GetBool("Show");
            GetComponent<Animator>()?.SetBool("Show", !current);
        }
    }
}