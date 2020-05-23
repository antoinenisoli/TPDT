using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Nostalgia : Item
{
    public float delay = 0.2f;

    public override void Effect()
    {
        float i = delay;

        foreach (RaycastHit hit in player.detectedThougts)
        {
            DT dark = hit.transform.GetComponent<DT>();
            i += delay;
            dark.StartCoroutine(dark.StepDestroy(i));
        }

        base.Effect();
    }
}
