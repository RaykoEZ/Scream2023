using System;
using UnityEngine;
public class WatchReceiver : ExternalFileReceiver 
{
    [SerializeField] SnapshotWatch m_toSpawn = default;
    protected override IFileValidator Validator => m_isValid;
    WatchFileValidator m_isValid = new WatchFileValidator();
}
