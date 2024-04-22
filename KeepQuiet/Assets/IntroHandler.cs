using Curry.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//Transitions intro sequences in intro scene
public class IntroHandler : MonoBehaviour
{
    [SerializeField] LevelLoader m_level = default;
    [SerializeField] PlayableDirector m_introSequence = default;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionToTitle()); 
    }
    IEnumerator TransitionToTitle() 
    {
        m_introSequence?.Play();
        //TODO: Play a sequence and go to title screen
        yield return new WaitForSeconds(1f);
        m_level?.LoadScene(1);
    }
}
