using System;
using Tabupet;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Analfabet
{
	public class State
	{
		public char[][] board;
		public int playerturn;
		public List<Player> players;

		public State ()
		{
			board = new char[15][];
			for (int i = 0; i < 15; i++)
			{
				board[i] = new char[15];
				for (int p = 0; p < 15; p++)
				{
					board[i][p] = '#';
				}
			}
		}

		public void getState(Controller ctrl)
		{
			board = ctrl.GetBoard ();
			playerturn = ctrl.getTurn ();
			players = ctrl.GetPlayers ();
		}

		public void saveToXML()
		{
			XDocument xdoc = new XDocument ();
			string xmlShell = @"<save><board></board><players></players><turn></turn> </save>";
			xdoc = XDocument.Parse (xmlShell);
			for (int x = 0; x < 15; x++)
			{
				XElement row = new XElement ("row");
				for (int y = 0; y < 15; y++)
				{
					row.Add(new XElement("column",board[x][y]));
				}
				xdoc.Element("save").Element ("board").Add (row);
			}
			for (int i = 0; i < players.Count; i++)
			{
				XElement player = new XElement ("player");
				player.Add (new XElement ("name", players [i].GetName ()));
				player.Add (new XElement ("bricks", players [i].GetLetters ()));
				player.Add (new XElement ("score", players [i].GetPoints()));
				xdoc.Element("save").Element("players").Add(player);
			}
			xdoc.Element("save").Element("turn").SetValue (playerturn);
			xdoc.Save ("Savestate.xml");
		}

		public void loadFromXML()
		{
			XDocument load = XDocument.Load ("Savestate.xml");
			int xcount = 0;
			int ycount = 0;
			var rows = from item in load.Element ("save").Element("board").Descendants ("row") select item;
			foreach (var x in rows)
			{
				var columns = from item in x.Descendants("column") select item;
				ycount = 0;
				foreach (var y in columns)
				{
					board [xcount] [ycount] = y.Value.ToString()[0];
					ycount++;
				}
				xcount++;
			}
				
			var xplayers = from item in load.Element ("save").Element ("players").Descendants ("player")
			              select item;
			players = new List<Player> ();
			foreach (var p in xplayers)
			{
				int points = int.Parse(p.Element ("score").Value.ToString());
				string bricks = p.Element ("bricks").Value.ToString ();
				string name = p.Element ("name").Value.ToString ();
				players.Add (new Player (name, points, bricks));
			}

			playerturn = int.Parse (load.Element ("save").Element ("turn").Value.ToString ());
		}

	}
}

