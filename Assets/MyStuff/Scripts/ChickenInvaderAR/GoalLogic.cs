using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLogic : MonoBehaviour
{
    private ChickenInvaderManager man;


    // This method is called when the object enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        InvaderLogic invaderLogic = other.GetComponentInParent<InvaderLogic>();

        if (invaderLogic != null)
        {
            man.LoseGame();
        }
    }

    public void SetManager(ChickenInvaderManager manager)
    {
        man = manager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
