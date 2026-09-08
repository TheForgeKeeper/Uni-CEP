using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneIndexed : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
        }
}
