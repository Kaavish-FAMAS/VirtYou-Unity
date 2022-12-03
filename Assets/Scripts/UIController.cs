using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button signupButton;
    public Label check;
    public TextField email;
    public Button gotosignin;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        signupButton = root.Q<Button>("signupButton");
        //check = root.Q<Label>("check");
        email = root.Q<TextField>("email");
        gotosignin = root.Q<Button>("gotosignin");

        signupButton.clicked += SignUpButtonPressed;
        gotosignin.clicked += GoToSignInButtonPressed;
    }

    void SignUpButtonPressed(){
        SceneManager.LoadScene("game");
    }

    void GoToSignInButtonPressed(){
        SceneManager.LoadScene("Login");
    }

    // void MessageButtonPressed(){
    //     check.text = email.text;
    //     check.style.display = DisplayStyle.Flex;
    // }
}
