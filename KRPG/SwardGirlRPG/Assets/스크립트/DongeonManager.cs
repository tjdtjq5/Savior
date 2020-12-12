using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongeonManager : MonoBehaviour
{
    public Transform playerCharacter;
    public Transform parentsTransform;
    IEnumerator sommonCoroutine;
    [Header("EnemyPrepab")]
    [SerializeField] GameObject grave;
    [SerializeField] GameObject slime;

    private void Start()
    {
        GraveSetting();
    }
    public void Initialize()
    {
        for (int i = 0; i < parentsTransform.childCount; i++)
        {
            parentsTransform.GetChild(i).GetComponent<Enemy>().Destroy();
        }
        if (sommonCoroutine != null) StopCoroutine(sommonCoroutine);
    }
    [ContextMenu("무덤")]
    public void GraveSetting()
    {
        Initialize();
        GameObject enemyClone = Instantiate(grave, new Vector2(6.13f, -1.263f), Quaternion.identity, parentsTransform);
    }
    [ContextMenu("슬라임")]
    public void StageTestSetting()
    {
        Initialize();

        sommonCoroutine = SommonCoroutine(slime, new Vector2(8, -1), 20);
        StartCoroutine(sommonCoroutine);
    }
    IEnumerator SommonCoroutine(GameObject enemyPrepab, Vector2 position, int num)
    {
        WaitForSeconds waitTime = new WaitForSeconds(.8f);
        for (int i = 0; i < num; i++)
        {
            GameObject enemyClone = Instantiate(enemyPrepab, position, Quaternion.identity, parentsTransform);
            enemyClone.GetComponent<Enemy>().TargetSetting(playerCharacter);
            yield return waitTime;
        }
    }
}
