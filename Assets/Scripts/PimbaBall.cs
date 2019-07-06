using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PimbaBall : MonoBehaviour
{
    Rigidbody2D body;
    public float maxLife;
    public float life;
    public CoinEffect coinEffect;
    public GameManager manager;

    [HideInInspector]
    public ArrayList powerups;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        maxLife = 100;
        life = maxLife;
        powerups = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPowerups()
    {
        powerups.Clear();
    }

    public void Damage(float damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Destroy(this);
            SceneManager.LoadScene(2);
        }
    }

    public void Impulse(Vector2 force)
    {
        body.AddForceAtPosition(force * 0.5f * GlobalVars.getPlayerProfile().GetImpulseUpgrade(), body.position, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            CoinEffect ef = Instantiate(coinEffect);
            ef.transform.position = gameObject.transform.position;
            GlobalVars.getPlayerProfile().coins += 1;
            manager.AddScore();
        }

    }

    public void TriggerOnObstacle(ObstacleBall obstacle)
    {
        foreach (Powerup powerup in powerups)
            powerup.PreCollidedWithObstacle(obstacle);
    }
}
