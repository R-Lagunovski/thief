using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject prevScreen;

    private GameObject curScreen;
    // Start is called before the first frame update
    void Start()
    {
        prevScreen.SetActive(true);
        curScreen = prevScreen;

    }

    // Update is called once per frame
    public void updateScreen(GameObject state)
    {
        if (curScreen != null)
        {
            curScreen.SetActive(false);
            state.SetActive(true);
            curScreen = state;
        }
    }
}
