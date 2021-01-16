using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Loader Components")]
    [SerializeField] private Animator sceneLoaderAnimator;

    private AsyncOperation loadingSyncOperation;

    public static SceneLoader Instance { get { return instance; } }
    private static SceneLoader instance;

    private static bool isFadeEndAnimationNeedToPlay = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(isFadeEndAnimationNeedToPlay == true)
        {
            //Debug.Log("isFadeEndAnimationNeedToPlay == true");
            sceneLoaderAnimator.SetTrigger(SceneLoaderAnimationTriggers.FadeEnd.ToString());
        }
    }

    private void LoadScene(string sceneName)
    {
        sceneLoaderAnimator.SetTrigger(SceneLoaderAnimationTriggers.FadeStart.ToString());

        loadingSyncOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingSyncOperation.allowSceneActivation = false;
    }

    public void OnAnimationOver()
    {
        //This Method Invokes From FadeStart Animation

        isFadeEndAnimationNeedToPlay = true;
        loadingSyncOperation.allowSceneActivation = true;
    }

    public static void LoadSceneByName(string sceneName)
    {
        Instance.LoadScene(sceneName);
    }

    public static void RestartThisScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Instance.LoadScene(scene.name);
    }
}

public enum SceneLoaderAnimationTriggers
{
    FadeStart,
    FadeEnd
}