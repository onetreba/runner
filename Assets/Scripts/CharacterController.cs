using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Для работы со сценами.

public class CharacterController : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    private int _currentJumpsCount;
    public int Life = 3; //Количество жизней.


    public LayerMask GroundLayer;
    public GameObject GroundPoint;
    public GameObject FrontPoint;
    public float JumpPower;
    public float Speed;
    public int JumpsAvailable;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        bool isGrounded = Physics2D.OverlapCircle(GroundPoint.transform.position, 0.1f, GroundLayer);

        if (isGrounded && _rigidbody.velocity.y < 0)
        {
            _currentJumpsCount = 0;
        }

        if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
        {
            if(_currentJumpsCount < JumpsAvailable)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpPower);
                //_rigidbody.AddForce(new Vector2(0, JumpPower));
                _currentJumpsCount++;
            }
        }

        _rigidbody.velocity = new Vector2(Speed, _rigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D damage)
    {
        if (damage.gameObject.tag == "Enemy") //Проверка столкновения с препятствием с тэгом "Enemy".
        {
            Life--; //Отнимает 1ну жизнь при столкновеннии с tag "Enemy".
        }

        if (Life == 0)
        {
            Invoke("ReloadLevel", 1); //Вызов перезагрузки сцены, когда показатель жизни = 0.
        }
    }

    void OnTriggerEnter2D(Collider2D damage)
    {
        if (damage.gameObject.tag == "Heal") //Проверка столкновения с препятствием с тэгом "Heal".
        {
            Life++; //Добавлят 1 жизнь при столкновении.
            Destroy(damage.gameObject); //Удаляет объект после столкновения.
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 30), "Life =" + Life); //Индикатор жизни (Вшитый от Unity).
    }

    void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel); //Метод перезагрузки сцены (Уровня).
    }
}
