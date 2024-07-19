using System;
using UnityEngine;
// A bubble dragged to chat for starting a dialogue
// Obtained from clues and chatting with NPCs
[Serializable]
[CreateAssetMenu(fileName = "Tht_", menuName = "New Thought", order = 1)]
public class ThoughtDetail : ScriptableObject
{
    // TODO:
    // Store ID + Description in game state(maybe json)
    // On obtaining thought bubble, instantiate + init Description from db with ID
    // On triggering dialogue with thought,
    // check ID & game state, trigger dialogue if ID has triggers and gamestate == true
    [TextArea]
    [SerializeField] string m_description = default;
    public string Id => name;
    public string Description { get => m_description; }
}
// Contains outcome of a thought being dropped
[Serializable]
public struct ThoughtDropResult
{
    [SerializeField] ThoughtDetail m_toDrop;
    [SerializeField] DialogueNode m_outcome;
    public ThoughtDetail ToDrop => m_toDrop;
    public DialogueNode Outcome => m_outcome; 
}
