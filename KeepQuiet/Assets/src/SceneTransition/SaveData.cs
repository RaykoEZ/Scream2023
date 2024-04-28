using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
// Contains persistent data for changing game environment
// and state of quicksaved game state
[Serializable]
public class SaveData
{
    #region Persistent Flags for ending & environment changes
    // State data that isn't reset on New Game
    // Reset this by clearing cache
    [Serializable]
    public class PersistentSave
    {
        // If player choose to hit Aria with the bat, we reach Bad End,
        // a different title & New Game & Continue if Aria is dead
        public bool AriaDead;
        // Leads to answer for handshake
        public bool SecretKeyObtained;
        // Responded to handshake with secret answer,
        // leads to an ending depending on other persistent states
        public bool HandshakeComplete;
        // No. of times player launched this game after first new game
        // Triggers new game tutorial sequences for new game players
        public int ContinueCount;
        // display slightly different title/continue/new game sequences
        // depending on ending
        [JsonConverter(typeof(StringEnumConverter))]
        public Ending PreviousEnding;
        public PersistentSave( bool isAriaDead,
            bool secretKeyObtained, bool handshakeComplete, 
            int continueCount, Ending previousEnding)
        {
            AriaDead = isAriaDead;
            SecretKeyObtained = secretKeyObtained;
            HandshakeComplete = handshakeComplete;
            ContinueCount = continueCount;
            PreviousEnding = previousEnding;
        }
        // Default ctor, for fresh game/clear cache
        public PersistentSave() 
        {
            AriaDead = false;
            SecretKeyObtained = false;
            HandshakeComplete = false;
            ContinueCount = 0;
            PreviousEnding = Ending.None;
        }
        public PersistentSave(PersistentSave persistent)
        {
            AriaDead = persistent.AriaDead;
            SecretKeyObtained = persistent.SecretKeyObtained;
            HandshakeComplete = persistent.HandshakeComplete;
            ContinueCount = persistent.ContinueCount;
            PreviousEnding = persistent.PreviousEnding;
        }
    }
    #endregion
    public PersistentSave Persistent;
    // Check if player found and dragged out the Jamming device found in secret puzzle 
    // Bad End Flag 
    public bool IsJammerRemoved;
    // Did player resolve malware overtaking Aria?
    // Secret/True End Flag
    public bool IsMalwareIsolated;
    // The dial puzzle to start Jammer trigger
    public bool DialPuzzleSolved;
    // Did the player take the bat in Room Left?
    // Bat can be dragged out and dragged in
    public bool BatUnlocked;
    // Special torch unlocked after Jammer is taken out, game
    public bool SpecialTorchUnlocked;
    // Clock is revealed after setting system clock to clued time frame
    // Player can hit the clock to reveal more clues
    public bool RevealClock;
    // Where is the player looking at
    public string CurrentlyViewing;
    public AriaState AriaStatus;
    public SaveData()
    {
        IsJammerRemoved = false;
        IsMalwareIsolated = false;
        DialPuzzleSolved = false;
        BatUnlocked = false;
        RevealClock = false;
        SpecialTorchUnlocked = false;
        Persistent = new PersistentSave();
        CurrentlyViewing = "RoomRight";
        AriaStatus = AriaState.Default;
    }
    public SaveData(SaveData copy)
    {
        IsJammerRemoved = copy.IsJammerRemoved;
        IsMalwareIsolated = copy.IsMalwareIsolated;
        BatUnlocked = copy.BatUnlocked;
        SpecialTorchUnlocked = copy.SpecialTorchUnlocked;
        DialPuzzleSolved = copy.DialPuzzleSolved;
        RevealClock = copy.RevealClock;
        Persistent = copy.Persistent;
        CurrentlyViewing = copy.CurrentlyViewing;
        AriaStatus = new AriaState(copy.AriaStatus);
    }
}

