using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource backg;
    void Start()
    {
        backg = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        backg.loop = true;
        backg.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
