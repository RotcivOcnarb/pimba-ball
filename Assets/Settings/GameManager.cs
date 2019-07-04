using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PimbaBall pimbaBall;
    public ObstacleBall obstaclePrefab;
    public Powerup[] powerups;
    int level = 1;

    //Manager
    public ButtonFastForward button;
    bool obstacleSpawned = true;
    public int score;

    GameObject top;
    GameObject bottom;
    GameObject left;
    GameObject right;
    float wallThickness = 5;
    Rigidbody2D body;

    void Awake()
    {
        top = new GameObject("Top");
        bottom = new GameObject("Bottom");
        left = new GameObject("Left");
        right = new GameObject("Right");
    }

    void CreateScreenColliders()
    {
        body = pimbaBall.GetComponent<Rigidbody2D>();
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        //// Create top collider
        BoxCollider2D collider = top.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x) + wallThickness * 2, wallThickness, 0f);
        collider.offset = new Vector2(collider.size.x / 2f - wallThickness, collider.size.y / 2f);
        collider.sharedMaterial = body.sharedMaterial;

        top.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, topRightScreenPoint.y, 0f);


        // Create bottom collider
        collider = bottom.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x) + wallThickness * 2, wallThickness, 0f);
        collider.offset = new Vector2(collider.size.x / 2f - wallThickness, collider.size.y / 2f);
        collider.sharedMaterial = body.sharedMaterial;

        //** Bottom needs to account for collider size
        bottom.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - collider.size.y, 0f);


        // Create left collider
        collider = left.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(wallThickness, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y) + wallThickness * 2, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f - wallThickness);
        collider.sharedMaterial = body.sharedMaterial;

        //** Left needs to account for collider size
        left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);


        // Create right collider
        collider = right.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(wallThickness, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y) + wallThickness * 2, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f - wallThickness);
        collider.sharedMaterial = body.sharedMaterial;

        right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);


    }

    public void TouchEnded(Vector2 force)
    {
        obstacleSpawned = false;
        button.EnableButton();
        if (body.velocity.magnitude < 0.1)
            pimbaBall.Impulse(force);
    }

    void Start()
    {
        level = 1;
        score = 0;
        CreateScreenColliders();
    }

    public void AddScore()
    {
        score++;
    }

    // Update is called once per frame
    void Update()
    {
        // The ball has finished moving, next round
        if (body.velocity.magnitude < 0.1 && obstacleSpawned == false)
        {
            obstacleSpawned = true;
            SpawnEnemies();
            Time.timeScale = 1;
            button.DisableButton();
            pimbaBall.ResetPowerups();
            ObstacleBall[] balls = FindObjectsOfType<ObstacleBall>();

            foreach (ObstacleBall ball in balls)
            {
                ball.Persist();
            }

        }
    }
    
    public void SpawnEnemies()
    {
        int quantity = Mathf.Max(Random.Range(level/3 - 1, level/3 + 2), 1);

        for(int i = 0; i < quantity; i++)
        {
            int hits = Mathf.Max(Random.Range(level/3 - 1, level/3 + 2), 1);

            Vector3 sMin = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 sMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

            ObstacleBall instanced = Instantiate(obstaclePrefab, new Vector3(
                    Random.Range(sMin.x, sMax.x),
                    Random.Range(sMin.y, sMax.y),
                    0
                ), new Quaternion());

            instanced.transform.localScale = new Vector3(0, 0, 0);
            instanced.hits = hits;
            instanced.color = Random.ColorHSV();
            instanced.color.a = 1;
            instanced.obstacleSize = Random.Range(0.4f, 0.7f);
            instanced.pimbaBall = pimbaBall;
        }

        if(Random.Range(0, 100) < 10)
        {
            Vector3 sMin = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 sMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

            Powerup p = powerups[(int)Random.Range(0, powerups.Length)];
            Powerup powerup = Instantiate(p, new Vector3(
                    Random.Range(sMin.x, sMax.x),
                    Random.Range(sMin.y, sMax.y),
                    0
                ), new Quaternion());
            powerup.pimbaBall = pimbaBall;
        }

        level++;
    }
}
