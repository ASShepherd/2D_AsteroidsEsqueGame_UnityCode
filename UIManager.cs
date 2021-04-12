using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Text _RestartText;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _LivesSprites;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //_ScoreText = GameObject.Find("Score_Text").GetComponent<Text>();    IS AN ALTERNATIVE TO SERIALIZING 
        _ScoreText.text = "Score: " + 0;
        _GameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("Game Manager is NULL!");
        }
    }


    public void UpdateScore(int PlayerScore)
    {
        _ScoreText.text = "Score: " + PlayerScore;
    }

    public void UpdateLives(int CurrentLives)
    {
        _LivesImage.sprite = _LivesSprites[CurrentLives];
        if (CurrentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        StartCoroutine(GameOverFlicker());
        _RestartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _GameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _GameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
