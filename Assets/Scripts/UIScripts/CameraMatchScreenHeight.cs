using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMatchScreenHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        camera.orthographicSize = 2f + 0.6f*(camera.orthographicSize / (1920f / Screen.height));
    }


    
}
