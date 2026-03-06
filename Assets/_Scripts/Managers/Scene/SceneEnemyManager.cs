using UnityEngine;

public class SceneEnemyManager : MonoBehaviour
{
    
    private SingleEnemyManager[] _listOfAliveEnemies;

    void Start()
    {
        Debug.Log(GetComponentsInChildren<SingleEnemyManager>());
        _listOfAliveEnemies = GetComponentsInChildren<SingleEnemyManager>();
    }

    // Update is called once per frame
    void Update() 
    {
        _listOfAliveEnemies = GetComponentsInChildren<SingleEnemyManager>();
        string list = "";
        foreach(SingleEnemyManager sem in _listOfAliveEnemies)
        {
            list += sem.name + ", ";
        }
        //Debug.Log(list);
    }

    public void UpdateListOfAliveEnemies()
    {
        _listOfAliveEnemies = GetComponentsInChildren<SingleEnemyManager>();
    }

    public int GetAmountOfEnemiesAlive()
    {
        return _listOfAliveEnemies.Length;
    }
}
