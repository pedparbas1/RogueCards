using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public string targetSceneName;
   public void ChangeScene()
   {
        Debug.Log($"Changing to scene: {targetSceneName}");
        SceneManager.LoadSceneAsync(targetSceneName);
   }
}
