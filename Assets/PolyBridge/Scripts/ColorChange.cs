using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Renderer rendererer;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rendererer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f) { 
        rendererer.material.color = new Color(Random.value, Random.value, Random.value);
            timer = 0f;
            
        }
        /*
        else if (timer > 4f)
        {
        } else
        {
            renderer.material.color = Color.white;
        }
        */

    }
}
