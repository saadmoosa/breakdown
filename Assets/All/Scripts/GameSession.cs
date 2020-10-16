using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.1f, 5f)] [SerializeField] float timeSpeed = 1.0f;
    [SerializeField] int pointsPerBlock;
    [SerializeField] int points = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplayEnabled;

    // Start is called before the first frame update

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed;
    }

    public void addPoints()
    {
        points = points + pointsPerBlock;
        scoreText.text = points.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool isAutoplayEnabled()
    {
        return autoplayEnabled;
    }

}
