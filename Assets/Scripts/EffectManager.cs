using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public static EffectManager Instance;

    public ParticleSystem selectEffect;

    // Use this for initialization
    void Start () {

        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Explosion(Vector3 position)
    {
        EffectInstantiate(selectEffect, position);
    }

    private ParticleSystem EffectInstantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        return newParticleSystem;
    }
}
