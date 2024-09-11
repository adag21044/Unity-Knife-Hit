using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private int knifeCount;

    [Header("Knife Spawning")]
    [SerializeField] private Vector2 knifeSpawnPosition;
    [SerializeField] private GameObject knifeObject;

    public GameUI gameUI { get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        gameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        gameUI.SetInitialDisplayedKnifeCount(knifeCount);
        SpawnKnife();
    }

    public void OnSuccessfulKnifeHit()
    {
        if(knifeCount > 0)
        {
      
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }   
    }

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequence", win);
    }

    private IEnumerator GameOverSequence(bool win)
    {
        if(win)
        {
            yield return new WaitForSecondsRealtime(0.3f);
        }
        else
        {
            gameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}