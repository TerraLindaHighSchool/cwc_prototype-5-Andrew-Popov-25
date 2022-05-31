
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Rendering;

public class RadialProgress : MonoBehaviour
{
    public Image LoadingBar;
    float currentValue;
    float promptFadeCounter;
    public float speed;
    public bool fadeCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        
    }
}
