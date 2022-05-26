using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public ListList list = new ListList();
    public List<Vector3> force;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton; 
    public bool isGameActive;
    public GameObject titleScreen;
    public int difficulty;
    public static int targetsClicked;
    private float spawnRate = 5f;
    private int score;

    // Update is called once per frame
    IEnumerator SpawnTarget() { 
        while(isGameActive) {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            IncreaseDifficulty();
        }
    }

    public void UpdateScore(int scoreToAdd) {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void StartGame(int difficulty)
    {
        this.difficulty = difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    public void IncreaseDifficulty()
    {
        //https://www.desmos.com/calculator/1md3lan2e9
        if (spawnRate > 1) {
            if (targetsClicked < Mathf.PI * 6) {
                spawnRate = -Mathf.Sin(0.08f * targetsClicked) * 4f + 5f;
}
        } else {
            spawnRate = 1f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
