using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerLogin : MonoBehaviour
{
    public Button signinButton;
    public Button gotoregister;
    public TextField email;
    public TextField password;
    public Label errorLabel;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        signinButton = root.Q<Button>("signin");
        email = root.Q<TextField>("email");
        gotoregister = root.Q<Button>("gotoregister");
        password = root.Q<TextField>("password");
        errorLabel = root.Q<Label>("errorLabel");

        signinButton.clicked += LoginButtonPressed;
        gotoregister.clicked += GoToRegisterButtonPressed;
    }

    void LoginButtonPressed(){
        if ((email.text == "Aliza") && (password.text) == "1234"){
            SceneManager.LoadScene("Home");
        }
        else
        {
            errorLabel.text = "Incorrect email or password";
            errorLabel.style.display = DisplayStyle.Flex;
        }
        
    }

    void GoToRegisterButtonPressed(){
        SceneManager.LoadScene("Register");
    }

    // void MessageButtonPressed(){
    //     check.text = email.text;
    //     check.style.display = DisplayStyle.Flex;
    // }
}
