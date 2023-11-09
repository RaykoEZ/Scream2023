using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DialogueTree
{
    [SerializeField] string m_chattingWith = default;
    // All dialogue nodes reachable in the tree, do not mix unreachable nodes
    [SerializeField] List<DialogueNode> m_dialogueNodes = default;
    public string ChattingWith => m_chattingWith;
    public IReadOnlyList<DialogueNode> DialogueNodes => m_dialogueNodes;

}
// A class to traverse and to keep the state of Dialogue nodes
[Serializable]
[CreateAssetMenu(fileName = "DialogueCollection_", menuName = "Dialogue/Collection", order = 0)]
public class DialogueCollection : ScriptableObject
{
    [SerializeField] List<DialogueTree> m_dialogueTrees = default;
    public DialogueTree GetTree(string chattingWith) 
    {
        return m_dialogueTrees.Find(
            (tree) => 
            { 
                return tree.ChattingWith == chattingWith; 
            });
    }
    public DialogueNode GetNode(string chattingWith, int messageIndex) 
    {
        if (messageIndex < 0) return null;
        DialogueTree tree = GetTree(chattingWith);
        if (tree == null) return null;
        var nodes = tree.DialogueNodes;
        // range check
        if (messageIndex >= nodes.Count) return null;
        return nodes[messageIndex];
    }
}
