using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PimbaBall pimbaBall;
    public ObstacleBall obstaclePrefab;
    public RectTransform playArea;
    public GameObject pauseMenu;
    public GameObject winMenu;
    //Manager
    public ButtonFastForward button;
    bool obstacleSpawned = true;
    public int score;
    public int level = 0;
    Rigidbody2D body;

    //Interface
    public FillBar levelBar;
    public ProgressBar healthBar;
    public Text scoreText;
    public Text stageText;

    public void TouchEnded(Vector2 force)
    {
        obstacleSpawned = false;
        button.EnableButton();
        if (body.velocity.magnitude < 0.1)
            pimbaBall.Impulse(force);
    }

    void Start()
    {
        Time.timeScale = 1;
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
        levelBar.percentage += ((level / 10f) * 100 - levelBar.percentage)/5f;
        healthBar.maxValue = GlobalVars.getPlayerProfile().GetPlayerHealth();
        healthBar.value = (int)pimbaBall.life;
        scoreText.text = "" + score;
        stageText.text = "Stage " + GlobalVars.getPlayerProfile().stage;


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
        pauseMenu.GetComponentInChildren<DropToCenterY>().transform.position = new Vector3(0, 6.3f, 0);
        pauseMenu.SetActive(true);
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.GetComponentInChildren<DropToCenterY>().transform.position = new Vector3(0, 6.3f, 0);
        pauseMenu.SetActive(false);
        Time.timeScale = lastTimeScale;
    }

    public void QuitGame()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ShowGameOverScreen(){
        winMenu.GetComponentInChildren<DropToCenterY>().transform.position = new Vector3(0, 7f, 0);
        winMenu.GetComponentInChildren<WinWindowScreen>().ShowScores();
        winMenu.SetActive(true);
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;

        
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

            Vector3 obsPos = new Vector3(
                    Random.Range(sMin.x, sMax.x),
                    Random.Range(sMin.y, sMax.y),
                    0
                );

            while((obsPos - pimbaBall.transform.position).magnitude < 1.5){
                obsPos = new Vector3(
                    Random.Range(sMin.x, sMax.x),
                    Random.Range(sMin.y, sMax.y),
                    0
                );
            }

            ObstacleBall instanced = Instantiate(obstaclePrefab, obsPos, new Quaternion());

            instanced.manager = this;
            instanced.coinEffect = pimbaBall.coinEffect.gameObject;
            instanced.transform.localScale = new Vector3(0, 0, 0);
            instanced.hits = hits;
            instanced.color = Random.ColorHSV();
            instanced.color.a = 1;
            instanced.obstacleSize = Random.Range(0.4f, 0.7f);
            instanced.pimbaBall = pimbaBall;
        }

        level++;

        if(level >= 10){
            level = 0;
            GlobalVars.getPlayerProfile().stage++;
        }
    }
}
