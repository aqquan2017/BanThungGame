using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public TextMesh infoText;
	public BeerCan[] beerCans;

	private float recordPoint;

	[SerializeField] private float timeGame = 20f;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Record"))
        {
			PlayerPrefs.SetFloat("Record", 0f);
        }

		recordPoint = PlayerPrefs.GetFloat("Record");
    }


    [ContextMenu("RESET POINT")]
	public void ResetRecord()
    {
		PlayerPrefs.SetFloat("Record", 0f);
	}

	void Update () {
		GameLogic();
	}

	void GameLogic()
    {
		bool won = true;
		foreach (BeerCan can in beerCans)
		{
			if (can.wasHit == false)
			{
				won = false;
				break;
			}
		}

		if (won == false && timeGame > 0f)
		{
			timeGame -= Time.deltaTime;
			infoText.text = "Bắn tất cả chai bia!\nThời gian còn lại: " + Mathf.Floor(timeGame);
		}
		else
		{

			if (won)
			{
				if (timeGame > recordPoint)
				{
					recordPoint = timeGame;
					PlayerPrefs.SetFloat("Record", recordPoint);
				}

				infoText.text = "Bạn thắng ! Thời gian chơi: " + System.Math.Round(timeGame, 2) + "s\n"
					+ "Kỷ lục hiện tại : " + System.Math.Round(recordPoint, 2)
					+ "\n Nhấn 'Space' để chơi lại !";

			}
			else
			{
				infoText.text = "Bạn thua ! Nhấn 'Space' để chơi lại\n"
					+ "Kỷ lục hiện tại : " + System.Math.Round(recordPoint, 2);
			}


			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}
