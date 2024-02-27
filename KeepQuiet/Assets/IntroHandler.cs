using Curry.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Transitions intro sequences in intro scene
public class IntroHandler : MonoBehaviour
{
    [SerializeField] LevelLoader m_level = default;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionToTitle()); 
    }
    IEnumerator TransitionToTitle() 
    {
        //TODO: Play a sequence and go to title screen
        yield return new WaitForSeconds(0.5f);
        m_level?.LoadScene(0);
    }
}
