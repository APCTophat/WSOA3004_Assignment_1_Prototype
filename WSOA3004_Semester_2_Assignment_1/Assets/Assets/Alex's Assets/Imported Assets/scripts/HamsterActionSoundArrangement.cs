using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterActionSoundArrangement : MonoBehaviour
{
    public AudioSource drinkingAudio;
    public AudioSource eatingAudio;
    public AudioSource execrsiceAudio;
    public AudioSource playingAudio;
  
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
        if ()   // if statement to know when DRINKING
        {
            //audios have to be made onto 4 seperate gameobjs with audio sources
            drinkingAudio.volume = 1;
            eatingAudio.volume = 0;
            execrsiceAudio.volume = 0;
            playingAudio.volume = 0;
        }
        else
        {
            //ASN.drinking = false
        }
        if () // if statement to know when EATING
        {
            drinkingAudio.volume = 0;
            eatingAudio.volume = 1;
            execrsiceAudio.volume = 0;
            playingAudio.volume = 0;
        }
        else
        {
            //ASN.eating = false
        }
        if ()   // if statement to know when EXERCISING
        {
            drinkingAudio.volume = 0;
            eatingAudio.volume = 0;
            execrsiceAudio.volume = 1;
            playingAudio.volume = 0;
        }
        else
        {
            //ASN.excersice = false
        }
        if ()  Action Script name.playing = true // if statement to know when PLAYING
        {
            drinkingAudio.volume = 0;
            eatingAudio.volume = 0;
            execrsiceAudio.volume = 0;
            playingAudio.volume = 1;
        }
        else
        {
            //ASN.playing = false
        }
        */
    }
    
}
