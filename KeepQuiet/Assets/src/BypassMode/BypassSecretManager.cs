using UnityEngine;
// Class to listen to event cue to create breadcrumbs for revealing secret bypass button
// Listens and propagates event when player drags the correct file into the game
public class BypassSecretManager : SecretManager
{
    [SerializeField] HiddenNodeHandller m_hiddenNodeSignal = default;
    [SerializeField] ClueFileTemplate m_jamHint = default;
    FileWriter m_writer = new FileWriter();
    public override SecretEventFlag EventFlagToRaise => SecretEventFlag.HiddenNodeRevealed;

    private void Start()
    {
        m_hiddenNodeSignal.SecretUnlocked += OnSecretReveal;
    }
    protected override void OnSecretReveal()
    {
        OnRevealSequence();
        base.OnSecretReveal();
    }
    // Play sequence when secret is revealed
    void OnRevealSequence() 
    {
    
    }
    public override void TriggerSecret() 
    {
        m_writer.WriteTextToDesktop(m_jamHint.Filename, FileUtil.s_operatorFolder, m_jamHint.Content, encode: true);
        m_hiddenNodeSignal?.Init();
        base.TriggerSecret();
    }
}
