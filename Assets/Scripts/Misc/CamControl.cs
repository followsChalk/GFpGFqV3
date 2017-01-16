using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {

	public float runMulti =2f;
	public float sneakMulti=0.5f;
	public float movementVelocity=20f;
	public float rotationVelocity=160f;
	public float angleRange=80f;

	// Update is called once per frame
	void Update () 
	{ 
		float multi = 	Input.GetKey(KeyCode.Keypad1) ? runMulti : (Input.GetKey(KeyCode.Keypad0)?sneakMulti:1f);
		float tL	= 	Input.GetKey(KeyCode.A) ? 1f:0f;
		float tR	=	Input.GetKey(KeyCode.D) ? 1f:0f;
		float tF 	= 	Input.GetKey(KeyCode.W) ? 1f:0f; 
		float tB	=	Input.GetKey(KeyCode.S) ? 1f:0f;
		float tU	=	Input.GetKey(KeyCode.E) ? 1f:0f;
		float tD	=	Input.GetKey(KeyCode.Q) ? 1f:0f;
		float rL	= 	Input.GetKey(KeyCode.LeftArrow) ? 1f:0f;
		float rR	= 	Input.GetKey(KeyCode.RightArrow) ? 1f:0f;
		float rU	= 	Input.GetKey(KeyCode.UpArrow) ? 1f:0f;
		float rD	= 	Input.GetKey(KeyCode.DownArrow) ? 1f:0f;

		transform.Translate( 	(	Vector3.right * (tR - tL ) + 
		                       		Vector3.Cross(Vector3.right,transform.InverseTransformDirection(Vector3.up)) * (tF - tB )	+
		                       		transform.InverseTransformDirection(Vector3.up) * (tU - tD ) 
		                       		).normalized * 
		                    	multi * movementVelocity * Time.deltaTime);

		transform.Rotate( transform.InverseTransformDirection(Vector3.up),		(rR - rL) * rotationVelocity * Time.deltaTime );
		Quaternion oldRot = transform.rotation;
		transform.Rotate( Vector3.right,										-(rU - rD) * rotationVelocity * Time.deltaTime);

		if(Vector3.Angle(transform.forward,Vector3.Cross(transform.right,Vector3.up))>angleRange)transform.rotation=oldRot;

	}


}
