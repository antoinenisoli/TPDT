using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Palette : Item
{
    public override void Effect()
    {
        player.MentalState = 0;
        base.Effect();
    }
}
