﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public PimbaBall pimbaBall;
    public ObstacleBall obstaclePrefab;
    public Powerup[] powerups;
    public RectTransform playArea;
    public GameObject pauseMenu;
    //Manager
    public ButtonFastForward button;
    bool obstacleSpawned = true;
    public int score;
    public int level = 0;

    Rigidbody2D body;

    public void TouchEnded(Vector2 force)
    {
        obstacleSpawned = false;
        button.EnableButton();
        if (body.velocity.magnitude < 0.1)
            pimbaBall.Impulse(force);
    }

    void Start()
    {
        score = 0;
        body = pimbaBall.GetComponent<Rigidbody2D>();
         SpawnEnemies();
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
            pimbaBall.ResetSpeed();
            ObstacleBall[] balls = FindObjectsOfType<ObstacleBall>();

            foreach (ObstacleBall ball in balls)
                ball.Persist();

        }
    }
    float lastTimeScale = 0;
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = lastTimeScale;
    }

    public void QuitGame()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void SpawnEnemies()
    {
        int lv = level + (GlobalVars.getPlayerProfile().stage-1)*10;
        int quantity = Mathf.Max(Random.Range(lv/3 - 1, lv/3 + 2), 1);

        for(int i = 0; i < quantity; i++)
        {
            int hits = Mathf.Max(Random.Range(lv/3 - 1, lv/3 + 2), 1);

            Vector3[] corners = new Vector3[4];
            playArea.GetWorldCorners(corners);
            Vector3 sMin = corners[0];
            Vector3 sMax = corners[2];

            ObstacleBall instanced = Instantiate(obstaclePrefab, new Vector3(
                    Random.Range(sMin.x, sMax.x),
                    Random.Range(sMin.y, sMax.y),
                    0
                ), new Quaternion());

            instanced.manager = this;
            instanced.coinEffect = pimbaBall.coinEffect.gameObject;
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

        if(level >= 10){
            level = 0;
            GlobalVars.getPlayerProfile().stage++;
        }
    }
}
