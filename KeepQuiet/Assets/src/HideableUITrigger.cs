﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Curry.Explore
{

    // This triggers multiple ui elements to show/hide at the same time.
    [Serializable]
    public class HideableUITrigger
    {
        [SerializeField] List<HideableUI> m_uiToHide = default;
        public void Show() 
        {
            foreach (var item in m_uiToHide)
            {
                item.Show();
            }    
        }
        public void Hide() 
        {
            foreach (var item in m_uiToHide)
            {
                item.Hide();
            }
        }
    }
}