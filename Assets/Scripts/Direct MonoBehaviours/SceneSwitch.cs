using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void switchTo(string name)
    {
        SceneManager.LoadScene(name);
    }
}
