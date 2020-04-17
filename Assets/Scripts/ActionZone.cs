using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : InteractionZone
{
    public override void Activate(Player player)
    {
        base.Activate(player);

        Deactivate();
    }
}
