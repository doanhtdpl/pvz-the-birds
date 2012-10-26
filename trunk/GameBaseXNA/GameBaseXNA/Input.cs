using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameBaseXNA
{
    /// <summary>
    /// Represent Game keyboard
    /// Need be updated per game loop
    /// </summary>
    public static class GKeyBoard
    {
        /// <summary>
        /// Last state of keyboard
        /// </summary>
        static public KeyboardState LastState { get; set; }
        /// <summary>
        /// Current state of keyboard
        /// </summary>
        static public KeyboardState State { get; set; }

        static GKeyBoard()
        {
            LastState = Keyboard.GetState(PlayerIndex.One);
            State = Keyboard.GetState(PlayerIndex.One);
        }

        /// <summary>
        /// Update keyboard state
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        static public void Update(GameTime gameTime)
        {
            LastState = State;
            State = Keyboard.GetState(PlayerIndex.One);
        }

        /// <summary>
        /// Return all pressed (is down) keys
        /// </summary>
        static public Keys[] PressedKeys
        {
            get { return State.GetPressedKeys(); }
        }

        /// <summary>
        /// Check a specified key is down or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key is down</returns>
        static public bool IsKeyDown(Keys key)
        {
            return State.IsKeyDown(key);
        }

        /// <summary>
        /// Check a specified key is up or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key is up</returns>
        static public bool IsKeyUp(Keys key)
        {
            return State.IsKeyUp(key);
        }

        /// <summary>
        /// Check a specified key is pressed up or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been pressed up</returns>
        static public bool IsKeyPressedUp(Keys key)
        {
            return (State.IsKeyUp(key) && LastState.IsKeyDown(key));
        }

        /// <summary>
        /// Check a specified key is pressed or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been pressed</returns>
        static public bool IsKeyPressed(Keys key)
        {
            return IsKeyPressedUp(key);
        }

        /// <summary>
        /// Check a specified key is pressed down or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been pressed down</returns>
        static public bool IsKeyPressedDown(Keys key)
        {
            return (State.IsKeyDown(key) && LastState.IsKeyUp(key));
        }

        /// <summary>
        /// Check a specified key is ctrl-pressed or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been ctrl-pressed</returns>
        static public bool IsControlAndKeyPressed(Keys key)
        {
            if (!State.IsKeyDown(Keys.LeftControl) && !(State.IsKeyDown(Keys.RightControl)))
                return false;

            return (State.IsKeyUp(key) && LastState.IsKeyDown(key));
        }

        /// <summary>
        /// Check a specified key is alt-pressed or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been alt-pressed</returns>
        static public bool IsAltAndKeyPressed(Keys key)
        {
            if (!State.IsKeyDown(Keys.LeftAlt) && !(State.IsKeyDown(Keys.RightAlt)))
                return false;

            return (State.IsKeyUp(key) && LastState.IsKeyDown(key));
        }

        /// <summary>
        /// Check a specified key is shift-pressed or not
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if specified key have just been shift-pressed</returns>
        static public bool IsShiftAndKeyPressed(Keys key)
        {
            if (!State.IsKeyDown(Keys.LeftShift) && !(State.IsKeyDown(Keys.RightShift)))
                return false;

            return (State.IsKeyUp(key) && LastState.IsKeyDown(key));
        }
    }

    /// <summary>
    /// Represent game mouse
    /// </summary>
    public static class GMouse
    {
        /// <summary>
        /// Last state of mouse
        /// </summary>
        static public MouseState LastState { get; set; }

        /// <summary>
        /// Current state of mouse
        /// </summary>
        static public MouseState State { get; set; }

        /// <summary>
        /// Double click time
        /// </summary>
        static public double DoubleClickDelay { get; set; }

        #region Double Click Fields
        static private double lastLClick = double.MinValue;
        static private double lastRClick = double.MinValue;
        static private bool LdblClick = false;
        static private bool RdblClick = false;
        static private double currentTime = 0.0;
        #endregion

        static GMouse()
        {
            LastState = Mouse.GetState();
            State = Mouse.GetState();
            DoubleClickDelay = 200f;
        }

        /// <summary>
        /// Update it self
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        static public void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            LastState = State;
            State = Mouse.GetState();

            #region Double Click
            if (IsLeftButtonClicked)
            {
                if (!LdblClick && (currentTime - lastLClick <= DoubleClickDelay))
                    LdblClick = true;
                else
                {
                    lastLClick = currentTime;
                    LdblClick = false;
                }
            }
            else
                LdblClick = false;

            if (IsRightButtonClicked)
            {
                if (!RdblClick && (currentTime - lastLClick <= DoubleClickDelay))
                    RdblClick = true;
                else
                {
                    lastRClick = currentTime;
                    RdblClick = false;
                }
            }
            else
                RdblClick = false;

            #endregion
        }

        /// <summary>
        /// Set Mouse Position
        /// </summary>
        /// <param name="x">X co-ordinate of mouse position</param>
        /// <param name="y">Y co-ordinate of mouse position</param>
        static public void SetPosition(int x, int y)
        {
            Mouse.SetPosition(x, y);
        }

        /// <summary>
        /// X co-ordinate of mouse position
        /// </summary>
        static public int X
        {
            get { return State.X; }
        }

        /// <summary>
        /// Y co-ordinate of mouse position
        /// </summary>
        static public int Y
        {
            get { return State.Y; }
        }

        /// <summary>
        /// [Read-only] Return X-coordinate distance of mouse movement
        /// </summary>
        static public int DX
        {
            get { return State.X - LastState.X; }
        }

        /// <summary>
        /// [Read-only] Return Y-coordinate distance of mouse movement
        /// </summary>
        static public int DY
        {
            get { return State.Y - LastState.Y; }
        }

        /// <summary>
        /// [Read-only] Return distance of mouse movement
        /// </summary>
        static public Vector2 MovedDistance
        {
            get { return new Vector2(State.X - LastState.X, State.Y - LastState.Y); }
        }

        /// <summary>
        /// Co-ordinate of mouse
        /// </summary>
        static public Vector2 MousePosition
        {
            get { return new Vector2(State.X, State.Y); }
            set { Mouse.SetPosition((int)value.X, (int)value.Y); Update(new GameTime()); }
        }

        /// <summary>
        /// Co-ordinate of last mouse
        /// </summary>
        static public Vector2 LastMousePosition
        {
            get { return new Vector2(LastState.X, LastState.Y); }
        }

        /// <summary>
        /// Left Button Current State
        /// </summary>
        static public ButtonState LeftButton
        {
            get { return State.LeftButton; }
        }

        /// <summary>
        /// Right Button Current State
        /// </summary>
        static public ButtonState RightButton
        {
            get { return State.RightButton; }
        }

        /// <summary>
        /// Middle Button Current State
        /// </summary>
        static public ButtonState MiddleButton
        {
            get { return State.MiddleButton; }
        }

        /// <summary>
        /// ScrollWhell Value
        /// </summary>
        static public int ScrollWheelValue
        {
            get { return State.ScrollWheelValue; }
        }

        /// <summary>
        /// Return true if left button is clicked
        /// </summary>
        static public bool IsLeftButtonClicked
        {
            get { return ((State.LeftButton == ButtonState.Released) && (LastState.LeftButton == ButtonState.Pressed)); }
        }

        /// <summary>
        /// Return true if right button is clicked
        /// </summary>
        static public bool IsRightButtonClicked
        {
            get { return ((State.RightButton == ButtonState.Released) && (LastState.RightButton == ButtonState.Pressed)); }
        }

        /// <summary>
        /// Return true if middle button is clicked
        /// </summary>
        static public bool IsMiddleButtonClicked
        {
            get { return ((State.MiddleButton == ButtonState.Released) && (LastState.MiddleButton == ButtonState.Pressed)); }
        }

        /// <summary>
        /// Return true if left button is down
        /// </summary>
        static public bool IsLeftButtonDown
        {
            get { return (State.LeftButton == ButtonState.Pressed); }
        }

        /// <summary>
        /// Return true if right button is down
        /// </summary>
        static public bool IsRightButtonDown
        {
            get { return (State.RightButton == ButtonState.Pressed); }
        }

        /// <summary>
        /// Return true if middle button is down
        /// </summary>
        static public bool IsMiddleButtonDown
        {
            get { return (State.MiddleButton == ButtonState.Pressed); }
        }

        /// <summary>
        /// Return true if left button is up
        /// </summary>
        static public bool IsLeftButtonUp
        {
            get { return (State.LeftButton == ButtonState.Released); }
        }

        /// <summary>
        /// Return true if right button is up
        /// </summary>
        static public bool IsRightButtonUp
        {
            get { return (State.RightButton == ButtonState.Released); }
        }

        /// <summary>
        /// Return true if middle button is up
        /// </summary>
        static public bool IsMiddleButtonUp
        {
            get { return (State.MiddleButton == ButtonState.Released); }
        }

        /// <summary>
        /// Return true if left button is double click
        /// </summary>
        static public bool IsLeftButtonDoubleClick
        {
            get
            {
                return LdblClick;
            }
        }

        /// <summary>
        /// Return true if right button is double click
        /// </summary>
        static public bool IsRightButtonDoubleClick
        {
            get
            {
                return RdblClick;
            }
        }
    }

    public static class GTouch
    {
        static public TouchCollection State;

        static bool IsPressed
        {
            get
            {
                foreach (TouchLocation tl in State)
                {
                    if (tl.State == TouchLocationState.Pressed)
                        return true;
                }

                return false;
            }
        }

        static bool IsMoved
        {
            get
            {
                foreach (TouchLocation tl in State)
                {
                    if (tl.State == TouchLocationState.Moved)
                        return true;
                }

                return false;
            }
        }

        static TouchLocation TouchLocation
        {
            get
            {
                return State[0];
            }
        }

        static Vector2 Moved
        {
            get
            {
                foreach (TouchLocation tl in State)
                {
                    if (tl.State == TouchLocationState.Moved)
                    {
                        TouchLocation lastPos;
                        tl.TryGetPreviousLocation(out lastPos);
                        Vector2 moved = tl.Position - lastPos.Position;
                    }
                }

                throw new InvalidOperationException();
            }
        }

        static Vector2 Position
        {
            get
            {
                return State[0].Position;
            }
        }

        static TouchLocationState Type
        {
            get
            {
                return State[0].State;
            }
        }
    }
}
