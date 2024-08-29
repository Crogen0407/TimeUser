using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string sceneName)
	{
		StartCoroutine(CoroutineSceneChange(sceneName));
	}

	public IEnumerator CoroutineSceneChange(string sceneName)
	{
		ScreenFadeManager.Instance.FadeScreen(false);
		yield return new WaitForSecondsRealtime(ScreenFadeManager.Instance.GetFadeDuration());
		SceneManager.LoadScene(sceneName);
	}
}
