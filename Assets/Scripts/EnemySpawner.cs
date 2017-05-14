using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 7.21f;
    public float height = 3.4f;
    public float speed = 2.5f;

    private int direction = -1; //direction of movement

    private float spawnDelay = 0.5f;

    // Use this for initialization
    void Start () {

        spawnUntilFull();
	}

    private void OnDrawGizmos()

    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {

        float currentxLeft = transform.position.x - (width / 2);
        float currentxRight = transform.position.x + (width / 2);

        if (currentxLeft <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            direction = 1;
        }
        else if (currentxRight >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            direction = -1;
        }

        float xDelta = direction * speed * Time.deltaTime;
        transform.position += new Vector3(xDelta, 0, 0);

        if (allMembersDead())
        {
            spawnUntilFull();
        }
	}

    Transform nextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    } 

    private bool allMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void spawnUntilFull()
    {
        Transform freePosition = nextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (nextFreePosition())
        {
            Invoke("spawnUntilFull", spawnDelay);
        }
    }
}
