using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAnimation : MonoBehaviour {
    private Animator _animator;
    private string takeDamageState = "TakeDamage";  //must be followed by 1 or 2
    private string idleState = "Idle";
    private string fallState = "Fall";
    private string victoryState = "Victory";
    private GameObject _ownBase;
    private bool _showIntroHints = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _ownBase = transform.parent.gameObject;
    }

    private void Start()
    {
        EventManager.StartListening("LevelComplete", Victory);
        EventManager.StartListening("LevelLost", Loss);
        EventManager.Damage += TakeDamage;

        if (_showIntroHints)
        {
            HintManager.Instance.CreateHint(1, Vector3.zero);
            _showIntroHints = false;
        }
    }



    private void OnDestroy()
    {
        EventManager.StopListening("LevelComplete", Victory);
        EventManager.StopListening("LevelLost", Loss);
        EventManager.Damage -= TakeDamage;
    }



    public void TakeDamage(GameObject dealer, List<GameObject> receivers)
    {
        if (receivers.Contains(_ownBase))
        {
            int rand = Random.Range((int)1, (int)3);
            _animator.Play(takeDamageState + rand.ToString());
        }
    }



    public void Victory()
    {
        _animator.Play(victoryState);

    }



    public void Loss()
    {
        _animator.Play(fallState);
    }



    public void Idle()
    {
        _animator.Play(idleState);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int rand = Random.Range((int)1, (int)3);
            _animator.Play(takeDamageState + rand.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _animator.Play(fallState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _animator.Play(idleState);

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _animator.Play(victoryState);
        }

    }
}
