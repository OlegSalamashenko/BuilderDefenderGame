using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance {  get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs{ 
        public BuildingTypeSO activeBuildingType;
    }

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
            if (activeBuildingType != null && CanSpawnBiulding(activeBuildingType,UtilsClass.GetMouseWorldPosition())) {
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            }
            Debug.Log("CanSpawnBiulding : " + CanSpawnBiulding(buildingTypeList.list[0],UtilsClass.GetMouseWorldPosition()));
        }
    }
    
    public void SetActiveBuildingType(BuildingTypeSO buildingType) { 
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActiveBuildingTypeChangedEventArgs {activeBuildingType = activeBuildingType}
        );
    }
    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

    private bool CanSpawnBiulding(BuildingTypeSO buildingType, Vector3 position ) {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset,boxCollider2D.size,0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear) { return false; }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            //Collider inside the constructions radius  
            BuildingTypeHolder buildingTypeHolder =  collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                //Has a BuildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingType) {
                    //Theres already a building of this type whith in the constructions radius!
                    return false;
                }
            }
        }
        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            //Collider inside the constructions radius  
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                //It is building
                return true;
            }
        }

        return false ;
    }
}
