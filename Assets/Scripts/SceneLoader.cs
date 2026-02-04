using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Esta es la forma de llamar la funcion ChangeScene a traves de codigo
    void Test()
    {
        ChangeScene("Main Menu");
    }
}
