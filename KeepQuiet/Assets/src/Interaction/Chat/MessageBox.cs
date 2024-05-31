﻿using Curry.Explore;
using TMPro;
using UnityEngine;
// A class to display a text message
public class MessageBox : HideableUI
{
    [SerializeField] TextMeshProUGUI m_content = default;
    public void Init(string name, string content)
    {
        m_content.text = content;
    }
}
