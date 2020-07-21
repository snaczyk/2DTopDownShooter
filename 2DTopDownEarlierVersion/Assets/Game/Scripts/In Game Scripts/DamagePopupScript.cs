using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;

public sealed class DamagePopupScript : MonoBehaviour
{
    //Create DamagePopup
    public static DamagePopupScript Create(Vector3 position, int damageAmount, bool isCriticalHit)
    { 
        Transform damagePopupTransform = Instantiate(InGameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopupScript damagePopup = damagePopupTransform.GetComponent<DamagePopupScript>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());

        if (!isCriticalHit)
        {
            textMesh.fontSize = 36;
            textColor = UtilsClass.GetColorFromString("FFC31C");
        }
        else
        {
            textMesh.fontSize = 45;
            textColor = UtilsClass.GetColorFromString("FF2B00");
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f, 1) * 60f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        //Creating a bunch of effects
        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            //Firs half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;

        if (disappearTimer <= 0)
        {
            //Start disappearing and destroying itself
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
