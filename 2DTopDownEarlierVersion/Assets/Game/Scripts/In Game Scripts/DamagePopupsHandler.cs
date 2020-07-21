using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public sealed class DamagePopupsHandler : MonoBehaviour
{

    private void Start()
    {
        //DamagePopupScript.Create(Vector3.zero, 300);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isCristicalHit = Random.Range(0, 100) < 30;
           // DamagePopupScript.Create(UtilsClass.GetMouseWorldPosition(), 100, isCristicalHit);
        }
    }
}
