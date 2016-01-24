using System;
using Tabupet;

namespace Analfabet
{
	public class UIt
	{
		private char[][] word;
		private Controller ctrl;
		private char letterchosen = '#';
		
		public UIt ()
		{
			this.ctrl = new Controller();
			ctrl.AddPlayer ("OBAMA");
			ctrl.AddPlayer ("TRUMP");
			int boardsize = 15;
			word = new char[boardsize][];
			for (int i = 0; i < boardsize; i++)
			{
				word[i] = new char[boardsize];
				for (int p = 0; p < boardsize; p++)
				{
					word[i][p] = '#';
				}
			}
		}

		public void ApplyConfig()
		{
			ctrl.applyConfig ();
		}

			
		public bool WordLaid()
		{
			for (int x = 0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					if (word [x] [y] != '#')
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool PutWordPressed()
		{
			//hitta ord!
			string verword = "";
			int vstartx = -1;
			int vstarty = -1;

			if (!WordLaid ())
			{
				return false;
			}

			for(int x=0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					if (word [x] [y] != '#')
					{	
						verword += word [x] [y];
						if (vstartx == -1)
						{
							vstartx = x;
							vstarty = y;
						}
					}
				}	
				if (vstartx != -1)
				{
					break;
				}
			}
	
			int hstartx = -1;
			int hstarty = -1;
			string horword = "";
			for(int x=0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					if (word [y] [x] != '#')
					{	
						horword += word [y] [x];
						if (hstartx == -1)
						{
							hstartx = x;
							hstarty = y;
						}
					}
				}	
				if (hstartx != -1)
				{
					break;
				}
			}

			if (horword.Length == 0 && verword.Length == 0)
			{
				return false;
			}

			if (horword.Length > verword.Length)
			{
				ClearWordPressed ();
				return ctrl.PlayWord (horword, hstarty, hstartx, false);
			} 
			else
			{
				ClearWordPressed ();
				return ctrl.PlayWord (verword, vstartx, vstarty, true);
			}
			//updatera
		}

		public void ThrowLettersPressed()
		{
			ctrl.ThrowBricks ();
			ClearWordPressed ();
			letterchosen = '#';
			//updatera
		}

		public void GridSquarePressed(int x, int y)
		{
			if (letterchosen != '#' && ctrl.GetBoard()[x][y] == '#')
			{
				word [x] [y] = letterchosen;
				letterchosen = '#';
			}
		}

		public void LetterSqarePressed (int index)
		{
			letterchosen = ctrl.GetBricks () [index];
		}

		public void NowherePressed()
		{
			letterchosen = '#';
		}

		public char GetLetterSelected()
		{
			return letterchosen;
		}

		public char[][] GetBoardLetters()
		{
			return word;
		}

		public void ClearWordPressed()
		{
			for (int i = 0; i < 15; i++)
			{
				for (int p = 0; p < 15; p++)
				{
					word[i][p] = '#';
				}
			}
		}

		public Controller GetController ()
		{
			return ctrl;
		}

		public string GetPlayerName()
		{
			return ctrl.GetPlayers () [ctrl.getTurn ()].GetName();
		}

		public string GetPlayerLetters()
		{
			return ctrl.GetBricks ();
		}

		public char[][] GetBoard()
		{
			return ctrl.GetBoard ();
		}

		public int GetScore()
		{
			return ctrl.GetPlayers () [ctrl.getTurn()].GetPoints ();
		}

		public string GetWinnerName()
		{
			return ctrl.GetWinner ();
		}

		public int ClickScreen(int mx, int my)
		{
			if ((mx >= 200 && mx <= 550) && (my >= 10 && my <= 360))
			{
				GridSquarePressed (15 * (mx - 200) / 350, 15 * (my - 10) / 350);
			}
			else if ((mx >= 200 && mx <= 550) && (my >= 380 && my <= 380 + (350 / 7)))
			{
				LetterSqarePressed (7 * (mx - 200) / 350);	
			}
			else if ((mx >= 200 && mx <= 300) && (my >= 380 + (350 / 7) && my <= 380 + (350 / 7) + 50))
			{
				bool returnval = PutWordPressed ();
				if (returnval == true)
				{
					return 1;
				}
			}
			else if ((mx >= 320 && mx <= 420) && (my >= 380 + (350 / 7) && my <= 380 + (350 / 7) + 50))
			{
				ThrowLettersPressed ();
				return 1;
			}
			else if ((mx >= 440 && mx <= 540) && (my >= 380 + (350 / 7) && my <= 380 + (350 / 7) + 50))
			{
				ClearWordPressed ();
			}
			return 0;
		}
	}
}

