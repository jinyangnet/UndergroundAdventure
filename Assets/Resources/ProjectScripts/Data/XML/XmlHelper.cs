
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
/*
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
*/

public class XmlHelper : MonoBehaviour 
{
	public static void ExportXML() 
	{
	    string filepath = IGState.GetDataPath() ;
		//Application.streamingAssetsPath + "/"+ GameState.LevelsIndex +".xml";
		
		Debug.Log( "XmlHelper filepath = " + filepath);
		
		if(File.Exists (filepath))
		{
			File.Delete(filepath);
		}
		
		XmlDocument xmlDoc = new XmlDocument(); 
		XmlElement root = xmlDoc.CreateElement("gameObjects");
		
				
        //string name = S.path;
		//EditorApplication.OpenScene(name);
		
		string name  = "sence"  ;
		string gamestyle = "normal" ;
		// stylebomb stylebomblocked stylethefifthelement
		Object[] styleobjs = GameObject.FindGameObjectsWithTag("gamestyle") ; //  FindObjectsOfType(typeof(GameObject));
		if( styleobjs.Length >0 )
		{
			gamestyle = styleobjs[0].name.ToLower().Replace("(Clone)","") ;
		}
	
		XmlElement scenes = xmlDoc.CreateElement("scenes");
        scenes.SetAttribute("name",name   ) ;
		scenes.SetAttribute("gamestyle",gamestyle ) ;
		
		Object[] objs = GameObject.FindGameObjectsWithTag("objects") ;
		foreach (GameObject obj in objs)
		//foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
		{
			
			 XmlElement gameObject = xmlDoc.CreateElement("gameObjects");
			 gameObject.SetAttribute("name",obj.name.ToLower().Replace("(clone)","") );
			
			 gameObject.SetAttribute("asset",obj.name + ".prefab");
			 XmlElement transform = xmlDoc.CreateElement("transform");
			
			 XmlElement position = xmlDoc.CreateElement("position");
			 XmlElement position_x = xmlDoc.CreateElement("x");
			 position_x.InnerText = obj.transform.position.x+"";
			 XmlElement position_y = xmlDoc.CreateElement("y");
			 position_y.InnerText = obj.transform.position.y+"";
			 XmlElement position_z = xmlDoc.CreateElement("z");
			 position_z.InnerText = obj.transform.position.z+"";
			 position.AppendChild(position_x);
			 position.AppendChild(position_y);
			 position.AppendChild(position_z);

			 XmlElement rotation = xmlDoc.CreateElement("rotation");
			 XmlElement rotation_x = xmlDoc.CreateElement("x");
			 rotation_x.InnerText = obj.transform.rotation.eulerAngles.x+"";
			 XmlElement rotation_y = xmlDoc.CreateElement("y");
			 rotation_y.InnerText = obj.transform.rotation.eulerAngles.y+"";
			 XmlElement rotation_z = xmlDoc.CreateElement("z");
			 rotation_z.InnerText = obj.transform.rotation.eulerAngles.z+"";
			 rotation.AppendChild(rotation_x);
			 rotation.AppendChild(rotation_y);
			 rotation.AppendChild(rotation_z);

			 XmlElement scale = xmlDoc.CreateElement("scale");
			 XmlElement scale_x = xmlDoc.CreateElement("x");
			 scale_x.InnerText = obj.transform.localScale.x+"";
			 XmlElement scale_y = xmlDoc.CreateElement("y");
			 scale_y.InnerText = obj.transform.localScale.y+"";
			 XmlElement scale_z = xmlDoc.CreateElement("z");
			 scale_z.InnerText = obj.transform.localScale.z+"";
			 scale.AppendChild(scale_x);
			 scale.AppendChild(scale_y);
			 scale.AppendChild(scale_z);

			 transform.AppendChild(position);	
			 transform.AppendChild(rotation);	
			 transform.AppendChild(scale);	

			 gameObject.AppendChild(transform);	
			 scenes.AppendChild(gameObject);
			 root.AppendChild(scenes);
			 xmlDoc.AppendChild(root);
			
		}
		xmlDoc.Save(filepath);
				
	   print("xmlDoc.Save(filepath);    -- ");
		 //AssetDatabase.Refresh();
	}
	
	
		// Use this for initialization
	public void XMLStart () 
	{
		
		//return ;
		
		Object[] objs = GameObject.FindGameObjectsWithTag("objects") ; //  FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in objs)
		{
		   //float area = ComputCircleArea(go.collider.bounds.size.x);
		   //print("objname: "+go.name + "position=" + go.transform.position + "localPosition=" + go.transform.localPosition   ) ;//+"- size = "+go.collider.bounds.size  ) ;
		   //SQLManager.SaveLevelObjects(go,1);
		   Destroy(go);	
	    }
		
		//fileName = Application.dataPath + "/" + "test.txt"; 
		string filepath = IGState.GetDataPath() ; //Application.dataPath + string.Format( "/{0}.xml",GameState.LevelsIndex) ;
		
		print( "0000XML XMLStart filepath = " + filepath) ;
		
		if(!File.Exists(filepath))
		{
			if( File.Exists(IGState.GetStreamingAssetsPath() ) )
			{
				File.Copy(IGState.GetStreamingAssetsPath(),filepath);
			}
		}
		
		print( "1111XML XMLStart filepath = " + filepath) ;
		
/*
#if UNITY_EDITOR
		 filepath =Application.streamingAssetsPath + string.Format( "/{0}.xml",levelid) ; //"/my.xml";
#elif UNITY_IPHONE
	    filepath = Application.dataPath +"/Raw"  + string.Format( "/{0}.xml",levelid) ; 
#endif
*/
		if( !File.Exists (filepath) )
		{
			return ;
		}
		
		IGState.PlayingStyleTheFifthElementList.Clear() ;
		IGState.PlayingStyleBombList.Clear() ;

		XmlDocument xmlDoc = new XmlDocument();
	 	xmlDoc.Load(filepath);
		
	 	XmlNodeList nodeList = xmlDoc.SelectSingleNode("gameObjects").ChildNodes;
		print("nodeList="+nodeList);
		
		foreach(XmlElement scene  in nodeList)
		{
			string name=scene.Attributes["name"].Value; 
			string gamestyle=scene.Attributes["gamestyle"].Value ;
			
			// stylebomb stylebomblocked stylethefifthelement
			if( gamestyle.Equals("stylebomb") )
			{
				IGState.Status = PlayingStatus.bomb ;
			}
			else if ( gamestyle.Equals("stylebomblocked")  )
			{
				IGState.Status = PlayingStatus.bombLocked ;
			}
			else if ( gamestyle.Equals("stylethefifthelement")  )
			{
				IGState.Status = PlayingStatus.thefifthelement ;
			}                         //stylebomblockedthefifthelement
			else if ( gamestyle.Equals("stylebomblockedthefifthelement")  )
			{
				IGState.Status = PlayingStatus.bombLockedthefifthelement ;
			}
			else
			{
				IGState.Status = PlayingStatus.Normal ;
			}
				
			print( "name = "+name  + "  nodeList = " + nodeList.Count + " gamestyle = " + gamestyle );
		
			foreach ( XmlElement gameObjects in scene.ChildNodes )
			{
				string asset = "Prefab/" + gameObjects.GetAttribute("name");
				Vector3 pos = Vector3.zero;
				Vector3 rot = Vector3.zero;
				Vector3 sca = Vector3.zero;
				
				foreach(XmlElement transform in gameObjects.ChildNodes)
				{
					foreach(XmlElement prs in transform.ChildNodes)
					{
						if(prs.Name == "position")
						{
							foreach(XmlElement position in prs.ChildNodes)	
							{
								switch(position.Name)
								{
								case "x":
									pos.x = float.Parse(position.InnerText);
									break;
								case "y":
									pos.y = float.Parse(position.InnerText);
									break;
								case "z":
									pos.z = float.Parse(position.InnerText);
									break;
							}
						}
					}
					else if(prs.Name == "rotation")
					{
						foreach(XmlElement rotation in prs.ChildNodes)	
						{
							switch(rotation.Name)
							{
							case "x":
								rot.x = float.Parse(rotation.InnerText);
								break;
							case "y":
								rot.y = float.Parse(rotation.InnerText);
								break;
							case "z":
								rot.z = float.Parse(rotation.InnerText);
								break;
							}
						}
					}
					else if(prs.Name == "scale")
					{
						foreach(XmlElement scale in prs.ChildNodes)	
						{
							switch(scale.Name)
							{
							case "x":
								sca.x = float.Parse(scale.InnerText);
								break;
							case "y":
								sca.y = float.Parse(scale.InnerText);
								break;
							case "z":
								sca.z = float.Parse(scale.InnerText);
								break;
							}
						}		
					}
				}
					
				
				//print( asset ) ; 
				if( Resources.Load(asset) ==null ) 
				{
					continue ;
				}
					
				//string asset = "Prefab/" + gameObjects.GetAttribute("name");
				GameObject ob = (GameObject)Instantiate(Resources.Load(asset),pos,Quaternion.Euler(rot));
				ob.transform.localScale = sca;
				
					
				string _gameObjectsName = gameObjects.GetAttribute("name") ;
				if( IGState.Status == PlayingStatus.bombLocked || IGState.Status == PlayingStatus.bomb  )
				{
					if( _gameObjectsName.Contains("box3")  )
					{
						IGState.PlayingStyleBombList.Add( ob , _gameObjectsName ) ;
					}
				}
				else if (IGState.Status == PlayingStatus.thefifthelement )
				{
					if( _gameObjectsName.Contains("box8")  )
					{
						IGState.PlayingStyleTheFifthElementList.Add( ob , _gameObjectsName ) ;
					}
				}
					
				else if (IGState.Status == PlayingStatus.bombLockedthefifthelement )
				{
					if( _gameObjectsName.Contains("box3")  )
					{
						IGState.PlayingStyleBombList.Add( ob , _gameObjectsName ) ;
					}
						
					if( _gameObjectsName.Contains("box8")  )
					{
						IGState.PlayingStyleTheFifthElementList.Add( ob , _gameObjectsName ) ;
					}
				}
				//
					
					
			
					
				// ************************************
			}
		}
	}
		
		StartCoroutine( InitPlayingStyle() ) ;
		Debug.Log( "GameState.SpecialObjectsList.Count= " +  IGState.PlayingStyleTheFifthElementList.Count + "PlayingGameStyle=" + IGState.Status ) ;
}
	
	// Use this for initialization
    IEnumerator InitPlayingStyle() 
	{
		yield return new WaitForSeconds(0.1f);
		
		if( IGState.Status == PlayingStatus.Normal  )
		{
			GameObject _object = GameObject.Find("little(Clone)");
			if( _object )
			{
				tk2dAnimatedSprite _sprite = _object.transform.GetComponent<tk2dAnimatedSprite>() ;
				if( _sprite )
				{
					LittleState _spritestate = _object.transform.GetComponent<LittleState>() ;
					if( _spritestate )
					{
						_spritestate.IsActivation = true ;
					}
				}
			}
		}
		else if( IGState.Status == PlayingStatus.bombLocked  && IGState.PlayingStyleBombList.Count > 0 )
		{
			string gamestyle = null ;
			GameObject _object = GameObject.Find("little(Clone)");
			{
				if( _object )
				{
					tk2dAnimatedSprite _sprite = _object.transform.GetComponent<tk2dAnimatedSprite>() ;
					if( _sprite )
					{
						_sprite.Stop() ;
						_sprite.Play(3) ;//( "littlelock" ) ;
								
						print ( "_sprite.Play( littlelock );" ) ;
						// _sprite.p
					}
					
					LittleState _spritestate = _object.transform.GetComponent<LittleState>() ;
					if( _spritestate )
					{
						_spritestate.IsActivation = false ;
					}
				}
			}
		}
		
		/////////
		
		else if(
			
			IGState.Status == PlayingStatus.bombLockedthefifthelement  &&
			( IGState.PlayingStyleBombList.Count > 0  &&IGState.PlayingStyleTheFifthElementList.Count>0 )
			
			)
		{
			string gamestyle = null ;
			GameObject _object = GameObject.Find("little(Clone)");
			{
				if( _object )
				{
					tk2dAnimatedSprite _sprite = _object.transform.GetComponent<tk2dAnimatedSprite>() ;
					if( _sprite )
					{
						_sprite.Stop() ;
						_sprite.Play(3) ;//( "littlelock" ) ;
						print ( "_sprite.Play( littlelock );" ) ;
					}
					
					LittleState _spritestate = _object.transform.GetComponent<LittleState>() ;
					if( _spritestate )
					{
						_spritestate.IsActivation = false ;
					}
				}
			}
			
			
			
		}
		
		
	}
	

}

