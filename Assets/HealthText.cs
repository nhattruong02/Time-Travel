using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timetoLive = 0.5f;
    public float floatSpeed = 500;
    public Vector3 floatDirection = new Vector3 (0f, 1f, 0f);
    RectTransform rectTransform;
    public TextMeshProUGUI textMesh;
    float timeElapsed = 0.0f;
    Color startingColor;
    private void Start()
    {
        startingColor = textMesh.color;
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;
        textMesh.color = new Color(startingColor.r,startingColor.g,startingColor.b, 1 - timeElapsed / timetoLive);
        if(timeElapsed > timetoLive)
        {
            Destroy(gameObject);
        }
    }
}
