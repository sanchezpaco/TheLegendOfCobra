using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigame {

	void Intro();
	void EnableGame();
	void GameOver ();
	void GameWin();
	void GameFinish ();
}
