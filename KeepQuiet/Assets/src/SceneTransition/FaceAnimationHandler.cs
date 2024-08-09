using Curry.Explore;
public class FaceAnimationHandler : HideableUI 
{
    public void EnterScene()
    {
        GetAnim?.SetTrigger("enter");
    }
    public void ExitScene()
    {
        GetAnim?.SetTrigger("exit");
    }
    public void SetTalking(bool talking)
    {
        GetAnim?.SetBool("talking", talking);
    }
    #region facial expressions
    public void Curious()
    {
        GetAnim?.SetTrigger("curious");
    }
    public void Angry()
    {
        GetAnim?.SetTrigger("angry");
    }
    public void Smug()
    {
        GetAnim?.SetTrigger("smug");
    }
    #endregion
}
