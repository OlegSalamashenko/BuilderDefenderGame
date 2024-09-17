using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
    private GameObject spriteGameObject;

    private ResourceNearByOverlay resourceNearByOverlay;

    private void Awake() {
        spriteGameObject = transform.Find("Sprite").gameObject;
        resourceNearByOverlay = transform.Find("ResourceNearByOverlay").GetComponent<ResourceNearByOverlay>();
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        if (e.activeBuildingType == null) {
            Hide();
            resourceNearByOverlay.Hide();
        } else {
            Show(e.activeBuildingType.sprite);
            resourceNearByOverlay.Show(e.activeBuildingType.resourceGeneratorData);
        }
    }

    private void Update() {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }
    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide() {
        spriteGameObject.SetActive(false);
    }

}
