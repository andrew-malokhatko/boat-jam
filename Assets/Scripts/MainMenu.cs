using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGamne()
	{
		SceneManager.LoadScene("Scene_expanding");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
