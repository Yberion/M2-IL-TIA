using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_DisplayInfos : MonoBehaviour
{
    public GameObject canvasDisplayInfos;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleDisplayInfo(bool display)
    {
        canvasDisplayInfos.SetActive(display);
    }
}
