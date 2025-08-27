using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RegisterSystem : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
    public Button registerButton;
    public Text messageText;

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/userData.txt";
        registerButton.onClick.AddListener(HandleRegister);
    }

    void HandleRegister()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (password != confirmPassword)
        {
            messageText.text = "Passwords do not match.";
            return;
        }

        if (IsUsernameTaken(username))
        {
            messageText.text = "Username already taken.";
            return;
        }

        if (RegisterUser(username, password))
        {
            messageText.text = "Registration successful!";
        }
        else
        {
            messageText.text = "Registration failed.";
        }
    }

    bool IsUsernameTaken(string username)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Split(',')[0] == username)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool RegisterUser(string username, string password)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(username + "," + password);
            }
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to register user: " + ex.Message);
            return false;
        }
    }
}
