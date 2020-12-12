using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrepab;
    public Camera theCam;
    private void Start()
    {
        StartCoroutine(GhostCoroutine());
    }
    IEnumerator GhostCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(3);
        while (true)
        {
            yield return waitTime;
            if(this.transform.childCount < 6)
            {
                float x = Random.Range(-5, 5);
                float y = Random.Range(0, 3);
                GameObject ghostClone = Instantiate(ghostPrepab, new Vector2(x, y), Quaternion.identity, this.transform);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 origin = theCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 100);
            if (hit && hit.transform.tag == "Ghost")
            {
                hit.transform.GetComponent<Ghost>().CatchGhost();
            }
        }
    }
}
