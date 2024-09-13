using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;

    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private Transform arrowBtn;
    private void Awake() {
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = +130f;
        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index + 66, 66);

        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform btnTransform = Instantiate(btnTemplate,transform);
            btnTransform.gameObject.SetActive(true);

            offsetAmount = +130f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index + 66 , 66);

            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            btnTransformDictionary[buildingType] = btnTransform;

            index++;
        }
    }
    private void Update() {
        UpdateActiveBiuldingTypeButton();
    }
    private void UpdateActiveBiuldingTypeButton() {
        arrowBtn.Find("Select").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys) {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("Select").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType =  BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null) {
            arrowBtn.Find("Select").gameObject.SetActive(true);
        } else {
            btnTransformDictionary[activeBuildingType].Find("Select").gameObject.SetActive(true);
        }
       
    }
}
