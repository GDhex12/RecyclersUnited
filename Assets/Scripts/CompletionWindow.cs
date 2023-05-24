using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletionWindow : MonoBehaviour
{
    public void ResetSceneData()
    {
        SaveSystem.RemoveSceneData();
        GetComponent<SceneLoader>().LoadScene(SceneManager.GetActiveScene().name);
    }
}
