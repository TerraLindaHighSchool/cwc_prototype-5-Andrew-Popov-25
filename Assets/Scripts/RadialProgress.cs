
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Rendering;

public class RadialProgress : MonoBehaviour
{
    public Image loadingBar;
    public float speed;
    public bool fadeCanvas;
    private GameObject powerupIndicator;
    private GameManager gameManager;
    private bool active;
    public float currentValue = 100f;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.fillAmount = 0f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (gameManager.powerupActive)
        {
            currentValue += -1f * Time.deltaTime * speed;
            loadingBar.fillAmount = currentValue / 100;
            if (loadingBar.fillAmount <= 0)
                gameManager.powerupActive = false;
        } else
        {
            currentValue = 0f;
            loadingBar.fillAmount = 0f;
        }
    }

    /*public IEnumerator PowerupCountdown()
    {
        active = true;
        loadingBar.fillAmount = 1f;

        float actualValue = 0f; // the goal
        float startValue = 1f; // animation start value
        float displayValue = 1f; // value during animation
        float timer = 0f;

        while (gameManager.powerupActive)
        {

            timer += Time.deltaTime;
            startValue = Mathf.Lerp(startValue, actualValue, timer);
            loadingBar.fillAmount = startValue;
            if (loadingBar.fillAmount <= 0.01)
            {
                gameManager.powerupActive = false;
                loadingBar.fillAmount = 0f;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }*/
}
