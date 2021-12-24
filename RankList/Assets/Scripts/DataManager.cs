using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class DataManager
{
   private DataManager() { }
   private static DataManager _Singleton = null;

   public string mySelfId = "3716954261";
   public static DataManager CreateInstance()
   {
      if (_Singleton == null)
      {
         _Singleton = new DataManager();
      }
      return _Singleton;
   }

   private JSONNode jsonNode;

   public JSONNode JsonNode
   {
      set
      {
         jsonNode = value;
      }
      get
      {
         return jsonNode;
      }
   }
}
