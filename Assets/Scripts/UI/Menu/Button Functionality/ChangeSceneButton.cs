using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour {
    public void OnClick(string currentScene) {
        StartCoroutine(ChangeSceneWithDelay(currentScene));
    }

    private IEnumerator ChangeSceneWithDelay(string sceneName) {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}