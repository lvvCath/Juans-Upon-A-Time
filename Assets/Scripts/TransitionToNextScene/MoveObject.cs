using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{
	public int NextSceneString;
	public GameObject[] objects;
	private bool load;
    public Vector3 playerChange;
    private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			if (!load)
			{
				load = true;

				StartCoroutine(ChangeScene());

                other.transform.position += playerChange;
            }

        }

	}
	public IEnumerator ChangeScene()
	{
		SceneManager.LoadScene(NextSceneString, LoadSceneMode.Additive);

		Scene NextScene = SceneManager.GetSceneAt(1);

		SceneManager.MoveGameObjectToScene(objects[0], NextScene);
		SceneManager.MoveGameObjectToScene(objects[1], NextScene);
		SceneManager.MoveGameObjectToScene(objects[2], NextScene);
		SceneManager.MoveGameObjectToScene(objects[3], NextScene);
		SceneManager.MoveGameObjectToScene(objects[4], NextScene);
		SceneManager.MoveGameObjectToScene(objects[5], NextScene);

		yield return null;

		SceneManager.UnloadScene(NextSceneString - 1);
	}
}
