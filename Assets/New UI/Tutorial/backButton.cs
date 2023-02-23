using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class backButton : MonoBehaviour
{   
    public Button back;
    public void clicked()
    {
        SceneManager.LoadScene("MenuModified");
    }
}
