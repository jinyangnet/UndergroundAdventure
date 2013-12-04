using UnityEngine;
using System.Collections;
using System;
//using System.Configuration;
using System.Xml;
using System.Text;
using System.IO;

//using System.Data;

public class XMLManager : MonoBehaviour {

    public XMLManager()
    {
    }
	
	
	public static int getStarNumber(int id)
	{
		if( IGState.LevelDataList.Count == 0 )
		{
			InitLevelData() ;
		}
		int i = -1 ;
		if(IGState.LevelDataList.ContainsKey(id))
		{
			i= IGState.LevelDataList[id].starCount ;
		}
		return i ;
	}
	
	public static void UpdateLevelStarNumber( int id,int score, int starNumber )
	{
		id = id ;
		if(IGState.LevelDataList.ContainsKey(id))
		{
		    IGState.LevelDataList[id].starCount = starNumber ;
			update(id+"",score+"",starNumber+"") ;
			
			/*
			string sql =  "UPDATE GameLevels SET starCount = "+ starNumber +" WHERE [ID]=" + GameState.LevelsIndex;
			Debug.Log ( sql ) ;
			SQLHelper sh = new SQLHelper () ;
			sh.executeNonQuery(sql);
			*/
		}
	}
	
	static void InitLevelData()
	{
		string path =IGState.GetLevelDataPath() ; 
		
		// GetXmlFullPath() ;
		// using System.Xml ;
		
   		XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load( path );
		XmlNodeList rootnodeList = xmlDoc.SelectSingleNode("gamelevel").ChildNodes;
		
		//Debug.Log(string.Format("id={0},score={1},starcount={2}",id,score,starcount) );
        foreach (XmlNode xn in rootnodeList) 
        {
			IGLevelMode lm=new IGLevelMode ()   ;
            XmlElement xe = (XmlElement)xn  ; 
            XmlNodeList nls = xe.ChildNodes ;
			
			lm.ID = System.Convert.ToInt32(  xe.Attributes[0].Value ) ; 
            foreach (XmlNode xnchlid in nls) 
            {
                XmlElement xeclild = (XmlElement)xnchlid; 
				if (xeclild.Name == "score") 
                {
                    lm.score  = System.Convert.ToInt32( xeclild.InnerText ) ;  ;
                }
				
                if (xeclild.Name == "starCount") 
                {
                   lm.starCount = System.Convert.ToInt32( xeclild.InnerText ) ;  ; 
                }

                if (xeclild.Name == "isLocked") 
                {
                    lm.isLocked  = System.Convert.ToBoolean( xeclild.InnerText ) ;  ;
                }
            }
			
			IGState.LevelDataList.Add(lm.ID,lm);
            // break ;
		}
		
		/*
		string selectlevelsql = " select id, score,starCount,isLocked from [GameLevels] " ;
		// SQLHelper sh = new SQLHelper () ;
		// sh.executeNonQuery(sql);
		
		using (SqliteDataReader reader =sh.executeReader (  selectlevelsql ) )
		{
			GameState.LevelDataList.Clear();
			while ( reader.Read() )
	    	{
				LevelMode lm=new LevelMode ();
				lm.ID = reader.GetInt32 (0);
	            lm.score = reader.GetInt32 (1);
				lm.starCount = reader.GetInt32 (2);
				lm.isLocked = reader.GetBoolean (3);
				GameState.LevelDataList.Add(lm.ID,lm);
	     		//Debug.Log( string.Format("id={0},ispass={1}, pic={2},score={3},sence={4},getstar={5}",id,ispass,pic,score,sence,getstar ) ); 
	    	}
		}
		
		*/
		
	}
	
	///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


    //XmlNode RowID = Doc.CreateElement("RowID");
    //XmlNode pic = Doc.CreateElement("pic");
    //XmlNode score = Doc.CreateElement("score");
    //XmlNode sence = Doc.CreateElement("sence");
    //XmlNode starCount = Doc.CreateElement("starCount");
    //XmlNode isLocked = Doc.CreateElement("isLocked");

    public static void insert(IGLevelMode leveldata)
    {
		string path =IGState.GetLevelDataPath() ; // GetXmlFullPath() ;
		
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load( path );

        XmlNode root = xmlDoc.SelectSingleNode("gamelevel");
        XmlElement _xmlElement = xmlDoc.CreateElement("level");
        _xmlElement.SetAttribute("id", leveldata.ID +"" );
      
        XmlElement xesub_pic = xmlDoc.CreateElement("pic");
        xesub_pic.InnerText = leveldata.pic ; 
        _xmlElement.AppendChild(xesub_pic);

        XmlElement xesub_score = xmlDoc.CreateElement("score");
        xesub_score.InnerText = leveldata.score +"" ;
        _xmlElement.AppendChild(xesub_score);

        XmlElement xesub_sence = xmlDoc.CreateElement("sence");
        xesub_sence.InnerText = leveldata.sence +"" ;
        _xmlElement.AppendChild(xesub_sence);

        XmlElement xesub_starCount = xmlDoc.CreateElement("starCount");
        xesub_starCount.InnerText = leveldata.starCount +"";
        _xmlElement.AppendChild(xesub_starCount);

        XmlElement xesub_isLocked = xmlDoc.CreateElement("isLocked");
        xesub_isLocked.InnerText = leveldata.isLocked +"" ;
        _xmlElement.AppendChild(xesub_isLocked);

        root.AppendChild(_xmlElement);//添加到节点中 
        xmlDoc.Save( path );

    }

    static void update(string id, string score, string starcount)
    {
		string path = IGState.GetLevelDataPath () ; // GetXmlFullPath() ;
        XmlDocument xmlDoc = new XmlDocument() ;
        xmlDoc.Load( path ) ;
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("gamelevel").ChildNodes ;
		
		Debug.Log(string.Format("id={0},score={1},starcount={2}",id,score,starcount) );
		
        foreach (XmlNode xn in nodeList) 
        {
            XmlElement xe = (XmlElement)xn; 
			
			Debug.Log( string.Format("xe.GetAttribute(id) = ",xe.GetAttribute("id")   ) ) ;
			Debug.Log( xe.Attributes[0].Value ) ;
			
            if ( xe.Attributes[0].Value ==  id )
            {
                XmlNodeList nls = xe.ChildNodes;
                foreach (XmlNode xnchlid in nls) 
                {
                    XmlElement xeclild = (XmlElement)xnchlid; 
                    if (xeclild.Name == "starCount") 
                    {
                        xeclild.InnerText = starcount ; 
                        //break; 
                    }

                    if (xeclild.Name == "score") 
                    {
                        xeclild.InnerText = score;
                        //break; 
                    }
                }
                //break;
            }
        }
        xmlDoc.Save( path ) ;
    }


    void delete()
    {
		string path = IGState.GetLevelDataPath() ; // GetXmlFullPath() ;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load( path );

        XmlNodeList xnl = xmlDoc.SelectSingleNode("bookstore").ChildNodes;

        foreach (XmlNode xn in xnl)
        {
            XmlElement xe = (XmlElement)xn;
            if (xe.GetAttribute("genre") == "fantasy")
            {
                xe.RemoveAttribute("genre");//删除genre属性 
            }
            else if (xe.GetAttribute("genre") == "update李赞红")
            {
                xe.RemoveAll();//删除该节点的全部内容 
            }
        }
        xmlDoc.Save( path );
    }
	
	
    public void XMLInsert( IGLevelMode leveldata, string bz, int childNodesIndex )
    {
        IList _list = new ArrayList();
        string Load =  "\\" + bz + ".xml";
		
        XmlDocument Doc = new XmlDocument();
		
		XmlNode RowID = Doc.CreateElement("RowID");
        XmlNode pic = Doc.CreateElement("pic");
        XmlNode score = Doc.CreateElement("score");
        XmlNode sence = Doc.CreateElement("sence");
        XmlNode starCount = Doc.CreateElement("starCount");
        XmlNode isLocked = Doc.CreateElement("isLocked");
  
        //XmlAttribute RowID = Doc.CreateAttribute("RowID");           
        //---------------------------------------------------------------
		
		XmlAttribute RowType = Doc.CreateAttribute("RowType");
        RowType.Value = leveldata.ID.ToString();
		
        XmlText strpic = Doc.CreateTextNode(leveldata.pic );
        XmlText strscore = Doc.CreateTextNode(leveldata.score.ToString() );
        XmlText strsence = Doc.CreateTextNode(leveldata.sence.ToString());
        XmlText strstarCount = Doc.CreateTextNode(leveldata.starCount.ToString() ); 
		XmlText strisLocked = Doc.CreateTextNode(leveldata.isLocked.ToString() );
		
        //----------------------------------------------------------------

        RowID.Attributes.Append(RowType);
		
        //RowNodes.Attributes.Append(RowID);
        RowID.AppendChild(pic);
        RowID.AppendChild(score);
        RowID.AppendChild(sence);
        RowID.AppendChild(starCount);
        RowID.AppendChild(isLocked);
		
        score.AppendChild(strscore);
        sence.AppendChild(strsence);
        pic.AppendChild(strpic);
        starCount.AppendChild(strstarCount);
        isLocked.AppendChild(strisLocked);

        // if (leveldata.Flag == 1)
        {
            try
            {
                Doc.Load(Load);
                XmlNode root = Doc.DocumentElement;

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    _list.Add(root.ChildNodes.Item(i));
                }
                // root.ChildNodes.Count
                for (int i = 0; i < _list.Count; i++)
                {
                    if (childNodesIndex == i)
                    {
                        root.AppendChild(RowID);
                        root.AppendChild((XmlNode)_list[i]);
                    }
                    else
                        root.AppendChild((XmlNode)_list[i]);
                }


                Doc.Save(Load);
            }
            catch (Exception e)
            {
               
            }
        }
    }
	
    public string UpdateNewXml(string sbz, string bz)
    {
        XmlDocument Doc = new XmlDocument();
        XmlDocument SDoc = new XmlDocument();
        string SLoadPath =  "\\" + sbz + ".xml";
        string LoadPath =  "\\" + bz + ".xml";
        string LoadPathCpy =  "_00"; //
        //File.Copy(oldLoad, Load);

        try
        {
            File.Copy(SLoadPath, LoadPathCpy);
            Doc.Load(LoadPath);
            SDoc.Load(LoadPathCpy);

            XmlNode node = Doc.DocumentElement;
            XmlNode Snode = SDoc.DocumentElement;

            Snode.Attributes.Item(0).Value = node.Attributes.Item(0).Value;
            Snode.Attributes.Item(1).Value = node.Attributes.Item(1).Value;
            Snode.Attributes.Item(2).Value = node.Attributes.Item(2).Value;
            Snode.Attributes.Item(3).Value = node.Attributes.Item(3).Value;
            Snode.Attributes.Item(4).Value = node.Attributes.Item(4).Value;

            Snode.Attributes.Item(5).Value = node.Attributes.Item(5).Value;
            Snode.Attributes.Item(6).Value = node.Attributes.Item(6).Value;

            Snode.Attributes.Item(7).Value = node.Attributes.Item(7).Value;
            Snode.Attributes.Item(8).Value = node.Attributes.Item(8).Value;
            Snode.Attributes.Item(9).Value = node.Attributes.Item(9).Value;
            Snode.Attributes.Item(10).Value = node.Attributes.Item(10).Value;

            //node.ReplaceChild(node, Doc.DocumentElement.OwnerDocument
            SDoc.Save(LoadPathCpy);
            File.Delete(LoadPath);
            File.Copy(LoadPathCpy, LoadPath);
            return null;
        }
        catch (XmlException e)
        {
            return e.ToString() + "  请联系技术人员！！";
        }
    }

    public string XmlCardChildsDel(string bz, int ChildNodesIndex)
    {
        string Load =  "\\" + bz + ".xml";
        XmlDocument Doc = new XmlDocument();

        if (File.Exists(Load))
        {
            try
            {
                Doc.Load(Load);
                XmlNode node = Doc.DocumentElement;
                if (node.ChildNodes.Count > 0)
                {
                    try
                    {
                        node.RemoveChild(node.ChildNodes.Item(ChildNodesIndex)); //node.ChildNodes.Count - 1));
                        //node.ReplaceChild(node.ChildNodes.Item(ChildNodesIndex), node.ChildNodes.Item(ChildNodesIndex));
                        //node.ReplaceChild(
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                }

                Doc.Save(Load);
                return null;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        else { return "文件不存在！！"; }
    }
	
}
	
	
