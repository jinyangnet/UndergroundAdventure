using UnityEngine;
using System.Collections;

public class IGScoreSync : MonoBehaviour {
	
	tk2dTextMesh tktextMeshHP ;
	tk2dTextMesh tktextMeshGoleCoin ;
	
	int i = 1000 ;
	int tick = 0 ;
	IGScores _score ;

	// Use this for initialization
	void Start () {
		
		//tktextMesh = gameObject.GetComponent<tk2dTextMesh> as tk2dTextMesh ;
		tktextMeshHP = transform.gameObject.GetComponent("tk2dTextMesh") as tk2dTextMesh;
		GameObject  _gold = GameObject.FindWithTag ( "goldcoin" ) ;
		if ( _gold )
		{
			tktextMeshGoleCoin = _gold.GetComponent("tk2dTextMesh") as tk2dTextMesh ;
		}

		//IGState.st.Score = new IGScores();
		
		//IGState.st.Score.HP  = 200;
		//IGState.st.Score.OriginalHP  = 200 ;
		//IGState.st.Score.GoldCoin  = 0 ;

		_score = new IGScores ();
		_score.HP = IGState.st.Score.HP ;
		_score.OriginalHP = IGState.st.Score.OriginalHP  ;
		_score.GoldCoin = IGState.st.Score.GoldCoin   ;

		StartCoroutine( CalculateScore() );
	}

	public  IGScores Score
	{
		get { return _score ; }
		set { _score = value; }
	}

	IEnumerator CalculateScore()
	{
		bool running = true;
		while (running) 
		{
			if( IGState.IsGameOver ) 
			{
				running = false;
				break;
			}

			yield return new WaitForSeconds(0.05f);
			
			//IGState.GameScore --;
			//if ( Score.HP <  IGState.st.Score.HP ) Score.HP++;
			if ( Score.HP > IGState.st.Score.HP ) Score.HP--;
			if ( Score.HP < IGState.st.Score.HP ) Score.HP++;

			if ( Score.GoldCoin <  IGState.st.Score.GoldCoin ) Score.GoldCoin = Score.GoldCoin + 10 ;

			tktextMeshHP.text = string.Format ( "{0}/{1}",Score.HP ,  Score.OriginalHP ) ;  // "SCORE:" +  IGState.GameScore.ToString() ; // string.Format("SCORE:{0}",i) ;  //i.ToString() ;SCORE
			tktextMeshHP.Commit() ;

			if ( tktextMeshGoleCoin )
			{
				tktextMeshGoleCoin.text = string.Format ( "{0}",Score.GoldCoin  ) ;  // "SCORE:" +  IGState.GameScore.ToString() ; // string.Format("SCORE:{0}",i) ;  //i.ToString() ;SCORE
				tktextMeshGoleCoin.Commit() ;
			}

		}
	}
}
