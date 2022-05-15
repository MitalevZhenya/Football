using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviourSingleton<SceneController>
{
    [SerializeField] private string[] playebleScenes;
    private int currSceneIndex;
    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(playebleScenes[0]);
    }
    public void LoadNextScene()
    {
        currSceneIndex = (currSceneIndex + 1) % playebleScenes.Length;
        SceneManager.LoadScene(playebleScenes[currSceneIndex]);
    }
    public void LoadPrevScene()
    {
        currSceneIndex--;
        if (currSceneIndex <= 0)
            currSceneIndex = playebleScenes.Length - 1;
        SceneManager.LoadScene(playebleScenes[currSceneIndex]);
    }
}