using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        instance = this;
    }
    public float radius;
    public float atkSpeed;
    IEnumerator auto_M_AttackCoroutine;
    IEnumerator auto_C_AttackCoroutine;


    [Header("UI")]
    public Button touchBtn;
    public Transform originTransform;
    [Header("스크립트")]
    [SerializeField] MissileManager missileManager;
    
    void Start()
    {
        touchBtn.onClick.RemoveAllListeners();
        touchBtn.onClick.AddListener(() => { M_Attack(); });
        Auto_M_Attack();
    }

    //몬스터 찾기 
    Transform GetTarget()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(originTransform.position, radius, Vector2.zero);
        List<RaycastHit2D> hitList = new List<RaycastHit2D>();
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.tag == "Enemy")
            {
                hitList.Add(hit[i]);
            }
        }

        if (hitList.Count == 0) return null;

        List<Transform> hitTransformList = new List<Transform>();
        List<float> hitDistance = new List<float>();
        for (int i = 0; i < hitList.Count; i++)
        {
            hitTransformList.Add(hitList[i].transform);
            hitDistance.Add(Vector2.Distance(originTransform.position, hitList[i].transform.position));
        }
        hitDistance.Sort();

        for (int i = 0; i < hitList.Count; i++)
        {
            if (hitDistance[0] == Vector2.Distance(originTransform.position, hitTransformList[i].position))
            {
                return hitTransformList[i];
            }
        }
        return null;
    }

    void Auto_M_Attack()
    {
        if (auto_C_AttackCoroutine != null) StopCoroutine(auto_C_AttackCoroutine);

        if (auto_M_AttackCoroutine != null) StopCoroutine(auto_M_AttackCoroutine);
        auto_M_AttackCoroutine = Auto_M_AttackCoroutine();
        StartCoroutine(auto_M_AttackCoroutine);
    }

    IEnumerator Auto_M_AttackCoroutine()
    {
        WaitForSeconds waitTime;
        while (true)
        {
            waitTime = new WaitForSeconds(atkSpeed);
            if (atkSpeed < 0.2f) atkSpeed = 0.2f;
            yield return waitTime;
            missileManager.Missile("10", GetTarget());
        }
    }

    public void M_Attack()
    {
        missileManager.Missile("10", GetTarget());
    }

    void Auto_C_Attack()
    {
        if (auto_M_AttackCoroutine != null) StopCoroutine(auto_M_AttackCoroutine);

        if (auto_C_AttackCoroutine != null) StopCoroutine(auto_C_AttackCoroutine);
        auto_C_AttackCoroutine = Auto_C_AttackCoroutine();
        StartCoroutine(auto_C_AttackCoroutine);

    }

    IEnumerator Auto_C_AttackCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(atkSpeed);

        while (GetTarget() == null)
        {
            yield return waitTime;
        }
        Transform target = GetTarget();

        float x = target.GetComponent<BoxCollider2D>().size.x;
        this.transform.DOMoveX(target.position.x - x, 0.1f);

        yield return new WaitForSeconds(0.1f);

        touchBtn.onClick.AddListener(() => { C_Attack(); });

        while (true)
        {
            waitTime = new WaitForSeconds(0.2f);
            yield return waitTime;
            if (target != null)
            {
                target.GetComponent<Enemy>().Hit("14");
            }
            else
            {
                Auto_C_Attack();
            }
        }
    }
    void C_Attack()
    {
        Transform target = GetTarget();
        if (target != null)
        {
            target.GetComponent<Enemy>().Hit("14");
        }
    }

    public void AngerModeStart()
    {
        touchBtn.onClick.RemoveAllListeners();
        Auto_C_Attack();
    }
    public void AngerModeStop()
    {
        touchBtn.onClick.RemoveAllListeners();

        if (auto_C_AttackCoroutine != null) StopCoroutine(auto_C_AttackCoroutine);

        this.transform.DOMoveX(originTransform.position.x, 0.1f).OnComplete(() => {
            touchBtn.onClick.AddListener(() => { M_Attack(); });
            Auto_M_Attack();
        });
    }
}
