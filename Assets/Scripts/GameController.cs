using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public TextMesh infoText;
    public static GameController Instance; 

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

    public System.Action playerAction;

	void GameLogic()
    {
        timeGame -= Time.deltaTime;

        if(timeGame <= 0f)
        {
            if(playerScore > recordScore)
            {
                PlayerPrefs.SetInt("Record", playerScore);
            }

            infoText.text = "KET THUC GAME, DIEM SO CUA BAN LA: " + playerScore
                + "\n KY LUC: " + PlayerPrefs.GetInt("Record")
                + "\n AN SPACE DE CHOI LAI";


            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            int timescore = (int)timeGame;
            infoText.text = "THOI GIAN : " + timescore;
        }
    }
}
