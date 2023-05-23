using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysRotate : MonoBehaviour
{
    public int degreesPerSecond = 200;
    public bool rotate = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            this.transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime); //rotates 50 degrees per second around z axis
        }
    }
}
