using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mario : MonoBehaviour
{

	private Rigidbody2D rbd;
	private Animator anim;
	public float speed;
	public float gravityJump;
	private bool ground = true;
	private bool dir = true;
	private bool active;
	private AudioSource[] audios;
	public LayerMask mascara;




	// Start is called before the first frame update
	void Start()
    {
		rbd = GetComponent<Rigidbody2D>();
		speed = 8;
		gravityJump = 220;
		anim = GetComponent<Animator>();

		
		audios = GetComponents<AudioSource>();
		audios[0].Play();
		active = true;

	}

	// Update is called once per frame
	void Update()
    {
		if (active) {
			float x = Input.GetAxis("Horizontal");
			rbd.velocity = new Vector2(x * speed, rbd.velocity.y);
			
			if (Input.GetKeyDown(KeyCode.Space) && ground)
			{	
				audios[1].Play();
				rbd.AddForce(new Vector2(0, gravityJump));
			}
			if (x == 0)
				anim.SetBool("moving", false);
			else
				anim.SetBool("moving", true);

			if (dir && x < 0 || !dir && x > 0)
			{
				transform.Rotate(new Vector2(0, 180));
				dir = !dir;
			}

			RaycastHit2D hit;
			hit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, mascara);
			if (hit.collider != null)
				hit.collider.gameObject.GetComponent<Animator>().SetBool("dying", true);
				Destroy (hit.collider.gameObject, 1f);
				rbd.AddForce(new Vector2(0, 10));
				audios[2].Play();
				CircleCollider2D[] colliders = hit.collider.gameObject.GetComponents<CircleCollider2D>();
				foreach (var collider in colliders)
				{
					collider.enabled = false;
				}
			}
	}

	public void destroyMario() {
		if (!anim.GetBool("dying")) {
			anim.SetBool("dying", true);
			rbd.AddForce(new Vector2(0, 400));
			foreach(Collider2D c in GetComponents<Collider2D> ()) {
        		 c.enabled = false;
    		}	
			active = false;

			Destroy(GameObject.Find("mario"),3.2f);

			audios[0].Stop();
			audios[3].Play();
			Invoke("callMenu", 3.2f);

		}
	
	}
    private void OnTriggerEnter2D (Collider2D collision) {


		if (collision.gameObject.name == "castelo") {
			anim.SetBool("winning", true);
			audios[0].Stop();
			audios[4].Play();
			active = false;
			Invoke("callMenu", 7.0f);
		}
    }

	private void callMenu() {
		SceneManager.LoadScene(0);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.name == "colisores" || collision.gameObject.name == "plataforma"){
			anim.SetBool("jumping", false);
			transform.parent = collision.transform;
			ground = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{	
		if(collision.gameObject.name == "colisores" || collision.gameObject.name == "plataforma"){
			anim.SetBool("jumping", true);
			ground = false;
		}
	}
}
