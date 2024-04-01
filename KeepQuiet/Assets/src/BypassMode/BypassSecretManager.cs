using Curry.Events;
using UnityEngine;
// Class to listen to event cue to create breadcrumbs for revealing secret bypass button
// Listens and propagates event when player drags the correct file into the game
public class BypassSecretManager : SecretManager
{
    [SerializeField] HiddenNodeHandller m_hiddenNodeSignal = default;
    [SerializeField] ClueFileTemplate m_initialReadme = default;
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
        m_writer.WriteTextToDesktop(m_initialReadme.Filename, FileUtil.s_operatorFolder, m_initialReadme.Content, encode: true);
        m_hiddenNodeSignal?.Init();
        base.TriggerSecret();
    }
}

public class FileWriter 
{
    public void WriteTextToDesktop(string filename, string folder, string content, bool encode = false) 
    {
        if (encode) 
        {
            FileUtil.EncodeTextTo(FileUtil.s_desktopPath, folder, filename, content);
        } 
        else 
        {
            FileUtil.RawTextTo(FileUtil.s_desktopPath, folder, filename, new string[] { content });
        }
    }

    public void WriteCogniToDesktop(string filename, string folder, string content) 
    {
        FileUtil.EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.cogni", content);
    }
    public void WriteKeyToDesktop(string filename, string folder, string content)
    {
        FileUtil.EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.key", content);
    }
    public void WriteJammerToDesktop(string filename, string folder, string content)
    {
        FileUtil.EncodeTextTo(FileUtil.s_desktopPath, folder, $"{filename}.jamm", content);
    }
    public void SendPngToDesktop(string filename, string foldername, Texture2D texture) 
    {
        FileUtil.PngImageTo(FileUtil.s_desktopPath, foldername, filename, texture);
    }
}