using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    PlayerController player;
    GameController gameController;
    public static UIManager Instance;
    public Text recordMap1, recordMap2, recordMap3;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OnOffMenuSetting();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffMenuSetting();
        }
    }

    void OnOffMenuSetting()
    {
        bool currentState = transform.GetChild(0).gameObject.activeSelf;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!currentState);
        }
        Cursor.lockState = currentState ? CursorLockMode.Locked : CursorLockMode.None;

        if (!player)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        player.enabled = currentState;

        if (!gameController)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
        gameController.enabled = currentState;

        recordMap1.text = "Map 1 : " +PlayerPrefs.GetInt("Map1").ToString();
        recordMap2.text = "Map 2 : " + PlayerPrefs.GetInt("Map2").ToString();
        recordMap3.text = "Map 3 : " + PlayerPrefs.GetInt("Map3").ToString();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        OnOffMenuSetting();
        NextGame();
    }

    public void NextGame()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        if (CurrentScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            Destroy(SoundManager.Instance.gameObject);
            SceneManager.LoadScene(0);
            Destroy(this.gameObject);
        }
        else
        {
            SceneManager.LoadScene(CurrentScene + 1);
        }
    }

    public void OnVolumeChangeValue(float var)
    {
        SoundManager.Instance.audioSource.volume = var;
    }
}
