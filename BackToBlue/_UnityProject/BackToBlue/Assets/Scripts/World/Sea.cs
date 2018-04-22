using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{

    public static Sea instance = null;

    Renderer seaRend;
    Color s_color;
    Color s_target;

    //The rate at which the color will change per frame
    float changeRate = 0.02f;

    // Use this for initialization
    void Start()
    {
        if (instance != this)
        {
            instance = this;
        }

        seaRend = gameObject.GetComponent<Renderer>();
        s_color = seaRend.material.color;
        s_target = s_color;
    }

    // Update is called once per frame
    void Update()
    {

        if (s_color != s_target)
        {
            //Red
            updateColor(0);
            //Green
            updateColor(1);
            //Blue
            updateColor(2);
        }

        seaRend.material.color = s_color;
    }

    void updateColor(int index)
    {
        if (s_color[index] > s_target[index])
        {
            s_color[index] -= changeRate;
        }
        else if (s_color[index] < s_target[index])
        {
            s_color[index] += changeRate;
        }

    }

    //Set the target to a specific color
    public void setTarget(Color newColor)
    {
        s_target = newColor;
    }

    //Adds .2f blue
    public void addBlue()
    {
        s_target.b += 0.2f;
    }
}
