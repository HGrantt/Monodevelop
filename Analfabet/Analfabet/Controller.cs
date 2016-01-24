using System;
using System.Collections.Generic;
using Analfabet;

namespace Tabupet
{
	public class Controller
	{
		private Board board;
		private List<Player> players;
		private int turn;
		private Random rand = new Random();
		private Rules rules;
		//private int endgame = 0;
		private DateTime dt;

		public Controller ()
		{
			dt = DateTime.MinValue;

			players = new List<Player> ();
			board = new Board (15);
			turn = 0;
			rules = new Rules ();
			rand.Next (0, 2);
		}

		public void applyConfig()
		{
			if (System.IO.File.Exists ("Savestate.xml") && (System.IO.File.GetLastWriteTime("Savestate.xml") > dt))
			{
				dt = System.IO.File.GetLastWriteTime ("Savestate.xml");
				State state = new State ();
				state.loadFromXML ();
				board = new Board ((char[][])state.board.Clone ());
				players = new List<Player> (state.players);
				turn = state.playerturn;
			}
		}

		public bool GameEnd()
		{
			return rules.GameDone ();
		}

		public List<Player> GetPlayers()
		{
			return players;
		}


		public void AddPlayer(string name)
		{
			players.Add(new Player(name));
		}

		public bool PlayWord(string word, int posx, int posy, bool isvert)
		{
			players [turn].BackupLetters ();
			if (players [turn].RemoveWord (word, false) == false)
			{
				players [turn].RestoreBackup ();
				return false;
			}
			board.AddWord (word, players [turn], posx, posy, isvert);
			if (rules.ValidateBoard (board))
			{
				rules.AddScore (word.Length);
				//endgame += word.Length;
				players [turn].RestoreLetters ();
				PlayerTurn ();
				return true;

			} 
			else
			{
				board.RestoreBackup ();
				players [turn].RestoreBackup ();
				return false;
			}
		}

		public void ThrowBricks()
		{
			players [turn].RemoveWord (players [turn].GetLetters(), true);

			players [turn].RestoreLetters ();
		
			PlayerTurn ();
		}

		public char[][] GetBoard()
		{
			return board.GetBoard();
		}

		public string GetBricks()
		{
			return players[turn].GetLetters();
		}

		public void PlayerTurn()
		{
			turn = (turn + 1) % players.Count;
		}
		public int getTurn()
		{
			return turn;	
		}
		public string GetWinner()
		{
			return rules.GetLeader (this);
		}
	}
}

