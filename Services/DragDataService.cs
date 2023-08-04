using HappyBrowser.Entity;

namespace HappyBrowser.Services
{
    public class DragDataService
    {
        private static string? dragKey = string.Empty;
        private static DragDataEntity? dragDataEntity = null;

        public static void Add(string? key, DragDataEntity? data)
        {
            if(string.IsNullOrEmpty(key) || data == null) return;
            dragKey = key;
            dragDataEntity = data;
        }

        public static DragDataEntity? Get(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            DragDataEntity? dragData = dragDataEntity;
            dragDataEntity = null;
            dragKey = string.Empty;
            return dragData;
        }

        public static void Remove(string key)
        {
            dragKey = string.Empty;
            dragDataEntity = null;
        }
    }
}
