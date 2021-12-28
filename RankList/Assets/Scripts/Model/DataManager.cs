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

        private static DataManager _singleton;

        public string mySelfId = "255862475";
        
        public List<JsonModel> JsonList;

        public JSONNode JsonNode { set; get; }

        public static DataManager CreateInstance()
        {
            if (_singleton == null)
            {
                _singleton = new DataManager();
            }

            return _singleton;
        }


    }
}