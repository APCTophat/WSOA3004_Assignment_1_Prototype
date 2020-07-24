using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterSoundArrangement : MonoBehaviour
{
    public AudioSource thirdActionAudio;
    public AudioSource fitnessAudio;
    public AudioSource otherActsAudio;
    // Start is called before the first frame update
    void Start()
    {
        // script needs to be attached to the main camera with an audio listener as well
        // audiosource gameobjs(with correlating name) should be dragged and dropped onto the script
        //ASN means Action Script's Name.
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if () // needs an instance on the actions script where the hamster is exercising to be false
        {
            //audios have to be made onto 2 seperate gameobjs audio sources, one for other actions and one for fitness.
            otherActsAudio.volume = 1;
            fitnessAudio.volume = 0;
        }
        if () /*ASN.excercising == false // needs an instance on the actions script where the hamster is exercising to be true
        {
            otherActsAudio.volume = 0;
            fitnessAudio.volume = 1;
        }
        */
    }
}
