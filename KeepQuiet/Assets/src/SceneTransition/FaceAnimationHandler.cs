using Curry.Explore;
public enum NpcEmotion 
{ 
    Default = 0,
    Angry = 1,
    Smug = 2,
    Surprise = 3,
    Happy = 4
}
public class FaceAnimationHandler : HideableUI 
{
    public void EnterScene()
    {
        GetAnim?.SetBool("enterScene", true);
    }
    public void ExitScene()
    {
        GetAnim?.SetBool("enterScene", false);
    }
    public void SetTalking(bool talking)
    {
        GetAnim?.SetBool("talking", talking);
    }
    #region facial expressions
    public void SetEmotion(NpcEmotion emote) 
    {
        var anim = GetAnim;
        anim?.SetInteger("emoteValue", (int)emote);
    }
    #endregion
}
