using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBehavior : MonoBehaviour
{
    public GameObject email;
    public string emailText;
    public GameObject password;
    public string passwordText;

    public void Login()
    {
        Debug.Log("Login button pressed");
        emailText = email.GetComponent<InputField>().text;
        passwordText = password.GetComponent<InputField>().text;
        Debug.Log("Email: " + emailText + " Password: " + passwordText);
    }
}
