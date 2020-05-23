using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Memory : Item
{
    public override void Effect()
    {
        if (CanUse())
        {
            player.ChangeSanity(-2, true);
            base.Effect();
        }
    }

    public override bool CanUse()
    {
        if (player.MentalState > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
