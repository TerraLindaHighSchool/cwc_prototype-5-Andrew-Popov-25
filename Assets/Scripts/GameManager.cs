using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1f;
    private int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    IEnumerator SpawnTarget() { 
        while(true) {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd) {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
