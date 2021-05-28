using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        if (!FindObjectOfType<EnemyController>().isFacingRight) {
            speed = -speed;
        } else {
            speed = +speed;
            flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {           
        if (other.tag == "Player" && SceneManager.GetActiveScene().buildIndex == 4) {
            other.gameObject.GetComponent<Status>().takeDamage(damage);
        } else {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        StartCoroutine(gunShoot());
    }


    private IEnumerator gunShoot() {
        yield return new WaitUntil(() => !GetComponent<AudioSource>().isPlaying);
        Destroy(this.gameObject); 
    }
    void flip() {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
