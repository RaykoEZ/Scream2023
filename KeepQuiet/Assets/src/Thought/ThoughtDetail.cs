using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewThought", menuName = "New Thought", order = 1)]
public class ThoughtDetail : ScriptableObject
{
    // TODO:
    // Store ID + Description in game state(SOs for now, maybe json)
    // On obtaining thought bubble, instantiate + init Description from db with ID
    // On triggering dialogue with thought,
    // check ID & game state, trigger dialogue if ID has triggers and gamestate == true
    [SerializeField] string m_description = default;
    public string Id => name;
    public string Description { get => m_description; set => m_description = value; }
}
