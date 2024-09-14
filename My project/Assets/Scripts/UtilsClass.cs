using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass 
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
        Vector3 mouceWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouceWorldPosition.z = 0;

        return mouceWorldPosition;
    }

}
