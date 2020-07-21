using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int OppeningDirection;
    // 1--> need bottom door
    // 2--> need top door
    // 3--> need left door
    // 4--> need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            switch (OppeningDirection)
            {
                case 1:
                    rand = Random.Range(0, templates.BottomRooms.Length);
                    Instantiate(templates.BottomRooms[rand], gameObject.transform.position, new Quaternion());
                    break;
                case 2:
                    rand = Random.Range(0, templates.TopRooms.Length);
                    Instantiate(templates.TopRooms[rand], gameObject.transform.position, new Quaternion());
                    break;
                case 3:
                    rand = Random.Range(0, templates.TopRooms.Length);
                    Instantiate(templates.TopRooms[rand], gameObject.transform.position, new Quaternion());
                    break;
                case 4:
                    rand = Random.Range(0, templates.TopRooms.Length);
                    Instantiate(templates.TopRooms[rand], gameObject.transform.position, new Quaternion());
                    break;
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
