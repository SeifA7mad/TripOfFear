using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public bool isFacingRight = false;
    public Animator anim;
    public float distance = 0.1f;
    private int layerMask = 1 << 2;
    private bool moving = true;
    private float waitDuration = 15f;
    private float waitTime = 0f;
    private float shootDuration = 5f;
    private float shootTime = 5f;
    public GameObject bullet;
    private bool died = false;
    private bool canFlip;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().buildIndex != 4) {
            this.gameObject.SetActive(false);
        }
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update() {
       anim.SetFloat("walkSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

    //shooting
       Physics2D.queriesStartInColliders = false;
       RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, layerMask);

       if (hit.collider != null && hit.collider.gameObject.tag == "Player" && !died) {
           if (hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled) {
               if (shootTime < shootDuration) {
                   shootTime += Time.deltaTime;
               } else {
                    shootTime = 0f;
                    Instantiate(bullet, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
               }
                this.moveSpeed = 0f;
                anim.SetFloat("walkSpeed", this.moveSpeed);
                anim.SetBool("Shoot", true);
                if (SceneManager.GetActiveScene().buildIndex != 4) { 
                    FindObjectOfType<PlayerController>().died = true;
                    FindObjectOfType<LevelManager>().gameOver = true;
                }
           }
       }

       if (SceneManager.GetActiveScene().buildIndex == 2) {
            if (!moving) {
                if (waitTime < waitDuration) {
                        waitTime += Time.deltaTime;              
                        if (FindObjectOfType<PlayerController>().anim.GetFloat("walkSpeed") > 2 && canFlip) {
                            Flip();
                            canFlip = false;
                            waitTime = 15f;
                        }
                }
                else {
                    waitTime = 0f;
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    moving = true;
                }
            }
       }


       //Dieing
        if (SceneManager.GetActiveScene().buildIndex == 4 && GetComponent<Status>().currentHealth <= 0) {
            this.died = true;
        }
        if (died) {
            moveSpeed = 0f;
            anim.SetFloat("walkSpeed", moveSpeed);
            anim.SetBool("Die", died);
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Flip() {
        this.isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate() {
        if (moving) {
            if (this.isFacingRight) {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.moveSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
            } else {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.moveSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
            }     
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Wall") {
            Flip();
        }
        if (other.tag == "Stop") {
            this.Flip();
            FindObjectOfType<LevelManager>().gone = true;
            this.gameObject.SetActive(false);
        }
        
        if (other.tag == "Wait") {
            if (isFacingRight) {
                StartCoroutine(FindObjectOfType<DialogSequense>().dialogSequence("Miles: I can see keys in his pocket I must sneak behind him and steal it"));
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
                moving = false;
                canFlip = true;
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    } 


     private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * distance);    
    }
   
}
