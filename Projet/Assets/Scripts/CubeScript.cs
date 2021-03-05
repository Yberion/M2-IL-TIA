using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{
    public static int nbCube;
    public AudioSource audioSource;
    public bool cubeSelected = false;
    public GameObject canvasMenu;
    public GameObject canvasInfos;
    public GameObject reference;
    private Outline outlineComponent;
    private Outline[] outlineOfAllCubes;
    private CubeScript[] cubeScriptOfAllCubes;
    private float movementOffset = 0.004f;
    private float rotationOffset = 10.0f;
    private float placementOffset = 0.006f;
    public Text pieceRestantVal;

    // Start is called before the first frame update
    void Start()
    {
        outlineComponent = GetComponent<Outline>();
        outlineOfAllCubes = transform.parent.gameObject.GetComponentsInChildren<Outline>();
        cubeScriptOfAllCubes = transform.parent.gameObject.GetComponentsInChildren<CubeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (cubeSelected)
        {
            outlineComponent.enabled = false;
            cubeSelected = false;
            canvasMenu.SetActive(false);
        }
        else
        {
            foreach (Outline outline in outlineOfAllCubes)
            {
                outline.enabled = false;
            }

            foreach (CubeScript cubeScript in cubeScriptOfAllCubes)
            {
                cubeScript.cubeSelected = false;
            }

            outlineComponent.enabled = true;
            cubeSelected = true;
            canvasMenu.SetActive(true);
            transform.parent.GetComponent<CubeBehavior>().selectedCube = gameObject;
        }
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-movementOffset, 0.0f, 0.0f);
        CheckCorrectPlacement();
    }

    public void MoveRight()
    {
        transform.position += new Vector3(movementOffset, 0.0f, 0.0f);
        CheckCorrectPlacement();
    }

    public void MoveForward()
    {
        transform.position += new Vector3(0.0f, 0.0f, movementOffset);
        CheckCorrectPlacement();
    }

    public void MoveBackward()
    {
        transform.position += new Vector3(0.0f, 0.0f, -movementOffset);
        CheckCorrectPlacement();
    }

    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0.0f, -rotationOffset, 0.0f));
        CheckCorrectPlacement();
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0.0f, rotationOffset, 0.0f));
        CheckCorrectPlacement();
    }

    private void CheckCorrectPlacement()
    {
        //Debug.Log("----- ANGLE     : " + gameObject.transform.rotation.eulerAngles.y);
        //Debug.Log("----- REFERENCE : " + reference.transform.rotation.eulerAngles.y);
        //Debug.Log("----- ABS       : " + Mathf.Abs((gameObject.transform.rotation.eulerAngles.y % 90) - (reference.transform.rotation.eulerAngles.y % 90)));
        //Debug.Log("----- Reference : X: " + reference.transform.localPosition.x + ", Z: " + reference.transform.localPosition.z);
        //Debug.Log("----- Current   : X: " + gameObject.transform.localPosition.x + ", Z: " + gameObject.transform.localPosition.z);
        if ((gameObject.transform.position.x > (reference.transform.position.x - placementOffset) && gameObject.transform.position.x < (reference.transform.position.x + placementOffset)) &&
            (gameObject.transform.position.z > (reference.transform.position.z - placementOffset) && gameObject.transform.position.z < (reference.transform.position.z + placementOffset)) &&
            ((Mathf.Abs(gameObject.transform.rotation.eulerAngles.y % 90 - reference.transform.rotation.eulerAngles.y % 90) < 2) || (Mathf.Abs(gameObject.transform.rotation.eulerAngles.y % 90 - reference.transform.rotation.eulerAngles.y % 90) > 88)))
        {
            //Debug.Log("----- Reference : X: " + reference.transform.localPosition.x + ", Z: " + reference.transform.localPosition.z);
            //Debug.Log("----- Current   : X: " + gameObject.transform.localPosition.x + ", Z: " + gameObject.transform.localPosition.z);

            audioSource.PlayOneShot((AudioClip)Resources.Load("piece_valide"));
            gameObject.SetActive(false);
            outlineComponent.enabled = false;
            cubeSelected = false;
            canvasMenu.SetActive(false);
            MeshRenderer meshRenderer = reference.GetComponent<MeshRenderer>();
            Color32 col = meshRenderer.material.GetColor("_Color");
            col.a = 255;
            meshRenderer.material.SetColor("_Color", col);
            nbCube--;

            pieceRestantVal.text = nbCube.ToString();

            if (nbCube == 0)
            {
                audioSource.PlayOneShot((AudioClip)Resources.Load("puzzle_fin"));
                canvasInfos.SetActive(true);
                SpawnRandomCube.puzzleDone = true;
            }
        }
    }
}
