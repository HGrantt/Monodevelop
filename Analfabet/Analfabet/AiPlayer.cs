using System;
using System.Collections.Generic;

namespace Analfabet
{
	public class StringLengthComparer : Comparer<object>
	{
		public override int Compare (object x, object y)
		{
			return ((string)x).Length - ((string)y).Length;
		}
	}
	public class AiPlayer : AbstractPlayer
	{
		List<string> vertResults = new List<string>();
		List<int[]> vertCords = new List<int[]>();
		List<string> horResults = new List<string> ();
		List<int[]> horCords = new List<int[]> ();


		SortedList<string,int[]> vertDone = new SortedList<string, int[]> (new StringLengthComparer());

		SortedList<string,int[]> horDone = new SortedList<string, int[]> (new StringLengthComparer());

		UIt uit;

		public override int Play(UIt ui)
		{
			uit = ui;
			vertCords.Clear ();
			vertResults.Clear ();
			horCords.Clear ();
			horResults.Clear ();
			horDone.Clear ();
			vertDone.Clear ();

			if (playwordchangeturn ())
			{
				return 1;
			} 
			else
			{
				uit.ThrowLettersPressed ();
				return 1;
			}
		}
		private bool playwordchangeturn()
		{
			findHorBoardWords();
			findVertBoardWords();
			for (int x = 0; x < horResults.Count; x++)
			{
				if (getAndPlayWords (horResults [x], horCords [x] [0], horCords [x] [1], false))
				{
					return true; 
				}
			}
			for (int x = 0; x < vertResults.Count; x++)
			{
				if (getAndPlayWords (vertResults [x], vertCords [x] [0], vertCords [x] [1], true))
				{
					return true;
				}
			}
			return false;
		}

		private void findVertBoardWords()
		{
			char[][] board = uit.GetBoard ();
			String temp = "";

			for (int x = 0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					if (board [x] [y] != '#')
					{
						temp += board [x] [y];
					}
					else if (temp.Length > 0)
					{
						vertResults.Add (temp);
						vertCords.Add (new int[] { x, y - temp.Length });
						temp = "";
					}
				}
			}
		}
		private void findHorBoardWords()
		{
			char[][] board = uit.GetBoard ();
			String temp = "";

			for (int x = 0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					if (board [y] [x] != '#')
					{
						temp += board [y] [x];
					}
					else if (temp.Length > 0)
					{
						horResults.Add (temp);
						horCords.Add (new int[] { y - temp.Length, x });
						temp = "";
					}
				}
			}
		}
		private bool getAndPlayWords(string onBoard, int startX, int startY, bool isvert)
		{
			string[] lines = System.IO.File.ReadAllLines("clean.txt");
			string myletters = uit.GetPlayerLetters ();
			string word = "";
			bool wordworks = false;
			foreach (string line in lines)
			{
				if (line.Contains (onBoard) && line.Length < (onBoard.Length + myletters.Length))
				{
					word = RemoveString (line, onBoard);

					for (int x = 0; x < word.Length; x++)
					{
						if (myletters.Contains (word [x].ToString()))
						{
							myletters = RemoveString (myletters, word [x].ToString ());
							wordworks = true;
						}
						else
						{
							myletters = uit.GetPlayerLetters ();
							wordworks = false;
							break;
						}
					}
					if (wordworks)
					{
						if (isvert)
						{
							if (!vertDone.ContainsKey (word))
							{
								vertDone.Add (word, new int[]{ startX, startY - line.IndexOf (onBoard [0]) });
							}
						}
						else
						{
							if (!horDone.ContainsKey (word))
							{
								horDone.Add (word, new int[]{ startX - line.IndexOf (onBoard [0]), startY });
							}
						}
					}
				}
			}

			for (int i = horDone.Count-1; i>=0; --i)
			{
				//Console.WriteLine ("HOR: " + horDone.Keys [i]);
				if(playWord(horDone.Keys[i],horDone.Values[i][0],horDone.Values[i][1], false))
				{
					return true;
				}
			}
			for (int i = vertDone.Count-1; i >=0; --i)
			{
				//Console.WriteLine ("VERT: " + vertDone.Keys [i]);
				if(playWord(vertDone.Keys[i],vertDone.Values[i][0],vertDone.Values[i][1], true))
				{
					return true;
				}
			}
			return false;
		}

		private string RemoveString(string todissemble, string toremove)
		{
			if(toremove.Length==1)
			{
				todissemble = todissemble.Remove (todissemble.IndexOf (toremove [0]),1);
			}
			else if(toremove.Length > 1)
			{
				todissemble = todissemble.Replace(toremove,"");
			}
			return todissemble;
		}
		
		private bool playWord(string word, int startX, int startY, bool isvert)
		{
			if (!(startX >= 0 && startX < 15 && startY >= 0 && startY < 15))
			{
				return false;
			}
			
			string myletters = uit.GetPlayerLetters ();
			int f = 0;
			for (int i= 0; i < word.Length; i++)
			{
				if (myletters.IndexOf (word [i]) == -1)
				{
					return false;
				}
				uit.LetterSqarePressed (myletters.IndexOf (word [i]));
				if (isvert)
				{
					while (startY + i + f < 15 && startY + i + f >= 0)
					{
						if (uit.GetBoard () [startX] [startY + i + f] != '#')
						{
							f++;
						}
						else
						{
							break;
						}
					}
					if (startY + i + f >= 15)
					{
						return false;
					}
					uit.GridSquarePressed (startX, startY + i + f);
				}
				else
				{
					while (startX + i + f < 15 && startX + i + f >= 0)
					{
						if (uit.GetBoard () [startX + i + f] [startY] != '#')
						{
							f++;
						} 
						else
						{
							break;
						}
					}
					if (startX + i + f >= 15)
					{
						return false;
					}
					uit.GridSquarePressed (startX+i+f, startY);
				}
			}
			return uit.PutWordPressed ();
		}

	}
}

