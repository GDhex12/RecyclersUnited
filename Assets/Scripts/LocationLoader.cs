using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationLoader : MonoBehaviour
{
    public static LocationLoader Instance;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] string transitionTrigger;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Loads scene without transition using build index
    /// </summary>
    /// <param name="sceneId">build index</param>
    public void LoadScene_Simple(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    /// <summary>
    /// Loads scene without transition using scene name
    /// </summary>
    /// <param name="sceneName">scene name</param>
    public void LoadScene_Simple(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads scene with transition using build index
    /// </summary>
    /// <param name="sceneId">build index</param>
    public void LoadScene_Transition(int sceneId)
    {
        StartCoroutine(LoadAfterTransition(sceneId));
    }

    /// <summary>
    /// Loads scene with transition using scene name
    /// </summary>
    /// <param name="sceneName">scene name</param>
    public void LoadScene_Transition(string sceneName)
    {
        StartCoroutine(LoadAfterTransition(sceneName));
    }

    IEnumerator LoadAfterTransition(int sceneId)
    {
        transitionAnimator.SetTrigger(transitionTrigger);
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);
        LoadScene_Simple(sceneId);
    }
    
    IEnumerator LoadAfterTransition(string sceneName)
    {
        transitionAnimator.SetTrigger(transitionTrigger);
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);
        LoadScene_Simple(sceneName);
    }
}
