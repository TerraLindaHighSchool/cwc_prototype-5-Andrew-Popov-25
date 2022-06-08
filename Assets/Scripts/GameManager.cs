using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public ListList groupList = new ListList();
    public GameObject crate;
    public List<Vector3> force;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton; 
    public bool isGameActive;
    public GameObject titleScreen;
    public int difficulty;
    public static int targetsClicked;
    private float spawnRate = 5f;
    private int score = 0;
    private int targetMaxIndex = 4;
    public bool powerupActive;

    //Add score to current score
    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    //Makes the game end
    public void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    //Initialize GameManager
    public void StartGame(int difficulty)
    {
        this.difficulty = difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnManager());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    //Mathematical difficulty increase
    public void IncreaseDifficulty()
    {
        //https://www.desmos.com/calculator/1md3lan2e9
        if (spawnRate > 1.5) {
            if (targetsClicked < Mathf.PI * 6) {
                spawnRate = -Mathf.Sin(0.08f * targetsClicked) * 4f + 5f;
}
        } else {
            spawnRate = 1.5f;
        }
    }
    
    //Reloads the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    //Manages target spawns
    IEnumerator SpawnManager()
    {
        while (isGameActive)
        {
            if (Random.Range(0, 2) >= 1) {
                SpawnTarget();
            }
            else if (Random.Range(0, 2) >= 1) {
                SpawnTargetGroup();
                if (spawnRate < 4.5f) {
                    spawnRate = +-0.5f;
                }
            }
            else SpawnCrate();


            IncreaseDifficulty();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    //Spawns a crate
    public void SpawnCrate()
    {
        Instantiate(crate);
    }
    
    //Spawns a group of targets
    public void SpawnTargetGroup()
    {
        int index = Random.Range(0, groupList.listList.Count - 1);

        //Debug.Log("Index:" + index);
        //Runthrough list and get stuff and stuff
        foreach (GameObject gameObject in groupList.listList[index].list) {
            if(gameObject == null) {
                Instantiate(targets[Random.Range(0, targetMaxIndex)]);
                continue;
            }
            Instantiate(gameObject);
        }
    }
    
    //Spawn a single target
    public void SpawnTarget()
    {
        int index = Random.Range(0, targetMaxIndex);
        Instantiate(targets[index]);
        IncreaseDifficulty();
    }
}
