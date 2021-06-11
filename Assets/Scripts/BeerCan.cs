using UnityEngine;
using System.Collections;
using UnityEngine;

public class BeerCan : MonoBehaviour {

	public bool wasHit = false;
	public GameObject[] models;
	public float respawnTime = 3f;
	Vector3 startPos;
	public GameObject prefab;

	void Start () {
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
				var newObject = Instantiate(prefab);
				newObject.transform.position = startPos;
				Destroy(gameObject);
            }
        }
    }

    public void OnHit (float hitForce, Vector3 direction) {
		GetComponent<Rigidbody> ().AddForce (hitForce * direction);
		wasHit = true;
	}
}
