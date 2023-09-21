using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioSource _audioSource; // ���� ��������
    private AudioClip _clip; // �������� ������� �������� �� ����
    private static int _score = 0; //����
    private static bool _isShooting; // ����� �� ��������
    private const float Period = 0.5f; // ������ ��������
    private float _timerShooting = Period; //������ ��������
    private static float _timerGame = 0f; // ������������ ����
    public static void Miss() => _score = _score > 0 ? _score - 1 : 0; // ��������� �������� ����� ��� �������� ������
    public static int Score => _score;
    public static float Timer => _timerGame;
    public static bool IsOver => _timerGame <= 0;

    private void Start()
    {
        Invoke(nameof(SetParameter), 4f); //�������������� ���� ����� �����
        _audioSource = GetComponent<AudioSource>();
        _clip = _audioSource.clip;
    }

    private void SetParameter() //������ ��������� ������ ����
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

        if (Input.GetMouseButtonDown(0)) // �������� �� ���� ����� ������� ����
        {
            if (_isShooting)
            {
                _timerShooting = Period;
                _isShooting = false; //��������� �������� �� �������
                _audioSource.PlayOneShot(_clip); //��������� ���� ��������
                Shoot(); //���������, � ���� ����������
            }
        }

        CheckTimerShooting();
    }

    private void CheckTimerShooting() //�������� �� ���������� ������� ������� ������� ��������
    {
        if (_timerShooting <= 0)
        {
            _isShooting = true;
        }
    }

    private void Shoot()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out var hit)) //���� ��� ����-�� ��������
        {
            if (hit.collider.gameObject.name == "Enemy") // ���� ������, �������� ��� ��������, - ��� ����
            {
                var coin = hit.collider.gameObject.GetComponent<Enemy>().Coin;
                _score += coin; // ����������� ����
                Destroy(hit.collider.gameObject); //���������� �����
            }
        }
    }
}