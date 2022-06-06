using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected GameManager gameManager;
    protected Rigidbody targetRb;
    protected float minSpeed = 12;
    protected float maxSpeed = 16;
    protected float maxTorque = 10;
    protected float xRange = 4;
    protected float ySpawnPos = -6;
    public int pointValue = 1;
    public bool crate = false;
    public bool powerup = false;
    public bool crateObject = false;
    public ParticleSystem explosionParticle;
    private RadialProgress radialProgress;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        if (!crateObject)
            targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        else 
            targetRb.AddForce(RandomSmallForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        if(!crateObject)
            transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    protected Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    Vector3 RandomSmallForce()
    {
        return Vector3.up * Random.Range(minSpeed / 2, maxSpeed / 3);
    }

    protected float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    protected Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            if (powerup)
            {
                gameManager.powerupActive = true;
                radialProgress = GameObject.Find("Canvas/Indicator").GetComponent<RadialProgress>();
                radialProgress.currentValue = 100f;
            }

            if (crate)
                SpawnTargetInCrate();

            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            GameManager.targetsClicked++;
        }
        if (gameObject.CompareTag("Bad")) {
            if(!gameManager.powerupActive) {
                gameManager.gameOver();
                return;
            }

            gameManager.powerupActive = false;
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good"))
            if (other.CompareTag("Sensor"))
                if (gameManager.difficulty > 1)
                    gameManager.gameOver();
    }

    private void SpawnTargetInCrate()
    {
        GameObject anchor = GameObject.Find("Anchor");
        int index = Random.Range(0, gameManager.targets.Count);
        if (Random.Range(0, 3) == 4)
            index = gameManager.targets.Count - 1;
        //Instantiate(gameManager.targets[index], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        GameObject inside = Instantiate(gameManager.targets[index], transform.position, Quaternion.identity, transform.parent);
        Target targetScript = inside.GetComponent<Target>();
        targetScript.crateObject = true;
        inside.transform.position = transform.position;
        inside.transform.forward = transform.forward;
    }
}
