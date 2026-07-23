using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    private GameObject _pauseMenuCanvas;

    private bool _isPaused;

    [SerializeField] private string _mainMenuScene;

    void Awake()
    {
        Instance = this;    
        _pauseMenuCanvas = GameObject.Find("PauseMenu");
        //_pauseMenuCanvas.SetActive(false);
    }

    void Start()
    {
        _pauseMenuCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        if(!_isPaused)
        {
            Time.timeScale = 0f;
            _isPaused = true;
            _pauseMenuCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            _isPaused = false;
            _pauseMenuCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
