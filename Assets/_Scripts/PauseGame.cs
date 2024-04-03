using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private BoolPublisherSO OnPausePublisher;

    private CanvasGroup canvasGroup;
    [SerializeField] private bool isPausing;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!isPausing)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }
    }

    public void SetIsPausing(bool flag)
    {
        isPausing = flag;
    }

    public void Resume()
    {
        isPausing = false;
        TogglePauseUI(false);
    }

    public void Pause()
    {
        isPausing = true;
        TogglePauseUI(true);
    }

    public void TogglePauseUI(bool flag)
    {
        if (flag)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            OnPausePublisher.RaiseEvent(false);
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            OnPausePublisher.RaiseEvent(true);
        }
    }

    public void ResetPauseState()
    {
        isPausing = false;
    }
}
