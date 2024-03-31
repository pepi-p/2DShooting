using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public void DestroyParticle()
    {
        Destroy(this.gameObject);
    }
}
