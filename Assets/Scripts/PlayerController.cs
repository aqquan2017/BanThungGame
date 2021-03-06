using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public Map map;
	public float rotationSpeed = 10f;


	void FixedUpdate()
	{
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    void Update () {
		Shot();
		UpdateLogicMap();
	}

	void Shot()
    {
		if (Input.GetMouseButtonDown(0))
		{

			RaycastHit hit;
			SoundManager.Instance.PlayAudio(SoundManager.Instance.shotSound);

			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{

				if (hit.transform.GetComponent<ObjectTarget>() != null)
				{
					GameController.Instance.playerScore++;

					hit.transform.GetComponent<ObjectTarget>().OnHit(700, transform.forward);
					SoundManager.Instance.PlayAudio(SoundManager.Instance.hitSound);
				}
			}
		}
	}

	void UpdateLogicMap()
    {
        switch (map)
        {
			case Map.Map3:
                {
					Transform t = transform.parent;
					t.eulerAngles += Vector3.up * Time.deltaTime * rotationSpeed;
					break;
                }
        }
    }

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
	}
}
