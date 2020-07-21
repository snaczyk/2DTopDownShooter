using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class InGameAssets : MonoBehaviour
{
    private static InGameAssets _i;

    public static InGameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<InGameAssets>("InGameAssets"));
            return _i;
        }
    }

    public Transform pfDamagePopup;



}
