using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class signin : MonoBehaviour
{
    public void toSignin()
    {
        SceneManager.LoadScene("LoginModified");
    }
}
