using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Zmienne

    [Header("Translation Controls")]
    [SerializeField] Camera MainCamera;
    [SerializeField] private Vector2 MiejsceWSwiecie;
    [SerializeField] private Vector2 RuchGracza;
    [Range(1, 50)] [SerializeField] private float Velocity;

    public Vector2 WektorObrotuGracza;

    public Rigidbody2D rb;
    #endregion Zmienne
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MiejsceWSwiecie = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        TurnPlayer();
        RuchGracza.x = Input.GetAxis("Horizontal");
        RuchGracza.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float kat = Mathf.Atan2(WektorObrotuGracza.y, WektorObrotuGracza.x) * Mathf.Rad2Deg;
        rb.rotation = kat - 90;
        rb.MovePosition(rb.position + RuchGracza * Velocity * Time.fixedDeltaTime);
    }
    void TurnPlayer()
    {
        WektorObrotuGracza = MiejsceWSwiecie - (Vector2)transform.position;
    }


}
