using UnityEngine;
using UnityEngine.Playables;
// Class to listen to event cue to create breadcrumbs for revealing secret bypass button
// Listens and propagates event when player drags the correct file into the game
public class BypassSecretManager : SecretManager
{
    [SerializeField] PlayableAsset m_onReveal = default;
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
        m_hiddenNodeSignal.SecretUnlocked -= OnSecretReveal;
        base.OnSecretReveal();
    }
    // Play sequence when secret is revealed
    void OnRevealSequence() 
    {
        m_director?.Play(m_onReveal);
    }
    public override void TriggerSecret() 
    {
        m_writer.WriteTextToDesktop(m_jamHint.Filename, FileUtil.s_operatorFolder, m_jamHint.Content, encode: true);
        base.TriggerSecret();
    }
}
