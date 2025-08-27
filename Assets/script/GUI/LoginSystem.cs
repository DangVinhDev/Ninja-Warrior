using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoginSystem : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button logoutButton;
    public Text messageText;

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/userData.txt";
        loginButton.onClick.AddListener(HandleLogin);
        logoutButton.onClick.AddListener(HandleLogout);
        LoadUserData();
    }

    void HandleLogin()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (IsValidLogin(username, password))
        {
            messageText.text = "Login successful!";
            SaveUserData(username, password);
        }
        else
        {
            messageText.text = "Invalid username or password.";
        }
    }

    void HandleLogout()
    {
        messageText.text = "Logged out.";
        DeleteUserData();
    }

    bool IsValidLogin(string username, string password)
    {
        // Add your login validation logic here
        // This example just checks if username and password are not empty
        return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }

    void SaveUserData(string username, string password)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(username);
            writer.WriteLine(password);
        }
    }

    void LoadUserData()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string username = reader.ReadLine();
                string password = reader.ReadLine();

                usernameInput.text = username;
                passwordInput.text = password;
                messageText.text = "Loaded saved user data.";
            }
        }
    }

    void DeleteUserData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
