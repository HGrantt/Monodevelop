using System;


namespace Tabupet
{

	public class Board
	{
		private char[][] boardval;
		private char[][] tempboard;
		private int boardsize;

		public char[][] GetBoard()
		{
			return boardval;
		}

		public int GetBoardSize()
		{
			return boardsize;
		}

		public Board (char[][] _board)
		{
			boardsize = 15;
			boardval = new char[15][];
			tempboard = new char[15][];
			for (int i = 0; i < 15; i++)
			{
				boardval[i] = new char[15];
				tempboard [i] = new char[15];
				for (int p = 0; p < 15; p++)
				{
					boardval[i][p] = _board[i][p];
					tempboard [i] [p] = _board[i][p];
				}
			}
		}

		public Board (int _boardsize)
		{
			boardsize = _boardsize;
			boardval = new char[_boardsize][];
			tempboard = new char[_boardsize][];
			for (int i = 0; i < _boardsize; i++)
			{
				boardval[i] = new char[_boardsize];
				tempboard [i] = new char[_boardsize];
				for (int p = 0; p < _boardsize; p++)
				{
					boardval[i][p] = '#';
					tempboard [i] [p] = '#';
				}
			}
		}

		public bool AddWord(string word, Player p, int xpos, int ypos, bool isvert)
		{
			for (int i = 0; i < boardsize; ++i)
			{
				for (int j = 0; j < boardsize; ++j)
				{
					tempboard [i] [j] = boardval [i] [j];
				}
			}
			if (xpos < 0 || xpos >= boardsize || ypos < 0 || ypos >= boardsize)
			{
				return false;
			}
			int shift = 0;
			if (isvert)
			{
				for (int i = 0; i < word.Length; i++)
				{
					if (ypos + i + shift >= boardsize)
					{
						return false;
					}
					while (boardval [xpos] [ypos + i + shift] != '#' && word[0] != '#')
					{
						shift++;
					}
					boardval [xpos] [ypos + i + shift] = (char)word [i];
				}

			}
			else
			{
				for (int i = 0; i < word.Length; i++)
				{
					if (xpos + i + shift >= boardsize)
					{
						return false;
					}
					while (boardval [xpos + i + shift] [ypos] != '#' && word[0] != '#')
					{
						shift++;
					}
					boardval [xpos + i + shift] [ypos] = (char)word [i];
				}
			}
			return true;
		}

		public void RestoreBackup()
		{
			for (int j = 0; j < boardsize; ++j)
			{
				for (int k = 0; k < boardsize; ++k)
				{
					boardval [j] [k] = tempboard [j] [k];
				}
			}
		}

		public void PrintBoard()
		{
			for (int i = 0; i < boardsize; i++)
			{
				for(int l = 0; l < boardsize; l++)
				{
					Console.Write(boardval[l][i]);
				}
				Console.Write ("\n");
			}
		}
	}
}

