using System;
using UnityEngine;

namespace Model
{
   [Serializable]
   public class SceneObjectData
   {
      public string Name;
      public float GoldCoast;
      public float BricksCoast;
      public float ScienceCoast;  
      
      public Sprite Icon;
      public GameObject ModelPrefab;
      
      public float Health;
      
           
      public Vector3 Position;
      public Quaternion Rotation;
      
   }
}
