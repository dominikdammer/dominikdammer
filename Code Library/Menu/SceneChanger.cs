using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //simple scene changer for triggers and UI Buttons

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
