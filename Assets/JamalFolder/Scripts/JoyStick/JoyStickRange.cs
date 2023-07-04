using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickRange : MonoBehaviour
{
	public Joystick joystick;

	private void Update()
	{
		Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

		if (moveVector != Vector3.zero)
		{ 
			transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
		}
	}
}
