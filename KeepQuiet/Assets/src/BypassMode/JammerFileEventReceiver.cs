public class JammerFileEventReceiver : ExternalFileEventReceiver
{
    protected override IFileValidator Validator => m_isValid;
    JammerFileValidator m_isValid = new JammerFileValidator();
}
