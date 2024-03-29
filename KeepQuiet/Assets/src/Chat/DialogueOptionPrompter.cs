﻿using System.Collections.Generic;
using UnityEngine;
public delegate void OnPlayerChosen(DialogueNode chosen, int choiceIndex);
public class DialogueOptionPrompter : MonoBehaviour
{
    [SerializeField] int m_maxOptions = 3;
    [SerializeField] DialogueOption m_optionPrefab = default;
    [SerializeField] Transform m_contentParent = default;
    public event OnPlayerChosen OnChosen;
    List<DialogueOption> m_options;
    void Awake() 
    {
        m_options = new List<DialogueOption>(m_maxOptions);
        for (int i = 0; i < m_maxOptions; ++i) 
        {
            DialogueOption instance = Instantiate(m_optionPrefab, m_contentParent);
            instance.Hide();
            m_options.Add(instance);
        }
    }
    
    public void PromptOption(IReadOnlyList<DialogueNode> options) 
    {
        if (options.Count == 0) return;
        HideAll();
        int numOptions = Mathf.Clamp(options.Count, 1, m_maxOptions);
        for (int i = 0; i < numOptions; ++i) 
        {
            m_options[i].Init(options[i]);
            m_options[i].OnChosen += OnOptionChosen;
            m_options[i].Show();
        }
    }
    void OnOptionChosen(DialogueOption chosen)
    {
        HideAll();
        OnChosen?.Invoke(chosen.OptionValue, m_options.IndexOf(chosen));
    }
    void HideAll()
    {
        foreach (var opt in m_options)
        {
            opt.OnChosen -= OnOptionChosen;
            opt.Hide();
        }
    }
}
