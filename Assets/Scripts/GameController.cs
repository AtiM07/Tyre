using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource _audioSource; // звук выстрела
    private AudioClip _clip; // звукова€ дорожка стрельбы из лука
    private static int _score = 0; //счет
    private static bool _isShooting; // можно ли стрел€ть
    private const float Period = 0.5f; // период стрельбы
    private float _timerShooting = Period; //таймер выстрела
    private static float _timerGame = 0f; // длительность игры
    public static void Miss() => _score = _score > 0 ? _score - 1 : 0; // изменение счетчика очков при пропуске мишени
    public static int Score => _score;
    public static float Timer => _timerGame;
    public static bool IsOver => _timerGame <= 0;

    private void Start()
    {
        Invoke(nameof(SetParameter), 4f); //инициализируем игру через врем€
        _audioSource = GetComponent<AudioSource>();
        _clip = _audioSource.clip;
    }

    private void SetParameter() //задаем параметры начала игры
    {
        _score = 0;
        _timerGame = 30f;
        _isShooting = true;
    }

    private void Update()
    {
        if (IsOver) return;

        Game();
    }

    private void Game()
    {
        _timerGame -= Time.deltaTime;
        _timerShooting -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) // проверка на клик левой кнопкой мыши
        {
            if (_isShooting)
            {
                _timerShooting = Period;
                _isShooting = false; //запрещаем стрел€ть по таймеру
                _audioSource.PlayOneShot(_clip); //запускаем звук выстрела
                Shoot(); //провер€ем, в кого выстрелили
            }
        }

        CheckTimerShooting();
    }

    private void CheckTimerShooting() //проверка на исчерпани€ времени таймера запрета выстрела
    {
        if (_timerShooting <= 0)
        {
            _isShooting = true;
        }
    }

    private void Shoot()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out var hit)) //если луч кого-то коснулс€
        {
            if (hit.collider.gameObject.name == "Enemy") // если объект, которого луч коснулс€, - наш враг
            {
                var coin = hit.collider.gameObject.GetComponent<Enemy>().Coin;
                _score += coin; // засчитываем очки
                Destroy(hit.collider.gameObject); //уничтожаем врага
            }
        }
    }
}