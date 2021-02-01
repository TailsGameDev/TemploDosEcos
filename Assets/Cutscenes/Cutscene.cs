using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName = null;

    private IEnumerator Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        while (!videoPlayer.isPlaying)
        {
            yield return null;
        }
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
