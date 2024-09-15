using Curry.Explore;
public enum NpcEmotion 
{ 
    Default,
    Angry,
    Smug
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
    public void SetTalking(bool talking)
    {
        GetAnim?.SetBool("talking", talking);
    }
    #region facial expressions
    public void SetEmotion(NpcEmotion emote) 
    {
        var anim = GetAnim;
        switch (emote)
        {
            case NpcEmotion.Default:
                anim?.SetTrigger("default");
                break;
            case NpcEmotion.Angry:
                anim?.SetTrigger("angry");
                break;
            case NpcEmotion.Smug:
                anim?.SetTrigger("smug");
                break;
            default:
                anim?.SetTrigger("default");
                break;
        }
    }
    #endregion
}
