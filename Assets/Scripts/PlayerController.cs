 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof (CharacterController))]


public class PlayerController : MonoBehaviour {

	//References
	public RawImage CoolDown;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	//Handling
	public float Recoil = 1f;
	public float ReloadSpeed = 1f;
	public float rotationSpeed = 450f;

	public float bulletVelocity = 6f;
	public float bulletDuration = 2f;
    public float BackwardsVelocity = 2f;
    public float ForwardsVelocity = 6f;
    public float SidewaysVelocity = 4f;

    
	//Local Variables
	private Quaternion targetRotation;

	private CharacterController controller;
	private float TimeStamp;
	private float speed = 5f;
	void Fire(){
		var bullet = (GameObject)Instantiate( bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		//Changing velocity as opposed to the tranform of the object takes into account collisions,
		//where the transform component does notbv n
		bullet.GetComponent<Rigidbody> ().velocity = bulletSpawn.transform.forward * bulletVelocity;
		Destroy (bullet, bulletDuration);
		CoolDown.texture = Resources.Load ("10_Empty") as Texture;
		//When it's time to start firing again
		TimeStamp = Time.time + ReloadSpeed;
	}
	void Start () {
		//Creates a reference the the character controller controller so that methods are easily used
		controller = GetComponent<CharacterController>();
      

		TimeStamp = Time.time;
	}


	void FixedUpdate () {
		
        
		Vector3 movement = new Vector3 (Input.GetAxisRaw ("Horizontal"), -1, Input.GetAxisRaw ("Vertical"));
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position);
     
        difference.Normalize ();
		targetRotation = Quaternion.LookRotation (difference);
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		Vector3 motion = movement.normalized;
       
     	//Determines if the player is moving backwards, sideways or forwards
        if (Vector3.Dot(transform.forward, motion) > .4)
        {
            speed = ForwardsVelocity;
        }
        else if(Vector3.Dot(transform.forward, motion) < -.6)
        {
            speed = BackwardsVelocity;
        }
        else
        {
            speed = SidewaysVelocity;
        }
        controller.Move(motion * Time.deltaTime * speed);
        
		//Detects whether the player is shooting and if the cooldown has passed
		if ((Input.GetKeyDown (KeyCode.Mouse0)) && (Time.time >= TimeStamp)) {
			Fire ();
			controller.Move((transform.forward * -1) * Recoil);
		}

 


		//Laborious code to change the texture of the UI 
		float TimeCalc = TimeStamp - Time.time;
		if ((TimeCalc) > (0.9  * ReloadSpeed) ) {
			CoolDown.texture = Resources.Load ("10_Empty") as Texture;
		}
		else if((TimeCalc ) >(0.8 * ReloadSpeed)) {

			CoolDown.texture = Resources.Load ("9") as Texture;
		}
		else if((TimeCalc) > (0.7  * ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("8") as Texture;
		}
		else if((TimeCalc) > (0.6* ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("7") as Texture;
		}
		else if((TimeCalc) > (0.5* ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("6") as Texture;
		}
		else if((TimeCalc) > (0.4* ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("5") as Texture;
		}
		else if((TimeCalc) > (0.3* ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("4") as Texture;
		}
		else if((TimeCalc) > (0.2 * ReloadSpeed) ) {
			CoolDown.texture = Resources.Load ("3") as Texture;
		}
		else if((TimeCalc) > (0.1 * ReloadSpeed)) {
			CoolDown.texture = Resources.Load ("2") as Texture;
		}
		else {
			CoolDown.texture = Resources.Load ("1_Full") as Texture;
		}
	

	}
}
