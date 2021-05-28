using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarryController : MonoBehaviour
{
    public bool trapped = true;
    public float walkSpeed = 4;
    private Animator anim;
    public bool isFacingRight = true;
    private bool flip = true;
    private AudioSource audioEffects;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioEffects = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("walkSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (SceneManager.GetActiveScene().buildIndex == 4) {
            anim.SetBool("Trapped", false);
        }

        if (anim.GetFloat("walkSpeed") > 3.9 && !audioEffects.isPlaying) {
            audioEffects.Play();
        } else if (anim.GetFloat("walkSpeed") < 0.1 && audioEffects.isPlaying) {
            audioEffects.Stop();
        } 
    } 

    private void Flip() {
        this.isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate() {
        if (trapped == false) {
            if (flip) {
                Flip();
                flip = false;
            }
            if (this.isFacingRight) {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.walkSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
            } else {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.walkSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
            }     
        } 
    }

    public void unTrap() {
        trapped = false;
        anim.SetBool("Trapped", trapped);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Wall1") {
            Flip();
            walkSpeed = 0;
            anim.SetFloat("walkSpeed", 0);
        }
    }

    
}
