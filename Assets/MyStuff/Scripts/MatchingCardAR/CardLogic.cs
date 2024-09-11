using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    public string plantName = "DefaultCardName";
    public int id = 0;
    public Color col = Color.white;

    [SerializeField] TextMeshProUGUI cardTextFront;
    [SerializeField] TextMeshProUGUI cardTextBack;
    private Transform highlight;

    public void SetCard(MatchingCardSO cardData)
    {
        this.plantName = cardData.name;
        this.id = cardData.id;
        cardTextFront.text = presentName();
        cardTextBack.text = presentName();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null && meshRenderer.material != null)
        {
            Debug.Log("Set");
            meshRenderer.material.color = cardData.col;
        } else
        {
            Debug.Log("Help");
        }
    }

    public void Select()
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

    public bool IsMatching(CardLogic otherCard)
    {
        return otherCard.id == id & otherCard != this;
    }

    public void Deselect()
    {
        highlight.gameObject.GetComponent<Outline>().enabled = false;
    }

    private string presentName()
    {
        return plantName;
    }

    // Start is called before the first frame update
    void Start()
    {
        highlight = this.transform;
        cardTextFront.text = presentName();
        cardTextBack.text = presentName();
    }
}
