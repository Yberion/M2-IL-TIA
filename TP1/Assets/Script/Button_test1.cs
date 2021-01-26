using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Button_test1 : MonoBehaviour
{

    public GameObject objets;

    protected VirtualButtonBehaviour[] virtualButtonBehaviours;

    private int cycliqueObjectNumberRenderer = 0;
    private int cycliqueObjectNumberCollider = 0;
    private int cycliqueObjectNumberCanvas = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            virtualButtonBehaviours[i].RegisterOnButtonPressed(OnButtonPressed);
            virtualButtonBehaviours[i].RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        var rendererComponents = objets.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = objets.GetComponentsInChildren<Collider>(true);
        var canvasComponents = objets.GetComponentsInChildren<Canvas>(true);

        foreach (var component in rendererComponents)
            component.enabled = false;

        foreach (var component in colliderComponents)
            component.enabled = false;

        foreach (var component in canvasComponents)
            component.enabled = false;

        if (rendererComponents.Length > 0)
        {
            rendererComponents[cycliqueObjectNumberRenderer].enabled = true;
            cycliqueObjectNumberRenderer = (cycliqueObjectNumberRenderer + 1) % rendererComponents.Length;
        }

        if (colliderComponents.Length > 0)
        {
            colliderComponents[cycliqueObjectNumberCollider].enabled = true;
            cycliqueObjectNumberCollider = (cycliqueObjectNumberRenderer + 1) % colliderComponents.Length;
        }

        if (canvasComponents.Length > 0)
        {
            canvasComponents[cycliqueObjectNumberCanvas].enabled = true;
            cycliqueObjectNumberCanvas = (cycliqueObjectNumberRenderer + 1) % canvasComponents.Length;
        }

        Debug.Log("OnButtonPressed::" + vb.VirtualButtonName);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased::" + vb.VirtualButtonName);
    }

    void Destroy()
    {
        // Unregister with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            virtualButtonBehaviours[i].UnregisterOnButtonPressed(OnButtonPressed);
            virtualButtonBehaviours[i].UnregisterOnButtonReleased(OnButtonReleased);
        }
    }
}
