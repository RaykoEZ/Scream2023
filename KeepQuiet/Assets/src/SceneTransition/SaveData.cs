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
        // Has the secret key for handshake,
        // leads to an ending depending on other persistent states
        public bool HasSecretKey;
        // No. of times player launched this game after first new game
        // Triggers new game tutorial sequences for new game players
        public int ContinueCount;
        // display slightly different title/continue/new game sequences
        // depending on ending
        [JsonConverter(typeof(StringEnumConverter))]
        public Ending CurrentEnding;
        public PersistentSave( 
            bool isAriaDead,
            bool hasSecretKey, 
            int continueCount, Ending previousEnding)
        {
            AriaDead = isAriaDead;
            HasSecretKey = hasSecretKey;
            ContinueCount = continueCount;
            CurrentEnding = previousEnding;
        }
        // Default ctor, for fresh game/clear cache
        public PersistentSave() 
        {
            AriaDead = false;
            HasSecretKey = false;
            ContinueCount = 0;
            CurrentEnding = Ending.None;
        }
        public PersistentSave(PersistentSave persistent)
        {
            AriaDead = persistent.AriaDead;
            HasSecretKey = persistent.HasSecretKey;
            ContinueCount = persistent.ContinueCount;
            CurrentEnding = persistent.CurrentEnding;
        }
    }
    #endregion
    public PersistentSave Persistent;
    public bool CheckedSubjectProfile;
    // Check if player found and dragged out the Jamming device found in secret puzzle 
    // Did player resolve malware overtaking Aria?
    // Secret/True End Flag
    public WatchDisplay WatchState;
    // Freedom flag before reaching ending
    public bool FreedomRoute;
    // Where is the player looking at
    public string CurrentlyViewing;
    public HashSet<ThoughtDetail> HeldThoughts;
    public AriaState AriaStatus;
    // time of the simulation, watch displays it
    public WatchDisplay SimulationTime;
    public DateTime InitDate;
    public string InitTime;
    // New save file
    public SaveData()
    {
        SimulationTime = WatchDisplay.Present;
        InitDate = DateTime.Now;
        InitTime = InitDate.ToString("d").Replace(@"/", string.Empty);
        CheckedSubjectProfile = false;
        FreedomRoute = false;
        WatchState = WatchDisplay.None;
        Persistent = new PersistentSave();
        HeldThoughts = new HashSet<ThoughtDetail>();
        CurrentlyViewing = "RoomRight";
        AriaStatus = AriaState.Default;
    }
    public SaveData(SaveData copy)
    {
        SimulationTime = copy.SimulationTime;
        CheckedSubjectProfile = copy.CheckedSubjectProfile;
        InitDate = copy.InitDate;
        InitTime = copy.InitTime;
        FreedomRoute = copy.FreedomRoute;
        WatchState = copy.WatchState;
        Persistent = new PersistentSave(copy.Persistent);
        CurrentlyViewing = copy.CurrentlyViewing;
        HeldThoughts = new HashSet<ThoughtDetail>(copy.HeldThoughts);
        AriaStatus = new AriaState(copy.AriaStatus);
    }
}