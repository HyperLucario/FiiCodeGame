using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed;
    public float scrollSpeed = 5.0f;
    // Update is called once per frame
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal") * speed;
        float zAxisValue = Input.GetAxis("Vertical") * speed;
        if (Camera.main != null)
        {
            Camera.main.transform.Translate(new Vector3(xAxisValue, zAxisValue, 0));

            // float scrlWheel = Input.GetAxis("Mouse ScrollWheel");

            if (Input.GetKey(KeyCode.Z))
            {
                float size = Camera.main.orthographicSize + scrollSpeed * Time.deltaTime;

                Camera.main.orthographicSize = Mathf.Clamp(size, 8.0f, 35.0f);
            }

            if (Input.GetKey(KeyCode.X))
            {
                float size = Camera.main.orthographicSize - scrollSpeed * Time.deltaTime;

                Camera.main.orthographicSize = Mathf.Clamp(size, 8.0f, 35.0f);
            }
        }
    }
}
