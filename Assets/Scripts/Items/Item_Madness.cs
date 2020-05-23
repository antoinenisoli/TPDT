using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Madness : Item
{
    public override void Effect()
    {
        if (CanUse())
        {
            player.ChangeSanity(1, false);
            base.Effect();
        }
    }
}
