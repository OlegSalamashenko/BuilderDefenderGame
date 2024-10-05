using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerMaxUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image constructionProgressImage;
    private void Awake()
    {
        constructionProgressImage = transform.Find("mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormilized();
    }
}
