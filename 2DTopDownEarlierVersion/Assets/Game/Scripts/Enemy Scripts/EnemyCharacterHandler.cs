using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyCharacterHandler : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShootEnemy;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 GunEndPosition;
        public Vector3 ShootPosition;
    }

    [SerializeField] GameObject muzzleFlash;
    [SerializeField] int framesToFlash = 1;
    [SerializeField] Transform aimGunEndPositionTransform;
    [SerializeField] private Transform playerPosition;
    [Range(10, 50)] [SerializeField] private float shootingRange = 20f;
    [SerializeField] float fireRate;

    public Vector3 ActualEnemyPosition;

    bool isEffect = false;
    private Rigidbody2D enemyRb;
    private float nextEnemyShootTime;
    public void Start()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(false);
        }

        enemyRb = GetComponent<Rigidbody2D>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void Update()
    {
        HandleShooting();

        ActualEnemyPosition.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
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

    public void HandleShooting()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);

        if (distance < shootingRange) 
        {
            if (Time.time > nextEnemyShootTime)
            {
                if (muzzleFlash != null && !isEffect)
                {
                    StartCoroutine(Effect());
                }
                nextEnemyShootTime = Time.time + fireRate;
            }

            OnShootEnemy?.Invoke(this, new OnShootEventArgs
            {

                GunEndPosition = aimGunEndPositionTransform.position,
                ShootPosition = playerPosition.position,
            });
        }
    }

}