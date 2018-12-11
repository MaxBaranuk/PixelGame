using System.Collections.Generic;
using Model;
using UnityEditor;
using UnityEngine;

namespace DataBase
{
    public class BuildingsEditor : EditorWindow
    {      
        public BuildingsCatalogue BuildingsCatalogueItems;
        private int _viewIndex = 1;
    
    [MenuItem ("Window/Buildings Storage %#e")]
    static void  Init () 
    {
        var window = GetWindow (typeof (BuildingsEditor));
//        window.titleContent = new GUIContent("Building editor");
    }
    
    void  OnEnable () {
        if (!EditorPrefs.HasKey("ObjectPath")) return;
        var objectPath = EditorPrefs.GetString("ObjectPath");
        BuildingsCatalogueItems = AssetDatabase.LoadAssetAtPath (objectPath, typeof(BuildingsCatalogue)) as BuildingsCatalogue;
    }
    
    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Building Editor", EditorStyles.boldLabel);
        if (BuildingsCatalogueItems != null) {
            if (GUILayout.Button("Show Item List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = BuildingsCatalogueItems;
            }
        }
        if (GUILayout.Button("Open Item List")) 
        {
                OpenItemList();
        }
//        if (GUILayout.Button("New Item List")) 
//        {
//            EditorUtility.FocusProjectWindow();
//            Selection.activeObject = BuildingsCatalogueItems;
//        }
        GUILayout.EndHorizontal ();
        
        if (BuildingsCatalogueItems == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false))) 
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal ();
        }
            
            GUILayout.Space(20);
            
        if (BuildingsCatalogueItems != null) 
        {
            GUILayout.BeginHorizontal ();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
            {
                if (_viewIndex > 1)
                    _viewIndex --;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
            {
                if (_viewIndex < BuildingsCatalogueItems.Buildings.Count) 
                {
                    _viewIndex ++;
                }
            }
            
            GUILayout.Space(60);
            
            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
            {
                DeleteItem(_viewIndex - 1);
            }
            
            GUILayout.EndHorizontal ();
            if (BuildingsCatalogueItems.Buildings == null)
                Debug.Log("wtf");
            if (BuildingsCatalogueItems.Buildings != null && BuildingsCatalogueItems.Buildings.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                _viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", _viewIndex, GUILayout.ExpandWidth(false)), 1, BuildingsCatalogueItems.Buildings.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField ("of   " +  BuildingsCatalogueItems.Buildings.Count + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();
                
                BuildingsCatalogueItems.Buildings[_viewIndex-1].Name = EditorGUILayout.TextField ("Name", BuildingsCatalogueItems.Buildings[_viewIndex-1].Name);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].Icon = EditorGUILayout.ObjectField ("Icon", BuildingsCatalogueItems.Buildings[_viewIndex-1].Icon, typeof (Sprite), false) as Sprite;
                BuildingsCatalogueItems.Buildings[_viewIndex-1].ModelPrefab = EditorGUILayout.ObjectField ("Prefab", BuildingsCatalogueItems.Buildings[_viewIndex-1].ModelPrefab, typeof (GameObject), false) as GameObject;
                BuildingsCatalogueItems.Buildings[_viewIndex-1].Square = EditorGUILayout.Vector2Field("Square", BuildingsCatalogueItems.Buildings[_viewIndex-1].Square, GUILayout.ExpandWidth(false));
               
                GUILayout.Space(10);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].GoldCoast = EditorGUILayout.FloatField("Gold Coast", BuildingsCatalogueItems.Buildings[_viewIndex-1].GoldCoast);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].BricksCoast = EditorGUILayout.FloatField("Brick Coast", BuildingsCatalogueItems.Buildings[_viewIndex-1].BricksCoast);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].ScienceCoast = EditorGUILayout.FloatField("Science Coast", BuildingsCatalogueItems.Buildings[_viewIndex-1].ScienceCoast);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].Health = EditorGUILayout.FloatField("Health", BuildingsCatalogueItems.Buildings[_viewIndex-1].Health);
         
                GUILayout.Space(10);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].GoldProduction = EditorGUILayout.FloatField("Gold Production", BuildingsCatalogueItems.Buildings[_viewIndex-1].GoldProduction);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].BricksProduction = EditorGUILayout.FloatField("Brick Production", BuildingsCatalogueItems.Buildings[_viewIndex-1].BricksProduction);
                BuildingsCatalogueItems.Buildings[_viewIndex-1].ScienceProduction = EditorGUILayout.FloatField("Science Production", BuildingsCatalogueItems.Buildings[_viewIndex-1].ScienceProduction);
                
                GUILayout.Space(10);
            
            } 
            else 
            {
                GUILayout.Label ("This Catalogue List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(BuildingsCatalogueItems);
        }
    }

    private void CreateNewItemList () 
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        _viewIndex = 1;
        BuildingsCatalogueItems = CreateCatalogueStorage.Create();
        if (!BuildingsCatalogueItems) return;
        BuildingsCatalogueItems.Buildings = new List<BuildingData>();
        var relPath = AssetDatabase.GetAssetPath(BuildingsCatalogueItems);
        EditorPrefs.SetString("ObjectPath", relPath);
    }

    private void OpenItemList () 
    {
        var absPath = EditorUtility.OpenFilePanel ("Select Buildings List", "", "");
        if (!absPath.StartsWith(Application.dataPath)) return;
        var relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
        BuildingsCatalogueItems = AssetDatabase.LoadAssetAtPath (relPath, typeof(BuildingsCatalogue)) as BuildingsCatalogue;
        if (BuildingsCatalogueItems != null && BuildingsCatalogueItems.Buildings == null)
            BuildingsCatalogueItems.Buildings = new List<BuildingData>();
        if (BuildingsCatalogueItems) {
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    private void AddItem () 
    {
        var newItem = new BuildingData {Name = "New Item"};
        BuildingsCatalogueItems.Buildings.Add (newItem);
        _viewIndex = BuildingsCatalogueItems.Buildings.Count;
    }

    private void DeleteItem (int index) 
    {
        BuildingsCatalogueItems.Buildings.RemoveAt (index);
    }       
    }
}