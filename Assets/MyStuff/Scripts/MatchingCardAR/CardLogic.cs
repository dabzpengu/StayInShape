using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CardLogic : MonoBehaviour
{
    public string plantName = "DefaultCardName";
    public int id = 0;

    [SerializeField] TextMeshProUGUI cardTextFront;
    [SerializeField] TextMeshProUGUI cardTextBack;
    private Transform highlight;

    public void OnSelect()
    {
        if (highlight.gameObject.GetComponent<Outline>() != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = true;
        } else
        {
            Outline outline = highlight.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
            highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
        }
    }

    public void OnDeselect()
    {
        highlight.gameObject.GetComponent<Outline>().enabled = false;
    }

    private string presentName()
    {
        return plantName + "\nID" + id.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        highlight = this.transform;
        cardTextFront.text = presentName();
        cardTextBack.text = presentName();
    }
}
