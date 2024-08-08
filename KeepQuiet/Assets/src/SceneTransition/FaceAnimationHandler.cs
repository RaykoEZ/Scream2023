using Curry.Explore;
public class ScaryAnimationHandler : HideableUI 
{
    public void EnterScene()
    {
        GetAnim?.SetTrigger("enter");
    }
    public void ExitScene()
    {
        GetAnim?.SetTrigger("exit");
    }
}
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
    public void SetTalking(bool talking)
    {
        GetAnim?.SetBool("talking", talking);
    }
}
