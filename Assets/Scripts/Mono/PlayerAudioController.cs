using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
   
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip deadSfxClip;

    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerControllerSystem>().playerActionState == PlayerControllerSystem.PlayerState.Dead && GetComponent<PlayerControllerSystem>().playerState == PlayerControllerSystem.PlayerState.Dead)
        {
            if(!isPlaying)
            {
                GetComponent<AudioSource>().PlayOneShot(deadSfxClip);
                isPlaying = true;
            }
        }
    }
}
