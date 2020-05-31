using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CarMove : MonoBehaviour
{
    Rigidbody fizik;
    float speed =60.0f, turn_speed = 60.0f;
    public TextMeshProUGUI scoreText;
    int score= 0;
    bool turn_right =false, turn_left = false;


    void Start()
    {
        fizik = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        scoreText.text = score.ToString();
        Finish();
    }

    public void Touch_Event()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary)
            {
                StartCoroutine(OnMouseDown());
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Score_Case();
        if(score %3 != 0)
        {
            StopCoroutine(Hizlandirma());
            speed = 60.0f;
        }   
        if (other.gameObject.tag == "Right")
        {
            turn_right = true;
        }
        if (other.gameObject.tag == "Left")
        {
            turn_left = true;          
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Touch_Event();
    }

    public void OnTriggerExit(Collider other)
    {
        StopCoroutine(OnMouseDown());
        StartCoroutine(Hizlandirma());
        if (other.gameObject.tag == "Right")
        {
            turn_right = false;
        }
        if (other.gameObject.tag == "Left")
        {
            turn_left = false;
        }
    }

    IEnumerator OnMouseDown()
    {
        yield return new WaitForSeconds(0.0f);
        if (turn_right == true)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * turn_speed, 0));
        }
        if (turn_left == true)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * turn_speed*-1, 0));
        } 
    }

    public void Score_Case()
    {
        score++;
    }

    IEnumerator Hizlandirma()
    {
        yield return new WaitForSeconds(0.0f);
        if (score % 3 == 0)
        {
            speed = speed * 2;
        }
    }

    public void Finish()
    {
        if (score == 6)
        {
            Time.timeScale = 0;
            speed = 0;
            SceneManager.LoadScene("FinishScene");
        }
    }
}
