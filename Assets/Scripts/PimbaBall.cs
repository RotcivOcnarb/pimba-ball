using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PimbaBall : MonoBehaviour
{
    Rigidbody2D body;
    public int life;
    public CoinEffect coinEffect;
    public GameManager manager;

    [HideInInspector]
    public ArrayList powerups;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        life = GlobalVars.getPlayerProfile().GetPlayerHealth();
        powerups = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        body.drag = GlobalVars.getPlayerProfile().GetLinearDampingUpgrade();
    }

    public void ResetSpeed(){
        body.velocity = new Vector2(0, 0);
    }

    public void ResetPowerups()
    {
        powerups.Clear();
    }

    public void Damage()
    {
        life --;
        if(life <= 0)
        {
            if(!dead){
                dead = true;
                manager.ShowGameOverScreen();
            }
        }
    }

    public void Impulse(Vector2 force)
    {
        body.AddForceAtPosition(force * 0.5f * GlobalVars.getPlayerProfile().GetImpulseUpgrade(), body.position, ForceMode2D.Impulse);
    }

    public void TriggerOnObstacle(ObstacleBall obstacle)
    {
        foreach (Powerup powerup in powerups)
            powerup.PreCollidedWithObstacle(obstacle);
    }
}
