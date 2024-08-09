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
    #region scary faces
    public void BaseScaryFace() 
    {
        GetAnim?.SetTrigger("base");
    }
    public void DarkerFace()
    {
        GetAnim?.SetTrigger("dark");
    }
    public void BloodyFace()
    {
        GetAnim?.SetTrigger("bloody");
    }
    public void ShadowFigure()
    {
        GetAnim?.SetTrigger("hidden");
    }
    #endregion
}
