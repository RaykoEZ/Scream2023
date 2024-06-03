public class KeyFileReceiver : ExternalFileReceiver 
{
    protected override IFileValidator Validator => m_isValid;
    KeyFileValidator m_isValid = new KeyFileValidator();
}
