using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer [] Lives;
	[SerializeField] private TextMesh Score;
	[SerializeField] private TextMesh Title;
	[SerializeField] private TextMesh SubTitle;

	void Start() 
	{
		Lives = GetComponentsInChildren<SpriteRenderer>();
		Score = GetComponentInChildren<TextMesh>();

		Game.OnNewGame += HandleHudChangedEvent;
		Game.OnGameOver += HandleOnGameOver;
		Game.OnRestart += HandleOnRestart;
		Game.OnLostLife += HandleHudChangedEvent;
		Game.OnScoreChange += HandleHudChangedEvent;

		for( int count = 0; count < Lives.Length; count++ )
		{
			Lives[count].enabled = false;
		}		
	}

	void HandleNewGame( int lives, int score )
	{
		for( int count = 0; count < Lives.Length; count++ )
		{
			Lives[count].enabled = true;
		}

		if( Title != null && SubTitle != null )
		{
			Title.gameObject.SetActive( false );
			SubTitle.gameObject.SetActive( false );
		}
	}

	void HandleOnRestart( int lives, int score )
	{
		if( Title != null && SubTitle != null )
		{
			Title.gameObject.SetActive( true );
			SubTitle.gameObject.SetActive( true );

			Title.text = "Jump!";
			SubTitle.text = "Press Space...";
		}
	}

	void HandleOnGameOver( int lives, int score )
	{
		if( Title != null && SubTitle != null )
		{
			Title.gameObject.SetActive( true );
			SubTitle.gameObject.SetActive( true );
			
			Title.text = "Score: " + score.ToString( "D5" );
			SubTitle.text = "Press Space to continue...";
		}
	}

	void HandleHudChangedEvent( int lives, int score )
	{
		if( Lives != null && Score != null )
		{
			for( int count = 0; count < Lives.Length; count++ )
			{
				Lives[count].enabled = ( count < lives );
			}

			Score.text = score.ToString( "D5" );
		}

		if( Title != null && SubTitle != null )
		{
			Title.gameObject.SetActive( false );
			SubTitle.gameObject.SetActive( false );
		}
	}
}
