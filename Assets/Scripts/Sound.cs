using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource[] destroyNoise;
    public void PlayDestroyNoise()
    {
        int clip = Random.Range(0, destroyNoise.Length);
        destroyNoise[clip].Play();
    }
    
}
