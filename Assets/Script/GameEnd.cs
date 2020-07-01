using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public GameObject[] fadeOutObj;
    float fadeSpeed = 1f;
    private void OnEnable()
    {
        for (int i = 0; i < fadeOutObj.Length; i++)
        {
            if (fadeOutObj[i].GetComponent<Image>() != null)
            {
                fadeOutObj[i].GetComponent<Image>().DOFade(0, 0);
                fadeOutObj[i].GetComponent<Image>().DOFade(1, fadeSpeed);
            }
            if (fadeOutObj[i].GetComponent<Text>() != null)
            {
                fadeOutObj[i].GetComponent<Text>().DOFade(0, 0);
                fadeOutObj[i].GetComponent<Text>().DOFade(1, fadeSpeed);
            }
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }
}
