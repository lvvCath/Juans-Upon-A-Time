using System;
using UnityEngine;

[Serializable]
public class Block 
{
	[SerializeField]
	private GameObject block1, block2;

	public void Deactivate() {
		block1.SetActive (false);
		block2.SetActive (false);
	}

	public void Activate() {	
		block1.SetActive (true);
		block2.SetActive (true);
	}
}
