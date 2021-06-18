using UnityEngine;

public class ObjectTarget : MonoBehaviour {

	public bool wasHit = false;
	public GameObject[] models;
	public float respawnTime = 3f;
	Vector3 startPos, startRotation, startScale;
	public string prefabName;
	public Map map;
	Rigidbody Rigidbody;

	public float flyRange = 10f;
	public float flySpeed = 3f;

	float minRange, maxRange;


	void Start () {
		wasHit = false;
		Rigidbody = GetComponent<Rigidbody>();

		if(map != Map.Map1)
        {
			transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(2f, 5f), Random.Range(-2, 3));
			transform.eulerAngles = Random.value < 0.5f ? Vector3.up * 90 : Vector3.down * 90;
        }

		startPos = transform.position;
		startRotation = transform.eulerAngles;
		startScale = transform.localScale;

		int randomIndex = UnityEngine.Random.Range (0, models.Length);
		for (int i = 0; i < models.Length; i++) {
			models [i].SetActive (i == randomIndex);
		}

		maxRange = transform.position.x + flyRange;
		minRange = transform.position.x - flyRange;
	}

    private void Update()
    {
        if (wasHit)
        {
			respawnTime -= Time.deltaTime;

			if(respawnTime <= 0f)
            {
				var newObject = Resources.Load<GameObject>(prefabName);
				var createObj = Instantiate(newObject , transform.parent);
				createObj.transform.position = startPos;
				createObj.transform.eulerAngles = startRotation;
				createObj.transform.localScale = startScale;
				wasHit = false;
				gameObject.SetActive(false);
            }
        }

		MapLogicBehaviour();
    }

	void MapLogicBehaviour()
    {
        switch (map)
        {
			case Map.Map2:
            {
				if (wasHit)
					return;

				Rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * flySpeed);

				if(transform.position.x < minRange )
                    {
						transform.eulerAngles = Vector3.up * 90; 
                    }

				if(transform.position.x > maxRange)
                    {
						transform.eulerAngles = - Vector3.up * 90;
					}

					break;
            }
			case Map.Map3:
            {
				
				
                break;
            }
        }
    }

    public void OnHit (float hitForce, Vector3 direction) {
		GetComponent<Rigidbody> ().AddForce (hitForce * direction);
		GetComponent<Rigidbody>().useGravity = true;
		wasHit = true;
	}
}
