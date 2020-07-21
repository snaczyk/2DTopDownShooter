using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerCharacterHandler : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs: EventArgs
    {
        public Vector3 gunEndPosition;
        public Vector3 shootPosition;
    }



    public Vector3 ActualPlayerPos;

    [SerializeField] GameObject muzzleFlash;
    [SerializeField] int framesToFlash = 1;
    [SerializeField] Transform aimGunEndPositionTransform;
    [SerializeField] UI_Inventory uiInventory;

    bool isEffect = false;
    private Inventory inventory;


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Start()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetIventory(inventory);

        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("jestem w trigerze");
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
            //Touching Item

            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
    }

    private void UseItem(Item item)
    {

        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, Amount = 1 });
               // playerHealth.Heal(500);
                break;
            case Item.ItemType.ManaPotion:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, Amount = 1 });
                break;
        }
    }

    private void Update()
    {
        //if (GetComponent<DragDrop>().IsDraging == false)
      //  {
            HandleShooting();
        //}

        ActualPlayerPos.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    IEnumerator Effect()
    {
        muzzleFlash.SetActive(true);
        var framesFlashed = 0;
        isEffect = true;

        while (framesFlashed <= framesToFlash)
        {

            framesFlashed++;
            yield return null;

        }

        muzzleFlash.SetActive(false);
        isEffect = false;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (muzzleFlash != null && !isEffect)
            {
                StartCoroutine(Effect());
            }

            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            OnShoot?.Invoke(this, new OnShootEventArgs {

                gunEndPosition = aimGunEndPositionTransform.position,
                shootPosition = mousePosition,
            });
        }
    }

}
