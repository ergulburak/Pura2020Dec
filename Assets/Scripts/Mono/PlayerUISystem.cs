using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUISystem : MonoBehaviour
{
    public GameObject PressE;
    public Gradient gradient;
    public Image fill;
    public Button retry;
    public Button finish;

    private void Start()
    {
        SetMaxHealth();
        retry.onClick.AddListener(Replay);
        finish.onClick.AddListener(Finish);
    }

    void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Finish()
    {
        SceneManager.LoadScene(0);
    }

    public void FinishOpen()
    {
        finish.transform.parent.gameObject.SetActive(true);
            
    }

    void Update()
    {
        GameObject.Find("SliderNoice").GetComponent<Slider>().value =
            GameObject.FindObjectOfType<PlayerControllerSystem>().noiceLevel;
        GameObject.Find("SliderCamouflage").GetComponent<Slider>().value =
            GameObject.FindObjectOfType<PlayerControllerSystem>().camouflage;
        GameObject.Find("SliderHealth").GetComponent<Slider>().value =
            GameObject.Find("Player").GetComponent<DamageSystem>().currentHealth;
        ;
        fill.color = gradient.Evaluate(GameObject.Find("SliderHealth").GetComponent<Slider>().normalizedValue);
        if (GameObject.FindObjectOfType<PlayerControllerSystem>().IsCamouflageble())
        {
            PressE.SetActive(true);
        }
        else
        {
            PressE.SetActive(false);
        }
        if (GameObject.FindObjectOfType<PlayerControllerSystem>().playerState==PlayerControllerSystem.PlayerState.Dead)
        {
            retry.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            retry.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SetMaxHealth()
    {
        GameObject.Find("SliderHealth").GetComponent<Slider>().maxValue =
            GameObject.Find("Player").GetComponent<DamageSystem>().health;
        GameObject.Find("SliderHealth").GetComponent<Slider>().value =
            GameObject.Find("Player").GetComponent<DamageSystem>().currentHealth;

        fill.color = gradient.Evaluate(1f);
    }
}