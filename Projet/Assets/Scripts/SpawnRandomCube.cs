using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnRandomCube : MonoBehaviour
{
    public GameObject imageTargetOxygen;
    public GameObject imageTargetOxygenElements;
    public GameObject SpawnedCube;
    public GameObject references;
    public Text tempsVal;
    public Text pieceRestantVal;
    private AudioSource audioSource;
    public bool firstTime = true;
    public int maxCube = 4;
    bool bImageTargetOxygen = false;
    private GameObject[] cubes;
    public GameObject canvasMenu;
    public GameObject canvasInfos;
    public static bool puzzleDone = false;
    public static bool puzzleStarted = false;
    float stopWatchTime;
    float stopWatchTimeSeconds;
    float stopWatchTimeMinutes;
    float stopWatchTimeHours;

    // Start is called before the first frame update
    void Start()
    {
        cubes = new GameObject[maxCube];
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bImageTargetOxygen)
        {
            if (firstTime)
            {
                audioSource.Play();
                Color[] colors = { Color.blue, Color.yellow, Color.green, Color.red };
                // Pas opti
                GameObject[] references = { GameObject.Find("Cube1"), GameObject.Find("Cube2"), GameObject.Find("Cube3"), GameObject.Find("Cube4") };

                CubeScript.nbCube = maxCube;

                for (int i = 0; i < maxCube; ++ i)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    CubeScript cubeScript = cube.AddComponent<CubeScript>();
                    cubeScript.reference = references[i];
                    cubeScript.canvasMenu = canvasMenu;
                    cubeScript.canvasInfos = canvasInfos;
                    cubeScript.audioSource = audioSource;
                    cubeScript.pieceRestantVal = pieceRestantVal;

                    Outline outline = cube.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineWidth = 6.0f;
                    outline.enabled = false;

                    cube.transform.SetParent(SpawnedCube.transform);

                    cube.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    // -0.04 > X > 0.04, -0.04 > Z > 0.04
                    // -0.1 < X < 0.1, -0.8 < Z < 0.08

                    float pos_x = Random.Range(-0.1f, 0.1f);
                    float pos_z = Random.Range(-0.08f, 0.08f);
                    float rotation = Random.Range(0, 8) * 10.0f;

                    cube.transform.position = imageTargetOxygen.transform.position + new Vector3(pos_x, 0.03f, pos_z);
                    cube.transform.Rotate(new Vector3(0.0f, rotation, 0.0f));
                    cube.GetComponent<Renderer>().materials[0].color = colors[i];

                    cubes[i] = cube;
                }

                firstTime = false;
                puzzleStarted = true;
                pieceRestantVal.text = maxCube.ToString();
            }
        }
    }

    private void FixedUpdate()
    {
        if (puzzleStarted && !puzzleDone)
        {
            updateStopWatchTime();
        }
    }

    void updateStopWatchTime()
    {
        stopWatchTime += Time.deltaTime;

        stopWatchTimeSeconds = (int)(stopWatchTime % 60);
        stopWatchTimeMinutes = (int)((stopWatchTime / 60) % 60);
        stopWatchTimeHours = (int)(stopWatchTime / 3600);

        tempsVal.text = stopWatchTimeHours.ToString("00") + ":" + stopWatchTimeMinutes.ToString("00") + ":" + stopWatchTimeSeconds.ToString("00");
    }

    public void imageTargetOxygenFound()
    {
        bImageTargetOxygen = true;
    }

    public void imageTargetOxygenLost()
    {
        bImageTargetOxygen = false;
    }

    public void ResetCube()
    {
        if (!bImageTargetOxygen)
        {
            return;
        }

        if (firstTime)
        {
            return;
        }

        foreach (MeshRenderer meshRenderer in references.GetComponentsInChildren<MeshRenderer>())
        {
            Color32 col = meshRenderer.material.GetColor("_Color");
            col.a = 100;
            meshRenderer.material.SetColor("_Color", col);
        }

        foreach (GameObject gameObject in cubes)
        {
            float pos_x = Random.Range(-0.1f, 0.1f);
            float pos_z = Random.Range(-0.08f, 0.08f);
            float rotation = Random.Range(0, 8) * 10.0f;

            gameObject.transform.position = imageTargetOxygen.transform.position + new Vector3(pos_x, 0.03f, pos_z);
            gameObject.transform.Rotate(new Vector3(0.0f, rotation, 0.0f));

            gameObject.SetActive(true);
        }

        CubeScript.nbCube = maxCube;
        SpawnRandomCube.puzzleDone = false;
        canvasInfos.SetActive(false);
        stopWatchTime = 0.0f;
        pieceRestantVal.text = maxCube.ToString();
    }
}
