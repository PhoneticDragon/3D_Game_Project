using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public Transform player;
    public Transform currentCheckpoint;

    [Header("Game Settings")]
    public int lives = 3;
    public float timer = 300f;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;

    private bool paused = false;
    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0);

        UpdateUI();

        if (timer <= 0)
        {
            LoseGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = $"Time: {minutes:00}:{seconds:00}";
        livesText.text = $"Lives: {lives}";
    }

    public void DamagePlayer()
    {
        if (gameEnded) return;

        lives--;

        if (lives <= 0)
        {
            LoseGame();
        }
        else
        {
            RespawnPlayer();
        }

        UpdateUI();
    }

    void RespawnPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
            player.position = currentCheckpoint.position;
            cc.enabled = true;
        }
        else
        {
            player.position = currentCheckpoint.position;
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        if (currentCheckpoint == checkpoint)
            return; // already set, do nothing

        currentCheckpoint = checkpoint;

        AudioManager.Instance.PlayCheckpoint();
    }

    public void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;
        UnlockCursor();
    }

    void LoseGame()
    {
        gameEnded = true;
        losePanel.SetActive(true);
        Time.timeScale = 0;
        UnlockCursor();
    }

    public void TogglePause()
    {
        paused = !paused;

        pausePanel.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
            UnlockCursor();
        }
        else
        {
            Time.timeScale = 1;
            LockCursor();
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}