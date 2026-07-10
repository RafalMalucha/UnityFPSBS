using UnityEngine;

public class SceneEnemyManager : MonoBehaviour
{

    //Singleton
    public static SceneEnemyManager Instance;
    
    private SingleEnemyManager[] _listOfAliveEnemies;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log(GetComponentsInChildren<SingleEnemyManager>());
        _listOfAliveEnemies = FindObjectsByType<SingleEnemyManager>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update() 
    {
        _listOfAliveEnemies = FindObjectsByType<SingleEnemyManager>(FindObjectsSortMode.None);
        string list = "";
        foreach(SingleEnemyManager sem in _listOfAliveEnemies)
        {
            list += sem.name + ", ";
        }
        //Debug.Log(list);
    }

    public void UpdateListOfAliveEnemies()
    {
        //_listOfAliveEnemies = GetComponentsInChildren<SingleEnemyManager>();
        
        _listOfAliveEnemies = FindObjectsByType<SingleEnemyManager>(FindObjectsSortMode.None);

    }

    public int GetAmountOfEnemiesAlive()
    {
        return _listOfAliveEnemies.Length;
    }
}
