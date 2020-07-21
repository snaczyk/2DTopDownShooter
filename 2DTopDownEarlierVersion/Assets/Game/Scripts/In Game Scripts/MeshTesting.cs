using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using  CodeMonkey.Utils;

public class MeshTesting : MonoBehaviour
{

    [SerializeField] private PlayerCharacterHandler _playerCharacterHandler;

    // Start is called before the first frame update
    void Start()
    {
        _playerCharacterHandler.OnShoot += PlayerCharacterHandlerOnshoot;
    }

    private void PlayerCharacterHandlerOnshoot(object sender, PlayerCharacterHandler.OnShootEventArgs e)
    {
        Vector3 quadPosition = e.gunEndPosition;
        Vector3 quadSize = new Vector3(0.5f, 1f);
        float rotation = 0f;

        //ShellParticlesSystemHandlerx.Instance.SpawnShell(quadPosition, new  Vector3(1,1));
        //int spawnedQuadIndex = AddQuad(quadPosition, rotation, quadSize, true, 0);

        //FunctionUpdater.Create(() =>
        //{
        //    quadPosition += new Vector3(1, 1) * 3f * Time.deltaTime;
        //    quadSize += new Vector3(1, 1) * Time.deltaTime;
        //    rotation += 180f * Time.deltaTime;
        //    UpdateQuad(spawnedQuadIndex, quadPosition, rotation, quadSize, true, 0);

        //});
    }
}
