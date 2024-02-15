using UnityEngine;

namespace Curry.Explore
{
    [RequireComponent(typeof(Animator))]
    public class HideableUI : MonoBehaviour 
    {
        protected Animator GetAnim => GetComponent<Animator>();
        public virtual void Show() 
        {
            GetAnim?.SetBool("Show", true);
        }
        public virtual void Hide() 
        {
            GetAnim?.SetBool("Show", false);
        }
        public virtual void Toggle() 
        {
            bool current = GetAnim.GetBool("Show");
            GetAnim?.SetBool("Show", !current);
        }
    }
}