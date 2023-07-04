using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterModernDoor : MonoBehaviour 
{

	public bool playerInRange;
	public Vector2 playerPosition;
	public VectorValue playerStorage;
	public GameObject fadeInPanel;
	public GameObject fadeOutPanel;
	public float fadeWait;

	private void Awake()
	{
		if(fadeInPanel != null){
			GameObject panel = Instantiate(fadeInPanel, 
				Vector3.zero, 
				Quaternion.identity) as GameObject;
			Destroy(panel, 1);
		}
	}


	void Start (){
	}

	private void Update (){
		if(Input.GetKeyDown(KeyCode.Space)){
			if (playerInRange){
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
				StartCoroutine(FadeCo());
				playerStorage.initialValue = playerPosition;
			}
		}
	}

	public IEnumerator FadeCo(){
		if(fadeOutPanel !=null) {
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
		while(!asyncOperation.isDone){
			yield return null;
	}


	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player")) {
			Debug.Log("PlayerInRange");
			playerInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player")) {
			playerInRange = false;
		}
	}

}
