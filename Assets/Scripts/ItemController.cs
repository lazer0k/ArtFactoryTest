using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ItemController : MonoBehaviour, IPointerDownHandler
    {

        public EnumObjectType CurrentType; // Тип итема
        public Vector2 FinishCoods; // Координаты, куда двигаемся
        public bool Moving = true; // Двигаемся ли

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Moving) 
            {
                _moveItem();
            }


        }

        /// <summary>
        /// Передвижение и проверка на финиш
        /// </summary>
        private void _moveItem()
        {
            Vector2 newCoods = Vector2.MoveTowards(gameObject.transform.position, FinishCoods, Time.deltaTime * StaticInfo.Speed);
            gameObject.transform.position = newCoods;
            if (FinishCoods == newCoods)
            {
                StaticInfo.ClickItem(this, CurrentType != EnumObjectType.BadType);
            }
        
        }

        /// <summary>
        /// Проверка на клик
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
        
            StaticInfo.ClickItem(this, (CurrentType == EnumObjectType.BadType));
        
        }


        /// <summary>
        /// Удаление обьекта
        /// </summary>
        public void DestroyGO()
        {
            Destroy(gameObject);
        }
    }
}
