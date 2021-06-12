using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public TextMesh infoText;
    public static GameController Instance;

    public bool inGame = true;
	public float timeGame = 20f;
    public int playerScore = 0;
    public int recordScore = 0;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Record"))
        {
			PlayerPrefs.SetInt("Record", 0);
        }

        recordScore = PlayerPrefs.GetInt("Record");
    }


    [ContextMenu("RESET POINT")]
	public void ResetRecord()
    {
		PlayerPrefs.SetInt("Record", 0);
	}

	void Update () {
		GameLogic();
	}


	void GameLogic()
    {
        timeGame -= Time.deltaTime;

        if(timeGame <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (!inGame)
                return;

            if(playerScore > recordScore)
            {
                PlayerPrefs.SetInt("Record", playerScore);
            }

            infoText.text = "Điểm số của bạn là: " + playerScore
                + "\n Kỷ lục: " + PlayerPrefs.GetInt("Record")
                + "\n Nhấn Space để chơi lại!";

            inGame = false;

        }
        else
        {
            int timescore = (int)timeGame;
            infoText.text = "Thời gian còn lại : " + timescore
                +"\n Điểm : " + playerScore;
        }
    }
}
