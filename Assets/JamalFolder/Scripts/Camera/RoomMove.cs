using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomMove : MonoBehaviour
{
	[SerializeField]
	private Vector2 MaxCameraPosition;
	[SerializeField]
	private Vector2 MinCameraPosition;
	public Vector3 playerChange;
	private CameraMovement Cam;
	public bool needText;
	public string placeName;
	public GameObject text;
	public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
		Cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	private void OnTriggerEnter2D(Collider2D NextRoom) {
		if (NextRoom.CompareTag("Player")) { 	
			Cam.MaxCameraPosition = MaxCameraPosition;
			Cam.MinCameraPosition = MinCameraPosition;
			NextRoom.transform.position += playerChange;
			if(needText)
			{
				StartCoroutine(placeNameCo());
			}
		}
	}
	private IEnumerator placeNameCo() {
		text.SetActive(true);
		placeText.text = placeName;
		yield return new WaitForSeconds(4f);
		text.SetActive(false);
	}
}
