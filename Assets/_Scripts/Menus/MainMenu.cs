using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private string _testProceduralSceneName;
    
    public void Play()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void TestProcedural()
    {
        SceneManager.LoadScene(_testProceduralSceneName);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
