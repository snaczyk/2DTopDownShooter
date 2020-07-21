using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 direction;
    private Vector2 vectorEnemyRotation;
    private Rigidbody2D enemyRigidbody;

    [SerializeField] private GameHandlerScript gameHandlerScript;

    [Range(50, 200)] public float targetRange;
    [Range(1, 20)] [SerializeField] private float enemySpeed = 10f;
    [SerializeField] private Transform playerPosition;

    //private void Awake()
    //{
    //        //path
    //}

    private void Start()
    {
        startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = (transform.position - playerPosition.position).normalized;
        //Player within target range
        float distance = Vector3.Distance(transform.position, playerPosition.position);

        if (distance < targetRange) // doing distance check
        {
            FindTarget();
            gameHandlerScript.isFollowing = true;
        }

        if (targetRange > distance)
        {
            gameHandlerScript.isFollowing = false;
        }

        vectorEnemyRotation = (Vector2)playerPosition.position - (Vector2)transform.position;
    }

    //public IEnumerator WaitAfterFollowing()
    //{
    //    yield return new WaitForSeconds(4);
    //    gameHandlerScript.isFollowing = false;
    //}

    //private Vector3 GetRoamingPosition()
    //{
    //    return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);
    //}

    public void FindTarget()
    {
        float kat = Mathf.Atan2(vectorEnemyRotation.y, vectorEnemyRotation.x) * Mathf.Rad2Deg;
        enemyRigidbody.rotation = kat - 90;

        enemyRigidbody.MovePosition(transform.position - direction * enemySpeed * Time.fixedDeltaTime);
    }

    //private void Patrol()
    //{

    //}

}
