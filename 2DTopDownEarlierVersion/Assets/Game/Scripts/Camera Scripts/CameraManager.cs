using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey;

public class CameraManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
    //public Transform character1Transform;
    //public Transform character2Transform;
    public Transform manualMovementTransform;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);

        CMDebug.ButtonUI(new Vector2(570, 330), "Player", () =>
        {
            cameraFollow.SetGetCameraFollowPositionFunc(() => playerTransform.position);
        });
        //CMDebug.ButtonUI(new Vector2(570, 330), "Character1", () =>
        //{
        //    cameraFollow.SetGetCameraFollowPositionFunc(() => character1Transform.position);
        //}); CMDebug.ButtonUI(new Vector2(570, 330), "Character2", () =>
        //{
        //    cameraFollow.SetGetCameraFollowPositionFunc(() => character2Transform.position);
        //});
        CMDebug.ButtonUI(new Vector2(570, 270), "Manual", () =>
        {
            cameraFollow.SetGetCameraFollowPositionFunc(() => manualMovementTransform.position);
        });
    } 
}
