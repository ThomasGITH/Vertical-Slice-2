using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void switchTo(string name)
    {
        EditorSceneManager.LoadScene(name);
    }
}
