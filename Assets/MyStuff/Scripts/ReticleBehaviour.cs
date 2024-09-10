using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReticleBehaviour : MonoBehaviour
{
    [SerializeField] GameObject image;

    private Transform currFocus;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int rayDistance = 100;
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Ray crosshairRay = Camera.main.ScreenPointToRay(screenCenter); //kiv this
        RaycastHit crosshairHit;
        if (Physics.Raycast(crosshairRay, out crosshairHit, rayDistance))
        {
            //spawn reticle (stretch goal)
            image.SetActive(true);
            transform.position = crosshairHit.point;
            currFocus = crosshairHit.transform;


        }
        transform.rotation = Camera.main.transform.rotation;
    }

    public Transform getTransform() 
    {
        return currFocus;
    }
}
