using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    public void OpenScene(int index)
    {
        SceneManager.LoadScene("Problem" + index);
    }
}
