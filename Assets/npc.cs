using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour {
    private Rigidbody2D rbd;
    private Animator anim;
    public float speed;

    public float time;

    public bool rotateAlone;

    private float dir;

    // Start is called before the first frame update
    void Start () {
        rbd = GetComponent<Rigidbody2D> ();
        speed = 8;
        dir = -0.2f;

        if (rotateAlone) {
            InvokeRepeating ("rotate", time, time);
        }

    }

    // Update is called once per frame
    void Update () {
        rbd.velocity = new Vector2 (dir * speed, rbd.velocity.y);
    }

    public void rotate () {
        transform.Rotate (new Vector2 (0, 180));
        dir = dir * -1;
    }


    private void OnCollisionEnter2D (Collision2D collision) {
        if ( collision.gameObject.name == "mario") {
            collision.gameObject.GetComponent<mario>().destroyMario();
        } 
        
    }


}