using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public GameObject menuScreen; // Đối tượng chứa màn hình menu

    private bool isPaused = false; // Biến kiểm tra trạng thái tạm dừng

    void Update()
    {
        // Kiểm tra nếu người chơi ấn phím Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Hàm để tạm dừng trò chơi và hiển thị menu
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Dừng thời gian trò chơi
        menuScreen.SetActive(true); // Hiển thị menu screen
        Cursor.visible = true; // Hiển thị con trỏ chuột
        Cursor.lockState = CursorLockMode.None; // Mở khóa con trỏ chuột
    }

    // Hàm để tiếp tục trò chơi và ẩn menu
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Tiếp tục thời gian trò chơi
        menuScreen.SetActive(false); // Ẩn menu screen
        Cursor.visible = false; // Ẩn con trỏ chuột
        Cursor.lockState = CursorLockMode.Locked; // Khóa con trỏ chuột
    }

    // Hàm để trở về Main Menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian trở lại bình thường
        SceneManager.LoadScene("GUI"); // Tải scene GUI
    }

    // Hàm để thoát trò chơi
    public void QuitGame()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian trở lại bình thường
        Application.Quit(); // Thoát ứng dụng
    }
}
