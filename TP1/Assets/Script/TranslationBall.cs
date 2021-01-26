using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationBall : MonoBehaviour
{
    public GameObject oxygenImageTarget;
    public GameObject astronautImageTarget;

    private GameObject theBall;
    private bool bAstronautFound = false;
    private bool bOxygenFound = false;
    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bAstronautFound && bOxygenFound)
        {
            if (firstTime)
            {
                firstTime = false;

                theBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                theBall.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                theBall.transform.position = astronautImageTarget.transform.position + new Vector3(0.0f, 0.02f, 0.0f);
            }

            theBall.transform.position = Vector3.MoveTowards(theBall.transform.position, oxygenImageTarget.transform.position, 0.05f * Time.deltaTime);
        }
    }

    public void astronautFound()
    {
        bAstronautFound = true;
    }

    public void astronautLost()
    {
        bAstronautFound = false;
    }

    public void oxygenFound()
    {
        bOxygenFound = true;
    }

    public void oxygenLost()
    {
        bOxygenFound = false;
    }
}
