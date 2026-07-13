using UnityEngine;

public class ButtonShotActivatedGate : MonoBehaviour
{
    [SerializeField] GameObject _gate;
    private bool isOpen = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GateOpen()
    {
        isOpen = true;
        Destroy(_gate);
    }
}
