using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public enum Map
{
    Map1, Map2, Map3
}

public class GameController : MonoBehaviour {

	public TextMesh infoText;
    public static GameController Instance;

    public bool inGame = true;
	public float timeGame = 20f;
    public int playerScore = 0;
    public int recordScore = 0;

    public Action NextGame;
    public Map map;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey(map.ToString()))
        {
			PlayerPrefs.SetInt(map.ToString(), 0);
        }

        recordScore = PlayerPrefs.GetInt(map.ToString());

        NextGame += UIManager.Instance.NextGame;
    }

    private void OnDestroy()
    {
        NextGame -= UIManager.Instance.NextGame;
        
    }


    [ContextMenu("RESET POINT")]
	public void ResetRecord()
    {
		PlayerPrefs.SetInt("Map1", 0);
		PlayerPrefs.SetInt("Map2", 0);
		PlayerPrefs.SetInt("Map3", 0);
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
            else if (Input.GetKeyDown(KeyCode.P))
            {
                NextGame?.Invoke();
            }

            if (!inGame)
                return;


            if(playerScore > recordScore)
            {
                PlayerPrefs.SetInt(map.ToString(), playerScore);
            }

            infoText.text = "Điểm số của bạn là: " + playerScore
                + "\n Kỷ lục: " + PlayerPrefs.GetInt(map.ToString())
                + "\n Nhấn Space để chơi lại, P để chơi tiếp!";


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
