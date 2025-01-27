using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSpawn : MonoBehaviour
{
    
    // Variables determining Spawnrate etc all public for dynamic increase possibility
    public float[] Spawnpositions = {2f, -2f};
    public float SpawnFrequency = 1.0f;
    public int SpawnAmount = 1;
    public int DifficultyStep = 3;

    // GameObjects of Collision Item and it's prefab (technically both variables are the same but one consistent is needed)
    public GameObject objectToSpawn;
    public GameObject CollisionInstance;
    
    private GameObject player;
    private PlayerInput playeScript;
    
    private GameObject spawner;

    private Vector3 position = new Vector3(0, 0, 0);
    private Quaternion rotation = Quaternion.Euler(0, 0, 90);


    void Start()
    {
        // get GameObject's
        spawner = GameObject.FindWithTag("Spawner");


        player = GameObject.FindWithTag("Player");
        playeScript = player.GetComponent<PlayerInput>();

        // Invoke the function to spawn Collision elements
        InvokeRepeating("instance_of_object", 2.0f, SpawnFrequency);
      
    }

    // spawn an instance of the collider GameObject at the spawner 
    //position with a random offset according to rates dynamically set
    void instance_of_object(){

        // get position data of spawner
        position = position = spawner.transform.position;
        position.y = Spawnpositions[Random.Range(0, 2)];
       
        CollisionInstance = Instantiate(objectToSpawn, position, rotation);
        if (SpawnAmount > 1) {
            StartCoroutine(WaitForSpawning(SpawnFrequency, SpawnAmount));
        }

    }

    // wait for x amount of seconds, then reload to the main menu
    IEnumerator WaitForSpawning(float seconds, int spawnAmount){

        for (int i = 1; spawnAmount > i; i++){

            yield return new WaitForSecondsRealtime(seconds);

            CollisionInstance = Instantiate(objectToSpawn, position, rotation);

        }
        
    }

}
