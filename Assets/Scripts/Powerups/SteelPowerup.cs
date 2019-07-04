using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelPowerup : Powerup
{
    public override void PreCollidedWithObstacle(ObstacleBall obstacle)
    {
        Debug.Log("Steel disabling collision");
        obstacle.currentHits = 0;
        obstacle.GetComponent<Collider2D>().enabled = false;
    }

    public override void OnPowerupAquired()
    {
        
    }

    public override void OnPowerupDestroyed()
    {
        
    }
}
