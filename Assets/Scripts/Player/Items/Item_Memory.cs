using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Memory : Item
{
    public override void Effect()
    {
        if (player.MentalState > 0)
        {
            player.ChangeSanity(-2, true, Color.green);
            base.Effect();
        }
    }
}
