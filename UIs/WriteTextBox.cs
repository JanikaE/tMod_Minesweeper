﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Minesweeper.UIs
{
    internal class WriteTextBox : UITextBox
    {
        private bool Enabled = false;
        private string text;
        private bool[] inputFlag = {
            false, false, false, false, false,
            false, false, false, false, false,
            false
        };

        private readonly Keys[] inputKey = {
            Keys.NumPad0,
            Keys.NumPad1,
            Keys.NumPad2,
            Keys.NumPad3,
            Keys.NumPad4,
            Keys.NumPad5,
            Keys.NumPad6,
            Keys.NumPad7,
            Keys.NumPad8,
            Keys.NumPad9,
            Keys.Back,
        };

        public WriteTextBox(string text, float textScale = 1, bool large = false) : base(text, textScale, large)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (ContainsPoint(Main.MouseScreen))
            {
                if (Main.mouseLeft)
                    Enabled = true;
            }
            else
            {
                if (Main.mouseLeft)
                    Enabled = false;
            }
            
            if (Enabled)
            {
                ShowInputTicker = true;
                text = Text;
                foreach(Keys key in inputKey)
                {
                    if (Main.keyState.IsKeyDown(key))
                    {
                        inputFlag[KeyToNum(key)] = true;
                    }
                    if (Main.keyState.IsKeyUp(key) && inputFlag[KeyToNum(key)])
                    {
                        if (KeyToNum(key) == 10)
                        {
                            if (text.Length > 0)
                            {
                                text = text.Substring(0, text.Length - 1);
                            }                            
                        }
                        else
                        {
                            if (text.Length < 4)
                            {
                                text = String.Concat(Text, KeyToNum(key).ToString());
                            }
                        }
                        Clear();
                        Write(text);
                        inputFlag[KeyToNum(key)] = false;
                    }
                }           
            }
            else
            {
                ShowInputTicker = false;
            }            
        }

        public void Clear()
        {
            while(Text.Length > 0)
            {
                Backspace();
            }
        }

        public int KeyToNum(Keys key)
        {
            return key switch
            {
                Keys.NumPad0 => 0,
                Keys.NumPad1 => 1,
                Keys.NumPad2 => 2,
                Keys.NumPad3 => 3,
                Keys.NumPad4 => 4,
                Keys.NumPad5 => 5,
                Keys.NumPad6 => 6,
                Keys.NumPad7 => 7,
                Keys.NumPad8 => 8,
                Keys.NumPad9 => 9,
                Keys.Back => 10,
                _ => -1,
            };
        }
    }
}
