using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

     
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO buildingType;


    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMousePos(),Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            buildingType = buildingTypeList.list[1];
        }
    }
    private Vector3 GetMousePos()
    { 
        Vector3 mouceWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);    
        mouceWorldPosition.z = 0;

        return mouceWorldPosition;
    }
}
