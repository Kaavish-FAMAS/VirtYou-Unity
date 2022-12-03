using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIControllerHome : MonoBehaviour
{
    public Button VRWithData;
    public Button VRWithoutData;
    public Button Tutorial;
    public Button About;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        VRWithData = root.Q<Button>("VRWithData");
        VRWithoutData = root.Q<Button>("VRWithoutData");
        Tutorial = root.Q<Button>("Tutorial");
        About = root.Q<Button>("About");

        // on press
        VRWithData.clicked += VRWithDataPressed;
        VRWithoutData.clicked += VRWithoutDataPressed;
        Tutorial.clicked += TutorialPressed;
        About.clicked += AboutPressed;
    }

    void VRWithDataPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void VRWithoutDataPressed()
    {
        SceneManager.LoadScene("game");
    }

    void TutorialPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void AboutPressed()
    {
        SceneManager.LoadScene("About");
    }
}
