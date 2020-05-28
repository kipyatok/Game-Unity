using UnityEngine;

/// <summary>
/// Вторая специальная атака. Объект движется в центр. Запускает лазеры и начинает их крутить.
/// </summary>
class BlastAttackBoss: MonoBehaviour
{
    public GameObject blactBullet; // Объект лазера
    
    GameObject setBlastBullet; //Группа для хранения лазеров
    GameObject[] _blast; // Массив 2 лазеров

    bool create; // Переменна для определения создались лазеры или нет
    Vector3 endBlast; //Размеры конечного расширения
    Vector3 startBlast; // Начальные размеры
    Vector3 centr; // Для переноса объекта который будет вращять лазеры в центр экрана
    float rotate; // Скорость вращения
    float startRotate=3f;

    float timeSpeedUp = 0.03f; //Ускорение

    float timeAttack = 50f; // Время вращения

    public void Start()
    {
        centr = Vector3.zero;
        rotate = startRotate;
        setBlastBullet = new GameObject();
        setBlastBullet.name = "BlastBullet";
        //setBlastBullet.transform.position = 2f;
        setBlastBullet.transform.SetParent(gameObject.transform);
        setBlastBullet.transform.localPosition = Vector3.zero;
        _blast = new GameObject[2];
        endBlast = new Vector3(7f, 1f, 0);
        startBlast = new Vector3(0.2f, 1f, 0);
        _blast[0] = Instantiate(blactBullet, Vector3.zero, Quaternion.Euler(0, 0, 0));
        _blast[1] = Instantiate(blactBullet, Vector3.zero, Quaternion.Euler(0, 0, 270));

        for (int i = 0; i < _blast.Length; i++)
        {
            _blast[i].transform.SetParent(setBlastBullet.transform);
            _blast[i].transform.localPosition = Vector3.zero;

        }
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, centr) > 0.05f) 
        {
            CenterMovement();
        }
        else
        {
            for (int i = 0; i < _blast.Length; i++)
            {
                //Создание лазеров на всю длину экрана
                if ((_blast[i].transform.localScale.x < endBlast.x - 1) && !create)
                {
                    _blast[i].transform.localScale = Vector3.Lerp(_blast[i].transform.localScale, endBlast, 1f * Time.deltaTime);
                }
                else
                {
                    //Если создались
                    create = true;
                    //Пока работает таймер лазеры вращаются снаращеваемой скоростью
                    if (!IsTimer(ref timeAttack))
                    {
                        setBlastBullet.transform.Rotate(new Vector3(0, 0, rotate) * Time.deltaTime);
                        rotate += timeSpeedUp;

                    }
                    else
                    {
                        //Уменьшаем до стартового значения
                        _blast[i].transform.localScale = Vector3.Lerp(_blast[i].transform.localScale, startBlast, 2f * Time.deltaTime);
                        //Если уменьшились то отключить компанент
                        if (_blast[i].transform.localScale.x < startBlast.x + 0.2f)
                        {
                            gameObject.GetComponent<TwoAttackBosss>().enabled = false;
                            timeAttack = 40f;
                            rotate = startRotate;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Движение объекта в центр экрана
    /// </summary>
    private void CenterMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, centr, 5f * Time.deltaTime);
        SetRotation(Random.Range(0, 1));
        create = false;
    }
    /// <summary>
    /// Выбор стороны вращения лазеров.
    /// Если 1 то вправо
    /// Если 0 то влево
    /// </summary>
    /// <param name="index">Параметр вращения</param>
    private void SetRotation(int index)
    {
        //rotate = 3f;
        _blast[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        _blast[1].transform.rotation = Quaternion.Euler(0, 0, 270);
        if (index == 0)
        {
            rotate *= -1;
            timeSpeedUp *= -1;
        }

    }
    /// <summary>
    /// Таймер вращения
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool IsTimer(ref float time)
    {
        time -= Time.deltaTime;
        return (time<0);
    }


}
