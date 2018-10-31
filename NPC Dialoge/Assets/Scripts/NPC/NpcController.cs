using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour {

    private GameObject target;
    private int talkRange = 4;
    

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.transform.position, transform.position) < talkRange) {
            LookAtPlayer();
            if (Input.GetKeyDown("b")) {
                Interact();
            }
        }     
    }

    void LookAtPlayer() {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
    }

    void Interact() {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
