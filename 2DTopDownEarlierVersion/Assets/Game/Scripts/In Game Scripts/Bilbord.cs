using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bilbord : MonoBehaviour
{
    [SerializeField] private Transform characterPosition;

    void LateUpdate()
    {
        Vector3 boundary = new Vector3(0f, 15f, 0);

        transform.position = characterPosition.position;
        transform.position += boundary;

        /*Vector3 rot = new Vector3(0f, 0f, 0f);
        transform.rotation.*/
    }
}
