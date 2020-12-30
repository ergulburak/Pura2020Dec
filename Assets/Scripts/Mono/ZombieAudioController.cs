using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ZombieSystem>().zombieState == ZombieSystem.ZombieState.Dead)
        {
            if (!Mathf.Approximately(_audioSource.volume, 0))
            {
                _audioSource.volume -= 1 * Time.deltaTime;
            }
        }
    }
}
