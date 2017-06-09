using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    private int score;
    private bool gameOver;
    private bool restart;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        float waveSpeedFactor = 1.0f;
        while (true)
        {
            for (int i = 0; i < hazardCount * waveSpeedFactor; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                var asteroidGameObject = Instantiate(hazard, spawnPosition, spawnRotation);
                asteroidGameObject.GetComponent<Mover>().speed *= waveSpeedFactor;
                yield return new WaitForSeconds(spawnWait/waveSpeedFactor);
            }
            waveSpeedFactor += 1.0f;
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int score)
    {
        this.score += score;
        UpdateScore();
    }

    private void Update()
    {
        if (restart && Input.GetKeyUp(KeyCode.R)   )
        {
            Application.LoadLevel(Application.loadedLevel); 
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}