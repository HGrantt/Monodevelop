#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Tabupet;
using System.Collections.Generic;

#endregion

namespace Analfabet
{
	public class Game1 : Game
	{
		private UIt ui;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Texture2D ikonen;
		private Texture2D putword;
		private Texture2D newbricks;
		private Texture2D clear;
		private Texture2D[] font = new Texture2D[39];
		private GameHandler handler;



		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			ui = new UIt();
			base.Initialize ();	
			handler = new GameHandler (ui, ui.GetController());
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			ikonen = Content.Load<Texture2D> ("board");
			putword = Content.Load<Texture2D> ("laggord");
			clear = Content.Load<Texture2D> ("rensa");
			newbricks = Content.Load<Texture2D> ("nyabrickor");
			for (char i = 'A'; i <= 'Z'; ++i)
			{
				font [i - 'A'] = Content.Load<Texture2D> ("FONT/" + i);
			}
			font[26] = Content.Load<Texture2D>("FONT/" + "A1");
			font[27] = Content.Load<Texture2D>("FONT/" + "A2");
			font[28] = Content.Load<Texture2D>("FONT/" + "O1");
			for(int i = 0; i<10; i++)
			{
				font[29+i] = Content.Load<Texture2D>("FONT/" + i.ToString());
			}
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			//VISAR MUSEN:
			this.IsMouseVisible = true;

			handler.Update ();

			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape))
			{
				Exit ();
			}
			#endif
			// TODO: Add your update logic here			
			base.Update (gameTime);
		}


		public void drawTextString(string word, int space, int xcor, int ycor)
		{
			word = word.ToUpper ();
			for (int i = 0; i < word.Length; i++)
			{
				if (word [i] == 'Å')
				{
					spriteBatch.Draw (font[26], new Rectangle (xcor+(i*space), ycor, space, space), Color.White);
				}
		
				if (word [i] == 'Ä')
				{
					spriteBatch.Draw (font[27], new Rectangle (xcor+(i*space), ycor, space, space), Color.White);
				}

				if (word [i] == 'Ö')
				{
					spriteBatch.Draw (font [28], new Rectangle (xcor + (i * space), ycor, space, space), Color.White);
				}

				if(word[i]-'A' >= 0 && word[i]-'A' <= 25)
				{
					spriteBatch.Draw (font [word [i] - 'A'], new Rectangle (xcor + (i * space), ycor, space, space), Color.White);
				}
				if(word[i]-'0' >= 0 && word[i]-'0' <= 9)
				{
					spriteBatch.Draw (font [word [i] - '0' + 29], new Rectangle (xcor + (i * space), ycor, space, space), Color.White);
				}

			}
		}

		public void DrawBoard(int boardsize, int boardx, int boardy)
		{
			char[][] board = ui.GetBoard ();
			for (int i = 0; i < board.Length; i++)
			{
				for(int j=0;j<board.Length; j++)
				{
					drawTextString (board [i][j].ToString(), boardsize / 15, boardx + (i*boardsize/15), boardy+(j*boardsize/15));
					drawTextString (ui.GetBoardLetters()[i][j].ToString(), boardsize / 15, boardx + (i*boardsize/15), boardy+(j*boardsize/15));
				}
			}
		}
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.TransparentBlack);
			
			//TODO: Add your drawing code her
			spriteBatch.Begin();
			if (ui.GetController().GameEnd()==false)
			{
				drawTextString (ui.GetLetterSelected ().ToString (), 40, 16, 16);

				int boardX = 200;
				int boardY = 10;
				int boardsize = 350;
				int bw = 2; // Border width
				int bricksy = boardsize + 30;

				//BRÄDE BAKGRUND:
				spriteBatch.Draw (ikonen, new Rectangle (boardX, boardY, boardsize, boardsize), Color.White);

				//KNAPPAR:
				spriteBatch.Draw (putword, new Rectangle (200, bricksy + boardsize / 7, 100, 50), Color.White);
				spriteBatch.Draw (newbricks, new Rectangle (200 + 120, bricksy + boardsize / 7, 100, 50), Color.White);
				spriteBatch.Draw (clear, new Rectangle (200 + 240, bricksy + boardsize / 7, 100, 50), Color.White);

				//SKRIVA UT BRÄDET:
				var t = new Texture2D (GraphicsDevice, 1, 1);
				t.SetData (new[] { Color.White });
				spriteBatch.Draw (t, new Rectangle (boardX, boardY, bw, boardsize), Color.White); // Left
				spriteBatch.Draw (t, new Rectangle (boardX + boardsize, boardY, bw, boardsize), Color.White); // Right
				spriteBatch.Draw (t, new Rectangle (boardX, boardY, boardsize, bw), Color.White); // Top
				spriteBatch.Draw (t, new Rectangle (boardX, boardY + boardsize, boardsize, bw), Color.White); // Bottom
				for (int i = 1; i < 15; i++)
				{
					spriteBatch.Draw (t, new Rectangle (boardX, boardY + i * boardsize / 15, boardsize, bw), Color.White);
					spriteBatch.Draw (t, new Rectangle (boardX + i * boardsize / 15, boardY, bw, boardsize), Color.White);
				}

				//SKRIVA UT SPELARENS BRICKOR:
				spriteBatch.Draw (t, new Rectangle (boardX, bricksy, boardsize, bw), Color.White);
				spriteBatch.Draw (t, new Rectangle (boardX, bricksy + boardsize / 7, boardsize, bw), Color.White);
				for (int i = 0; i < 8; i++)
				{
					spriteBatch.Draw (t, new Rectangle (boardX + i * boardsize / 7, bricksy, bw, boardsize / 7), Color.White);
				}

				drawTextString (ui.GetPlayerLetters (), boardsize / 7, boardX, bricksy);
				String name = ui.GetPlayerName ();
				String points = ui.GetScore ().ToString ();
		
				drawTextString (points, 60 / points.Length, boardX + boardsize + 40, bricksy - 100);
				drawTextString (name, 160 / name.Length, boardX + boardsize + 40, bricksy);

				DrawBoard (350, 200, 10);
			}
			else
			{
				string winner = ui.GetWinnerName ();
				drawTextString (winner + " won", 300 / winner.Length+4, 100, 100);
			}

			spriteBatch.End ();
			base.Draw (gameTime);
		}
	}
}

