using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance {  get; private set; }

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;


    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update(){
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null) {
                Instantiate(activeBuildingType.prefab, GetMousePos(), Quaternion.identity);
            }
        }
    }
    private Vector3 GetMousePos()
    { 
        Vector3 mouceWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);    
        mouceWorldPosition.z = 0;

        return mouceWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) { 
        activeBuildingType = buildingType;
    }
    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }
}
