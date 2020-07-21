using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class GameHandlerScript : MonoBehaviour
{
    [SerializeField] private EnemyCharacterHandler enemyCharacterHandler;
    [SerializeField] private PlayerCharacterHandler playerCharacterHandler;
    [SerializeField] private Window_CharacterPortrait windowCharacterPortraitHandler;
    [SerializeField] private EnemyHealth enemyHealthHandler;
    [SerializeField] private EnemyAI enemyAIHandler;
    [SerializeField] private Material WeaponTracerMaterial;
    [SerializeField] private Sprite shootFlashSprite;
    [SerializeField] private Transform enemyDamagePopupPlace;
    [SerializeField] private Transform playerDamagePopupPlace;
    [SerializeField] private Transform pfEnemyDeadBody;
    [SerializeField] private Transform playerTransform;

    public GameObject enemy;
    public float NextEnemyShootTime;
    public float FireRate = .3f;
    public bool isFollowing = false;

    private Vector3 DmgPopupPos;

    private void Start()
    {
        playerCharacterHandler.OnShoot += PlayerCharacterHandlerOnShoot;
        enemyCharacterHandler.OnShootEnemy += EnemyCharacterHandlerOnShoot;
        enemyHealthHandler.OnEnemyKilled += PlayerCharacterHandler_OnEnemyKilled;

        windowCharacterPortraitHandler.Show(playerTransform);
    }

    public void Update()
    {
        if (isFollowing == true)
        {
            enemyAIHandler.FindTarget();
        }
    }

    private void PlayerCharacterHandler_OnEnemyKilled(object sender, EnemyHealth.OnEnemyKilledEventArgs e)
    {
       // Vector3 enemyPos = GetComponent<EnemyCharacterHandler>().ActualEnemyPosition;

        Vector3 flyDirection = (enemyCharacterHandler.GetPosition() - playerCharacterHandler.GetPosition()).normalized;
        FlyingBody.Create(pfEnemyDeadBody, enemyCharacterHandler.GetPosition(), flyDirection);
        enemy.SetActive(false);
        UtilsClass.ShakeCamera(2f, .1f);
    }

    public void PlayerCharacterHandlerOnShoot(object sender, PlayerCharacterHandler.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(0.5f, 0.25f);

        Vector3 quadPosition = e.gunEndPosition;
        Vector3 quadSize = new Vector3(0.5f, 1f);
        DmgPopupPos.Set(playerDamagePopupPlace.position.x, playerDamagePopupPlace.position.y, playerDamagePopupPlace.position.z);

        CreateWeaponTracer(e.gunEndPosition, e.shootPosition);
        CreateShootFlash(e.gunEndPosition);

        ShellParticleSystemHandler.Instance.SpawnShell(quadPosition, new Vector3(1, 1));

        RaycastHit2D raycastHit = Physics2D.Raycast(e.gunEndPosition,
            (e.shootPosition - e.gunEndPosition).normalized,
            Vector3.Distance(e.gunEndPosition, e.shootPosition));
        if (raycastHit.collider != null)
        {
            EnemyHealth enemyHealth = raycastHit.collider.GetComponent<EnemyHealth>();
            isFollowing = true;
            StartCoroutine(WaitAfterFollowing());
            if (enemyHealth != null)
            {
                //Hit enemy
                int DamageAmount = UnityEngine.Random.Range(10, 100);
                bool isCritical = UnityEngine.Random.Range(0, 100) < 30;
                if (isCritical) DamageAmount *= 2;
                //Deal Dmg
                enemyHealth.TakeDamage(DamageAmount);
                DamagePopupScript.Create(DmgPopupPos, DamageAmount, isCritical);
            }
        }
    }
    public IEnumerator WaitAfterFollowing()
    {
        yield return new WaitForSeconds(4);
        isFollowing = false;
    }
    private void EnemyCharacterHandlerOnShoot(object sender, EnemyCharacterHandler.OnShootEventArgs e)
    {
        Vector3 quadPosition = e.GunEndPosition;
        Vector3 quadSize = new Vector3(0.5f, 1f);
        DmgPopupPos.Set(enemyDamagePopupPlace.position.x, enemyDamagePopupPlace.position.y, enemyDamagePopupPlace.position.z);

        //Player shot weapon
        if (Time.time > NextEnemyShootTime)
        {
            CreateWeaponTracer(e.GunEndPosition, e.ShootPosition);
            CreateShootFlash(e.GunEndPosition);

            ShellParticleSystemHandler.Instance.SpawnShell(quadPosition, new Vector3(1, 1));
            NextEnemyShootTime = Time.time + FireRate;


            //If any enemy hit
            RaycastHit2D raycastHit = Physics2D.Raycast(e.GunEndPosition,
                (e.ShootPosition - e.GunEndPosition).normalized,
                Vector3.Distance(e.GunEndPosition, e.ShootPosition));
            if (raycastHit.collider != null)
            {
                PlayerHealth playerHealth = raycastHit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    //Hit enemy
                    int DamageAmount = UnityEngine.Random.Range(10, 100);
                    bool isCritical = UnityEngine.Random.Range(0, 100) < 30;
                    if (isCritical) DamageAmount *= 2;
                    //Deal Dmg
                    playerHealth.TakeDamage(DamageAmount);
                    DamagePopupScript.Create(DmgPopupPos, DamageAmount, isCritical);
                }
            }
        }
    }

    private void CreateShootFlash(Vector3 spawnPosition)
    {
        World_Sprite worldSprite = World_Sprite.Create(spawnPosition, shootFlashSprite);
        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }


    private void CreateWeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - fromPosition).normalized;

        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPosition, targetPosition);

        Vector3 tracerSpawnPosition = fromPosition + dir * distance * .5f;
        Material tmpWeaponTracerMaterial = new Material(WeaponTracerMaterial);
        
        tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance/ 256f));
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosition, eulerZ, 6f, distance, tmpWeaponTracerMaterial, null, 10000);


        int frame = 0;
        float frameRate = 0.016f;
        float timer = .1f;
        worldMesh.SetUVCoords(new  World_Mesh.UVCoords(0,0,16,256));
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                frame++;
                timer += frameRate;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                else
                {
                    worldMesh.SetUVCoords(new World_Mesh.UVCoords(16* frame, 0, 16, 256));
                }
            }

            return false;
        });
    }

}

