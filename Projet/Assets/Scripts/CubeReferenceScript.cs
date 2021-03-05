using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeReferenceScript : MonoBehaviour
{
    public bool cubeSelected = false;
    public Text text;
    private Outline outlineComponent;
    private Outline[] outlineOfAllCubes;
    private CubeReferenceScript[] cubeReferenceScriptOfAllCubes;

    // Start is called before the first frame update
    void Start()
    {
        outlineComponent = GetComponent<Outline>();
        outlineOfAllCubes = transform.parent.gameObject.GetComponentsInChildren<Outline>();
        cubeReferenceScriptOfAllCubes = transform.parent.gameObject.GetComponentsInChildren<CubeReferenceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!SpawnRandomCube.puzzleDone)
        {
            return;
        }

        if (cubeSelected)
        {
            outlineComponent.enabled = false;
            cubeSelected = false;
            text.enabled = false;
        }
        else
        {
            foreach (Outline outline in outlineOfAllCubes)
            {
                outline.enabled = false;
            }

            foreach (CubeReferenceScript cubeReferenceScript in cubeReferenceScriptOfAllCubes)
            {
                cubeReferenceScript.cubeSelected = false;
                cubeReferenceScript.text.enabled = false;
            }

            outlineComponent.enabled = true;
            cubeSelected = true;
            text.enabled = true;
        }
    }
}
