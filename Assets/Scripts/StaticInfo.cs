using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class StaticInfo
    {
        public static MainController MainController; // Основной контроллер
        public static int CurrentLife = 5; // Количество жизней
        public static float Speed = 350; // Скорость
        public static float Score = 0; // Счёт
        public static List<ItemController> AllItems = new List<ItemController>(); // Компоненты ItemController всех итемов

        /// <summary>
        /// Перезагрузка игры
        /// </summary>
        public static void ResetGame()
        {
            for (int i = 0; i < AllItems.Count; i++) // Удаляем все Гейм Обьекты
            {
                AllItems[i].DestroyGO();
            }
            AllItems = new List<ItemController>(); // Чистим лист

            // Ставим переменные на дефолт
            CurrentLife = 5;
            MainController.SetLifed(CurrentLife);
            Speed = 350;
            Score = 0;
        }

        /// <summary>
        /// Вызываем при клике на итем или при прохождения итема всего пути для проверки на изменение жизней,для изминения скорости и удаление
        /// </summary>
        /// <param name="item"></param>
        /// <param name="wrongClick"></param>
        public static void ClickItem(ItemController item, bool wrongClick)
        {
            if (wrongClick)
            {
                CurrentLife--;
                MainController.SetLifed(CurrentLife);
            }
            Speed += 20;
            AllItems.Remove(item);
            item.DestroyGO();
        }

    }
}
