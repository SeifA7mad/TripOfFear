using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float sneakSpeed;
    public bool isFacingRight;
    public KeyCode L;
    public KeyCode R;
    public KeyCode shift;
    public KeyCode F;
    public KeyCode space;
    public KeyCode c;
    public bool usingFlashlight;
    public bool died;
    public Animator anim;
    public float distance;
    private int layerMask = 1 << 8;
    private GameObject Pushable;
    private float attakDuration = 0.5f;
    private float attackTime = 0f;

    void Start() {
        this.isFacingRight = true;
        this.usingFlashlight = false;
        this.died = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        layerMask = ~layerMask;
    }

    void flip() {
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    
    // Update is called once per frame
    void Update() {
    //Walking
        if (Input.GetKey(this.L)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-this.moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (this.isFacingRight) {
                flip();
                this.isFacingRight = false;
            }
        }

        if (Input.GetKey(this.R)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(this.moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (!this.isFacingRight) {
                flip();
                this.isFacingRight = true;
            }
        }
    //Snekaing    
        if (Input.GetKey(this.L) && Input.GetKey(this.shift)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-this.sneakSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (this.isFacingRight) {
                flip();
                this.isFacingRight = false;
            }
        }

        if (Input.GetKey(this.R) && Input.GetKey(this.shift)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(this.sneakSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (!this.isFacingRight) {
                flip();
                this.isFacingRight = true;
            }
        }
    //Flashlight
        if (this.gameObject.GetComponent<PlayerInventory>().Flashlight) {
            if (Input.GetKeyDown(this.F)) {
                this.usingFlashlight = !this.usingFlashlight;
                anim.SetBool("Flashlight", usingFlashlight);
                if ( this.gameObject.transform.GetChild(0).gameObject.GetComponent<FlashlightRaycast>() != null) {
                    this.gameObject.transform.GetChild(0).gameObject.GetComponent<FlashlightRaycast>().mystery.GetComponent<SpriteRenderer>().enabled = false;
                }
                this.gameObject.transform.GetChild(0).gameObject.SetActive(!this.gameObject.transform.GetChild(0).gameObject.activeSelf);
            }
        }
        anim.SetFloat("walkSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("sneakSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        
        if (anim.GetFloat("walkSpeed") == 4 || anim.GetFloat("sneakSpeed") == 2) 
            anim.SetBool("moving", true);
        else {
            anim.SetBool("moving", false);
        }
        
    //Dieing
        if (SceneManager.GetActiveScene().buildIndex == 4 && GetComponent<Status>().currentHealth <= 0) {
            this.died = true;
            FindObjectOfType<LevelManager>().gameOver = true;
        }
        if (died) {
            moveSpeed = 0f;
            sneakSpeed = 0f;
            anim.SetFloat("walkSpeed", moveSpeed);
            anim.SetFloat("sneakSpeed", sneakSpeed);
            anim.SetBool("Die", died);
        }

    //Pushing 
        if (SceneManager.GetActiveScene().buildIndex == 3) {
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, layerMask);

            if (hit.collider != null && hit.collider.gameObject.tag == "Pushable" && Input.GetKey(space)) {
                Pushable =  hit.collider.gameObject;
                Pushable.GetComponent<PushController>().isBeingPushed = true;
                Pushable.GetComponent<FixedJoint2D>().enabled = true;
                Pushable.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                anim.SetBool("Pushing", true);
            } else if (Input.GetKeyUp(space)) {
                    Pushable.GetComponent<PushController>().isBeingPushed = false;
                    Pushable.GetComponent<FixedJoint2D>().enabled = false;
                    anim.SetBool("Pushing", false);
            }
        }

    //Blocking
        if (SceneManager.GetActiveScene().buildIndex == 4 && Input.GetKey(c)) {
            anim.SetBool("Block", true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        } else if (Input.GetKeyUp(c)) {
            anim.SetBool("Block", false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }

    //Attack
        if (SceneManager.GetActiveScene().buildIndex == 4 && Input.GetKeyDown(space)) {
            attackTime = 0f;
            anim.SetBool("Attack", true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        } else if (SceneManager.GetActiveScene().buildIndex == 4) {
            if (attackTime < attakDuration)
                attackTime += Time.deltaTime;
            else {
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                attackTime = 0f;
                anim.SetBool("Attack", false);
            }
        }
    }

    public void Hide() {
        if (this.usingFlashlight) {
            if ( this.gameObject.transform.GetChild(0).gameObject.GetComponent<FlashlightRaycast>() != null)
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<FlashlightRaycast>().mystery.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(!this.gameObject.transform.GetChild(0).gameObject.activeSelf);
        }
       this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
       this.gameObject.GetComponent<BoxCollider2D>().isTrigger = !this.gameObject.GetComponent<BoxCollider2D>().isTrigger;
       if (this.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic) {
           this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
           this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
       } else {
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
       }
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);    
    }

}

    

