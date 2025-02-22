using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGamne()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
