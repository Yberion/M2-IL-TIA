using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public GameObject objectForRotation;
    private Renderer buttonRender;
    private Collider buttonCollider;
    private Color previousColor;

    private bool rotateLeft = false;
    private bool rotateRight = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonRender = GetComponent<Renderer>();
        buttonCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateLeft)
        {
            objectForRotation.transform.Rotate(Vector3.up, Time.deltaTime * 10.0f, Space.Self);
        }

        if (rotateRight)
        {
            objectForRotation.transform.Rotate(Vector3.up, Time.deltaTime * -10.0f, Space.Self);
        }
    }

    void OnMouseDown()
    {
        previousColor = buttonRender.materials[0].color;
        buttonRender.materials[0].color = Color.green;

        if (buttonCollider.gameObject.tag == "button_rotate_left")
        {
            print("START left");
            rotateLeft = true;
        }

        if (buttonCollider.gameObject.tag == "button_rotate_right")
        {
            print("START right");
            rotateRight = true;
        }
    }
    void OnMouseOver()
    {
    }
    void OnMouseUp()
    {
        buttonRender.materials[0].color = previousColor;

        if (buttonCollider.gameObject.tag == "button_rotate_left")
        {
            print("STOP left");
            rotateLeft = false;
        }

        if (buttonCollider.gameObject.tag == "button_rotate_right")
        {
            print("STOP right");
            rotateRight = false;
        }
    }
}
