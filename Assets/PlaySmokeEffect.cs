using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySmokeEffect : MonoBehaviour
{

    [SerializeField]private List<ParticleSystem> smokeParticles = new List<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticles()
    {
        foreach(ParticleSystem particle in smokeParticles)
        {
            particle.Play();

        }
    }
}
