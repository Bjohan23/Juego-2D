using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mobileControls;

    public bool fadeToBlack, fadeFromBlack;
    public Image blackScreen;
    public float fadeSpeed = 2f;

    //player reference
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Find player controller if not assigned
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }
    }

    public void DisableMobileControls()
    {
        if (mobileControls != null)
        {
            mobileControls.SetActive(false);
        }
    }

    public void EnableMobileControls()
    {
        if (mobileControls != null)
        {
            mobileControls.SetActive(true);
        }
    }

    private void Update()
    {
        UpdateFade();
    }

    private void UpdateFade()
    {
        if (fadeToBlack)
        {
            FadeToBlack();
        }
        else if (fadeFromBlack)
        {
            FadeFromBlack();
        }
    }

    private void FadeToBlack()
    {
        FadeScreen(1f);

        if (blackScreen.color.a >= 1f)
        {
            fadeToBlack = false;
        }
    }

    private void FadeFromBlack()
    {
        FadeScreen(0f);

        if (blackScreen.color.a <= 0f)
        {
            if (playerController != null && playerController.controlmode == Controls.mobile)
            {
                EnableMobileControls();
            }
            fadeFromBlack = false;
        }
    }

    private void FadeScreen(float targetAlpha)
    {
        Color currentColor = blackScreen.color;
        float newAlpha = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
        blackScreen.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    }

    // Mobile control methods to be called by UI buttons
    public void OnMobileJumpPressed()
    {
        if (playerController != null)
        {
            playerController.MobileJump();
        }
    }

    public void OnMobileMovePressed(float direction)
    {
        if (playerController != null)
        {
            playerController.MobileMove(direction);
        }
    }

    public void OnMobileDashPressed()
    {
        if (playerController != null)
        {
            playerController.MobileDash();
        }
    }

    public void OnMobileShootPressed()
    {
        if (playerController != null)
        {
            playerController.MobileShoot();
        }
    }

    // Utility methods
    public bool IsPlayerControllerValid()
    {
        return playerController != null;
    }

    public void AssignPlayerController(PlayerController controller)
    {
        playerController = controller;
    }
}