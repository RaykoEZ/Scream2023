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
        // Responded to handshake with secret answer,
        // leads to an ending depending on other persistent states
        public bool HandshakeComplete;
        // No. of times player launched this game after first new game
        // Triggers new game tutorial sequences for new game players
        public int ContinueCount;
        // display slightly different title/continue/new game sequences
        // depending on ending
        [JsonConverter(typeof(StringEnumConverter))]
        public Ending CurrentEnding;
        public PersistentSave( bool isAriaDead,
            bool handshakeComplete, 
            int continueCount, Ending previousEnding)
        {
            AriaDead = isAriaDead;
            HandshakeComplete = handshakeComplete;
            ContinueCount = continueCount;
            CurrentEnding = previousEnding;
        }
        // Default ctor, for fresh game/clear cache
        public PersistentSave() 
        {
            AriaDead = false;
            HandshakeComplete = false;
            ContinueCount = 0;
            CurrentEnding = Ending.None;
        }
        public PersistentSave(PersistentSave persistent)
        {
            AriaDead = persistent.AriaDead;
            HandshakeComplete = persistent.HandshakeComplete;
            ContinueCount = persistent.ContinueCount;
            CurrentEnding = persistent.CurrentEnding;
        }
    }
    #endregion
    public PersistentSave Persistent;
    // Check if player found and dragged out the Jamming device found in secret puzzle 
    // Did player resolve malware overtaking Aria?
    // Secret/True End Flag
    public bool IsMalwareGone;
    public bool WatchUnlocked;
    // The dial puzzle to start Jammer trigger
    public bool DialSolved;
    // Did the player take the bat in Room Left?
    // Bat can be dragged out and dragged in
    public bool BatTaken;
    // Special torch unlocked after Jammer is taken out, game
    public bool SpecialTorchUnlocked;
    // Clock is revealed after setting system clock to clued time frame
    // Player can hit the clock to reveal more clues
    public bool RevealClock;
    // Freedom flag before reaching ending
    public bool FreedomRoute;
    // Where is the player looking at
    public string CurrentlyViewing;
    public AriaState AriaStatus;
    public SaveData()
    {
        IsMalwareGone = false;
        DialSolved = false;
        BatTaken = false;
        RevealClock = false;
        SpecialTorchUnlocked = false;
        FreedomRoute = false;
        WatchUnlocked = false;
        Persistent = new PersistentSave();
        CurrentlyViewing = "RoomRight";
        AriaStatus = AriaState.Default;
    }
    public SaveData(SaveData copy)
    {
        IsMalwareGone = copy.IsMalwareGone;
        BatTaken = copy.BatTaken;
        SpecialTorchUnlocked = copy.SpecialTorchUnlocked;
        DialSolved = copy.DialSolved;
        RevealClock = copy.RevealClock;
        FreedomRoute = copy.FreedomRoute;
        WatchUnlocked = copy.WatchUnlocked;
        Persistent = copy.Persistent;
        CurrentlyViewing = copy.CurrentlyViewing;
        AriaStatus = new AriaState(copy.AriaStatus);
    }
}

