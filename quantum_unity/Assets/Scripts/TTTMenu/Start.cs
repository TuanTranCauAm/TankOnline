using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quantum;

public class Start: MonoBehaviour
{
    [SerializeField] private InputField createName;
    [SerializeField] private InputField joinName;
    public void play()
    {
        SceneManager.LoadScene("Game");
    }
}
