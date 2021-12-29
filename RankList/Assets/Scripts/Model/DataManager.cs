using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using View;

namespace Model
{
    public class DataManager
    {
        private DataManager()
        {
        }

        private static DataManager singleton;

        public string mySelfId = "255862475";

        public List<JsonModel> JsonList;

        public JSONNode JsonNode { set; get; }

        public static DataManager CreateInstance()
        {
            if (singleton == null)
            {
                singleton = new DataManager();
            }

            return singleton;
        }
    }
}