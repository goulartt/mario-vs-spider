using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
	public GameObject PC;
	public float y_offSet = 1;
	public float h_limite = 3;
	public float v_limite = 2;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {	
		if (PC != null) {

		
			Vector2 dist = PC.transform.position - transform.position;
			int factor = (dist.x < 0) ? -1 : 1;

			if (dist.x > h_limite || dist.x < -h_limite)
				transform.position = new Vector3(PC.transform.position.x - h_limite * factor,
											PC.transform.position.y + y_offSet,
											-10);
			else
				transform.position = new Vector3(transform.position.x,
												PC.transform.position.y + y_offSet,
												-10);
		}
	}
}
