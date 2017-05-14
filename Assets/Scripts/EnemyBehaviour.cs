using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    
    public float health = 300f;
    public GameObject enemyLaser;
    private float projectileSpeed = -10f;
    private float shotsPerSecond = 0.9f;

    public int scoreValue = 150;
    private ScoreKeeper scoreKeeper;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if(Random.value < probability)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        Vector3 pos = transform.position - new Vector3(0, 0.5f, 0);
        GameObject laser = Instantiate(enemyLaser, pos, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Laser missile = col.gameObject.GetComponent<Laser>();
            if (missile)
            {
                scoreKeeper.Score(scoreValue);
                Debug.Log("hit");
                hit(missile);
            }
        }
    }

    void hit(Laser missile)
    {
        health -= missile.getDamage();
        missile.hit();

        if(health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(this.gameObject);
        }
    }

}
