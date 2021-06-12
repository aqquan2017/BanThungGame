using UnityEngine;
using System.Collections;

public class BeerCan : MonoBehaviour {

	public bool wasHit = false;
	public GameObject[] models;
	public float respawnTime = 3f;
	Vector3 startPos;
	public string prefabName;

	void Start () {
		wasHit = false;

		startPos = transform.position;

		int randomIndex = Random.Range (0, models.Length);
		for (int i = 0; i < models.Length; i++) {
			models [i].SetActive (i == randomIndex);
		}
	}

    private void Update()
    {
        if (wasHit)
        {
			respawnTime -= Time.deltaTime;

			if(respawnTime <= 0f)
            {
				var newObject = Resources.Load<GameObject>(prefabName);
				var createObj = Instantiate(newObject);
				createObj.transform.position = startPos;
				wasHit = false;
				gameObject.SetActive(false);
            }
        }
    }

    public void OnHit (float hitForce, Vector3 direction) {
		GetComponent<Rigidbody> ().AddForce (hitForce * direction);
		wasHit = true;
	}
}
