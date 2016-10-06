using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainController : MonoBehaviour
    {

        public List<GameObject> ItemPrefabList; // Префабы итемов
        public List<GameObject> LifeGameObjects; // Гейм обьекты жизней
        public Transform ItemConteinerTransform; // Перент для итемов
        public GameObject GameOverGameObject; // Гейм Овер окно
        public Text GameOverText; // Текст счёта в гейм овер окне
        public Text ScoreTxt; // Текст счёта общий
        private float _timePass = 0; // Прошло времени с посленего созданного обьекта

        // Use this for initialization
        void Awake()
        {
            StaticInfo.MainController = this; // Ссылка на контроллер
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!GameOverGameObject.activeSelf) // Если активно окно Гейм Овер - игра замирает, а значит не создаются новые итемы
            {
                if (_timePass > 350f / StaticInfo.Speed)  // Чем больше скорость, тем быстрее создаются итемы, изначально раз в секунду
                {
                    _timePass = 0;
                    CreateNewItem();
                }
                else
                {
                    _timePass += Time.deltaTime;
                }

                SetScore(StaticInfo.Score + Time.deltaTime); // Обновляем время (счёт)
            }



        }

        /// <summary>
        /// Создание нового итема
        /// </summary>
        public void CreateNewItem()
        {
            int randId = Random.Range(0, ItemPrefabList.Count); // Рандумный ИД итема
            GameObject newItem = Instantiate(ItemPrefabList[randId]); // создаём итем
            newItem.transform.SetParent(ItemConteinerTransform); // устанавливаем родитель
            newItem.transform.SetSiblingIndex(1); // устанавливаем айди слоя
            Vector2 itemSize = newItem.GetComponent<RectTransform>().sizeDelta; // узнаем размеры обьекта 
            newItem.transform.position = new Vector2(Random.Range(0 + (itemSize.x / 2f), Screen.width - (itemSize.x / 2f)), Screen.height + (itemSize.y / 2f)); // Создаём обьект поверх экрана, в рандумной широте, но что бы  обьект не вылезал своей половиной. Для полного ресайзинга нужно заморочится и выбирать не просто из ширины/высоты экрана, а с ширины и высоты бекграунда * на его скейл
            ItemController itemComponent = newItem.AddComponent<ItemController>(); // Добовляем компонент итем контролера
            itemComponent.CurrentType = (EnumObjectType)randId; // Устанавливаем тип
            StaticInfo.AllItems.Add(itemComponent); // Добавляем в лист типов в статик инфо
            itemComponent.FinishCoods = randId == 0 ? new Vector3(Random.Range(0 + (itemSize.x / 2f), Screen.width - (itemSize.x / 2f)), 0 - itemSize.y) : new Vector3(newItem.transform.position.x, 0 - itemSize.y); // Если айди обьекта 0 (квадрат), то пускать его в рандумную точку снизу, если нет, тогда по прямой (точка х - не меняется)
        }

        /// <summary>
        /// Установка кол-ва жизней
        /// </summary>
        /// <param name="lifesNum"></param>
        public void SetLifed(int lifesNum)
        {
            for (int i = 0; i < LifeGameObjects.Count; i++)
            {
                LifeGameObjects[i].SetActive(lifesNum > i);
            }
            if (lifesNum <= 0) // жизней не осталось
            {
                ShowResult();
            }
        }

        /// <summary>
        /// сброс всех параметров и удаление существующих итемов
        /// </summary>
        public void Retry()
        {
            GameOverGameObject.SetActive(false);
            StaticInfo.ResetGame();
        }

        /// <summary>
        /// Показать ГеймОвер окно
        /// </summary>
        public void ShowResult()
        {
            for (int i = 0; i < StaticInfo.AllItems.Count; i++)
            {
                StaticInfo.AllItems[i].Moving = false;
            }
            GameOverGameObject.SetActive(true);
            GameOverText.text = "You score: " + ScoreTxt.text;
        }

        /// <summary>
        /// Установка кол-ва счёта
        /// </summary>
        /// <param name="newScore"></param>
        public void SetScore(float newScore)
        {
            int min = (int)(newScore / 60f);
            int sec = (int)(newScore - (min * 60));
            ScoreTxt.text = min.ToString("00") + ":" + sec.ToString("00");
            StaticInfo.Score = newScore;
        }
    }
}
