using System;

namespace Tabupet
{
	public class Rules
	{
		private int limit = 10;
		public int start = 0;

		public Rules ()
		{
		}

		public string GetLeader (Controller ctrl)
		{
			if (ctrl.GetPlayers () [0].GetPoints () > ctrl.GetPlayers () [1].GetPoints ())
			{
				return ctrl.GetPlayers () [0].GetName ();
			}
			else if (ctrl.GetPlayers () [0].GetPoints () < ctrl.GetPlayers () [1].GetPoints ())
			{
				return ctrl.GetPlayers () [1].GetName ();
			}
			else
			{
				return "No one";
			}
		}

		public bool ValidateRow(Board board, bool isvert, int row)
		{
			char current;
			string str = "";
			for (int i = 0; i < board.GetBoardSize(); ++i)
			{
				if (isvert) 
				{
					current = board.GetBoard() [row] [i];
				} 
				else 
				{
					current = board.GetBoard() [i] [row];
				}
				if (current == '#')
				{
					if (str.Length > 1)
					{
						if (ValidateWord (str) == false)
						{
							return false;
						}
					}
					str = "";
				}
				else
				{
					str += current;
				}
			}
			if (str.Length > 1)
			{
				if (ValidateWord (str) == false)
				{
					return false;
				}
			}
			return true;
		}

		public bool ValidateWord(string word)
		{
			string[] lines = System.IO.File.ReadAllLines("clean.txt");
			foreach (string line in lines)
			{
				if (line.Equals (word))
				{
					return true;
				}
			}

			return false;
		}

		public bool ValidateBoard(Board board)
		{
			for (int i = 0; i < board.GetBoardSize(); i++)
			{
				if (ValidateRow (board, true, i) == false || ValidateRow (board, false, i) == false)
				{
					return false;
				}
			}
			return true;
		}

		public bool GameDone()
		{
			return (start >= limit);
		}

		public void AddScore(int points)
		{
			start += points;
		}
	}
}

