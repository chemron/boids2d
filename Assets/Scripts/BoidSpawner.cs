using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {
    public GameObject boidPrefab;
    public int numBoids = 10;
    Vector2 screenHalfSizeWorldUnits;
    
    // Start is called before the first frame update


    private void Start() {
        screenHalfSizeWorldUnits = new (Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        // spawn in boids
        for (int i = 0; i < numBoids; i++) {
            Vector2 spawnPostition = new Vector2(
                x: Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x),
                y: Random.Range(-screenHalfSizeWorldUnits.y, screenHalfSizeWorldUnits.y)
            );
            Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Instantiate(boidPrefab, spawnPostition, spawnRotation);
        }
    }
}
