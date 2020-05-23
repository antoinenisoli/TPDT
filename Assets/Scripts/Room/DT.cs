using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT : MonoBehaviour
{
    public ParticleSystem fx;

    public IEnumerator StepDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        fx.Play();
        yield return new WaitForSeconds(fx.main.duration + fx.main.startLifetimeMultiplier);
        Destroy(gameObject);
    }
}
