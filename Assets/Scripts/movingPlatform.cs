using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float speed;
    Vector2 targetPos;

    void Start()
    {
        targetPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, startPos.position) < 0.1f)
            targetPos = endPos.position;

        if (Vector2.Distance(transform.position, endPos.position) < 0.1f)
            targetPos = startPos.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
            
            collision.transform.localScale = Vector3.one;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            collision.transform.localScale = Vector3.one;
        }
    }
}
