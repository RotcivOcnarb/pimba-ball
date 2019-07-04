using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public PimbaBall pimbaBall;
    public abstract void PreCollidedWithObstacle(ObstacleBall obstacle);
    public abstract void OnPowerupAquired();
    public abstract void OnPowerupDestroyed();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pimba")
        {
            pimbaBall.powerups.Add(this);
            Destroy(gameObject);
        }
    }
}
