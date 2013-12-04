using UnityEngine;
using System.Collections;

public partial class IGManager  : MonoBehaviour // IGManagerThirdInterface : MonoBehaviour 
{

	public void _ScoreFlashPushWorld( Vector3 worldPosition, Vector2 screenOffset, object message, Color color)
	{
		ScoreFlash.Instance.PushWorld( worldPosition,screenOffset,message, color); // int
	}

	public void _ScoreFlashPushScreen(Vector2 screenPosition, object message) //( Vector3 worldPosition, Vector2 screenOffset, object message, Color color)
	{
		ScoreFlash.Instance.PushScreen (  screenPosition,  message ) ;
	}
	// ScoreFlash.Instance.PushScreen(new Vector2(Screen.width/2  ,0.0f),(GenerateAchievement( "",  "",_tex )));  


	public void _AudioPlay( string audioID )
	{
		AudioController.Play (audioID); // ("RockDie_snd");
	}
}
