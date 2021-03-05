using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    public GameObject selectedCube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveLeft()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.MoveLeft();
    }

    public void MoveRight()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.MoveRight();
    }

    public void MoveForward()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.MoveForward();
    }

    public void MoveBackward()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.MoveBackward();
    }

    public void RotateLeft()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.RotateLeft();
    }

    public void RotateRight()
    {
        CubeScript cubeScript = selectedCube.GetComponent<CubeScript>();

        cubeScript.RotateRight();
    }
}
