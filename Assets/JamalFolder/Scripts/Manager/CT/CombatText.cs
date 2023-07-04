using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
	[SerializeField]
	private float Speed;
	//LIFE TIME
	[SerializeField]
	private float lifeTime;

	[SerializeField]
	private Text text;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(TaxtFadeOut());
    }

    // Update is called once per frame
    void Update()
    {
		TextMovement();
    }
	private void TextMovement() {
		transform.Translate(Vector2.up * Speed * Time.deltaTime);
	}
	public IEnumerator TaxtFadeOut()
	{
		float starAlpha = text.color.a;

		float rate = 1.0f / lifeTime;

		float progress = 0.0f;

		while (progress < 1.0) {
			Color tmp = text.color;

			tmp.a = Mathf.Lerp(starAlpha, 0, progress);

			text.color = tmp;

			progress += rate * Time.deltaTime;

			yield return null;
		}
		Destroy(gameObject);
	}
}
