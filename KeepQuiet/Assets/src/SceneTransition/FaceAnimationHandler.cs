using Curry.Explore;
public enum NpcEmotion 
{ 
    Default,
    Angry,
    Smug,
    Surprise,
    Happy
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
    public void SetTalking(NpcEmotion emote, bool talking)
    {
        // switch off talking
        if (!talking) 
        {
            GetAnim?.SetBool("talk_default", talking);
            GetAnim?.SetBool("talk_happy", talking);
            return;
        }
        // Switch on talk animation, smiling for smug face
        switch (emote)
        {
            case NpcEmotion.Smug | NpcEmotion.Happy:
                GetAnim?.SetBool("talk_default", false);
                GetAnim?.SetBool("talk_happy", talking);
                break;
            default:
                GetAnim?.SetBool("talk_default", talking);
                GetAnim?.SetBool("talk_happy", false);
                break;
        }
    }
    #region facial expressions
    public void SetEmotion(NpcEmotion emote) 
    {
        var anim = GetAnim;
        switch (emote)
        {
            case NpcEmotion.Angry:
                anim?.SetTrigger("angry");
                break;
            case NpcEmotion.Smug:
                anim?.SetTrigger("smug");
                break;
            case NpcEmotion.Surprise:
                anim?.SetTrigger("surprise");
                break;
            default:
                anim?.SetTrigger("default");
                break;
        }
    }
    #endregion
}
