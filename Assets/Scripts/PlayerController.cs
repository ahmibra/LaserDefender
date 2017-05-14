using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerLaser;
    public float projectileSpeed = 2f;

    public float speed = 12f;
    public float fireRate = 0.1f;

    public float health = 300f;

    float xmin;
    float xmax;
    float padding = 0.7f;

    public AudioClip fireSound;

	void Start () {

        GetComponent<Rigidbody2D>().position = new Vector2(0f, -4.0f);

        xmin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xmax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireLaser", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireLaser");
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Laser missile = col.gameObject.GetComponent<Laser>();
            if (missile)
            {
                Debug.Log("hit");
                hit(missile);
            }
        }
    }

    void hit(Laser missile)
    {
        health -= missile.getDamage();
        missile.hit();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            man.LoadLevel("Win Screen");
        }
    }

    void FireLaser()
    {
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
        GameObject laser = Instantiate(playerLaser, pos, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void UpdatePosition()
    {
        Vector2 currentPos = GetComponent<Rigidbody2D>().position;

        Vector2 deltaPos = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            deltaPos += Vector2.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            deltaPos += Vector2.right * speed * Time.deltaTime;
        }

        //set new position
        Vector2 newPos = currentPos + deltaPos;
        GetComponent<Rigidbody2D>().position = new Vector2(Mathf.Clamp(newPos.x, xmin, xmax), newPos.y);
    }
}
