using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] public PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private Vector3 playerPosition;
    private Vector3 lastCoinPosition; // Nueva variable para guardar la última posición de moneda
    private bool hasCollectedCoin = false; // Para verificar si ya recogió al menos una moneda

    // Lives system
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    // Level Complete
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text levelCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;

    // Game Over Panel
    [SerializeField] GameObject gameOverPanel;

    // Screen Shake
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.5f;
    private Vector3 originalCameraPosition;
    private bool isShaking = false;

    private int totalCoins = 0;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        playerPosition = playerController.transform.position;
        lastCoinPosition = playerPosition; // Inicializar con posición del jugador
        currentLives = maxLives;

        if (mainCamera == null)
            mainCamera = Camera.main;
        originalCameraPosition = mainCamera.transform.position;

        FindTotalPickups();
    }

    // Manual debug trigger (remove in production)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Press G to trigger manual game over
        {
            Debug.Log("Manual Game Over triggered!");
            Debug.Log($"Current lives before: {currentLives}");
            currentLives = 0;
            Debug.Log($"Current lives after setting to 0: {currentLives}");
            Debug.Log("Calling Death() function...");
            Death();
        }
    }

    public void IncrementCoinCount()
    {
        coinCount++;

        // Guardar la posición actual del jugador como checkpoint
        lastCoinPosition = playerController.transform.position;
        hasCollectedCoin = true;

        // Efecto visual opcional
        TriggerScreenShake(0.1f, 0.3f);

        UpdateGUI();

        Debug.Log($"Coin collected! New checkpoint at: {lastCoinPosition}");
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
    }

    public void Death()
    {
        Debug.Log("🔴 DEATH FUNCTION STARTED");
        Debug.Log($"isGameOver: {isGameOver}");

        if (isGameOver)
        {
            Debug.Log("Death called but already game over!");
            return;
        }

        Debug.Log($"Death function called. Current lives: {currentLives}");

        // Step 1: Screen shake
        Debug.Log("Step 1: Starting screen shake...");
        TriggerScreenShake();
        Debug.Log("Screen shake started");

        // Step 2: Disable UI
        Debug.Log("Step 2: Disabling UI...");
        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;
        Debug.Log("UI Manager updated");

        // Step 3: Disable player
        Debug.Log("Step 3: Disabling player...");
        playerController.gameObject.SetActive(false);
        Debug.Log("Player disabled");

        // Step 4: Lose life
        Debug.Log("Step 4: Losing life...");
        Debug.Log("Calling LoseLife()...");
        LoseLife();

        // Step 5: Set game over state if no lives left
        Debug.Log("Step 5: Setting game over state...");
        if (currentLives <= 0)
        {
            isGameOver = true;
            Debug.Log($"Game over state set to: {isGameOver}");
        }

        // Step 6: Check lives for next action
        Debug.Log("Step 6: Checking lives for next action...");
        if (currentLives <= 0)
        {
            Debug.Log("🔴 No lives left - showing game over!");
            Debug.Log("Starting GameOverCoroutine...");
            StartCoroutine(GameOverCoroutine());
        }
        else
        {
            Debug.Log($"Lives remaining: {currentLives} - respawning player!");
            StartCoroutine(DeathCoroutine());
        }

        Debug.Log("🔴 DEATH FUNCTION COMPLETED");
    }

    public void LoseLife()
    {
        Debug.Log($"LoseLife() called. Current lives: {currentLives}");
        currentLives--;
        Debug.Log($"Life lost! Current lives: {currentLives}");
    }

    public void AddLife()
    {
        currentLives++;
        Debug.Log($"Life added! Current lives: {currentLives}");
    }

    public void TriggerScreenShake()
    {
        if (!isShaking)
        {
            StartCoroutine(ScreenShakeCoroutine());
        }
    }

    public void TriggerScreenShake(float intensity, float duration)
    {
        if (!isShaking)
        {
            StartCoroutine(ScreenShakeCoroutine(intensity, duration));
        }
    }

    private IEnumerator ScreenShakeCoroutine()
    {
        yield return StartCoroutine(ScreenShakeCoroutine(shakeIntensity, shakeDuration));
    }

    private IEnumerator ScreenShakeCoroutine(float intensity, float duration)
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
        isShaking = false;
    }

    public IEnumerator GameOverCoroutine()
    {
        Debug.Log("🔴🔴🔴 GAME OVER COROUTINE STARTED");
        Debug.Log("Waiting 2 seconds...");
        yield return new WaitForSeconds(2f);
        Debug.Log("Wait completed");

        // Show game over panel or restart level
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            // If no game over panel, restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        // Decidir dónde respawnear: última moneda o posición inicial
        Vector3 respawnPosition = hasCollectedCoin ? lastCoinPosition : playerPosition;

        // Reset player position to checkpoint
        playerController.transform.position = respawnPosition;

        // Reactivate player
        playerController.gameObject.SetActive(true);

        // Wait for fade
        yield return new WaitForSeconds(1f);

        // Reset UI
        UIManager.instance.fadeFromBlack = true;

        Debug.Log($"Player respawned at: {respawnPosition}");
    }

    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsByType<pickup>(FindObjectsSortMode.None);

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }
        }
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        levelCompletePanelTitle.text = "LEVEL COMPLETE";
        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount.ToString() + " / " + totalCoins.ToString();
    }

    // Public methods to restart game or reset lives
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        isGameOver = false;
    }

    // Getters
    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public Vector3 GetLastCoinPosition()
    {
        return lastCoinPosition;
    }

    public bool HasCollectedAnyCoin()
    {
        return hasCollectedCoin;
    }

    // Método para resetear checkpoints (útil para testing)
    public void ResetCheckpoints()
    {
        lastCoinPosition = playerPosition;
        hasCollectedCoin = false;
        Debug.Log("Checkpoints reset to initial position");
    }
}