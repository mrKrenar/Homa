using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoSingleton<FinishLine>
{
    public ParticleSystem[] particles;
    public void PlayWonParticles()
    {
        foreach (var item in particles)
        {
            item.Play();
        }
    }
}
