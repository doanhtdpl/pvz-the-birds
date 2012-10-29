using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;

namespace PlantsVsZombies.Controls
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Control : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Attributes
        // Sprite Batch to Draw
        protected SpriteBatch spriteBatch;

        // Control Name
        protected string assetName = string.Empty;

        // Control Background
        protected Sprite controlBackground;
        protected Vector2 position = Vector2.Zero;
        protected Rectangle bound = Rectangle.Empty;
        private static int countControl = 0;

        // Flag for update and draw itself
        protected bool enable = true;
        protected bool visible = true;

        #endregion

        #region Properties
        public string AssetName
        {
            get { return this.assetName; }
            set { this.assetName = value; }
        }
        public Sprite ControlBackground
        {
            get { return this.controlBackground; }
            set
            {
                this.controlBackground = value;
                this.bound.Width = controlBackground.Texture.Width;
                this.bound.Height = controlBackground.Texture.Height;
            }
        }
        public Rectangle Bound
        {
            get { return this.bound; }
            set { this.bound = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                this.bound.X = (int)position.X;
                this.bound.Y = (int)position.Y;
                this.controlBackground.Position = this.position;
            }

        }
        public float PositionX
        {
            get { return this.position.X; }
            set
            {
                this.position.X = value;
                this.bound.X = (int)position.X;
                this.controlBackground.Position = this.position;
            }
        }
        public float PositionY
        {
            get { return this.position.Y; }
            set
            {
                this.position.Y = value;
                this.bound.Y = (int)position.Y;
                this.controlBackground.Position = this.position;
            }
        }

        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        public bool Visibled
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        #endregion

        #region Initialize

        // Constructor to create new control
        public Control(Game game, Sprite controlBackground, Vector2 position)
            : base(game)
        {
            this.controlBackground = controlBackground;
            this.position = position;
            this.Initialize();
        }
        public Control(Game game, string pathImage, Vector2 position)
            : base(game)
        {
            if (pathImage != string.Empty)
                this.controlBackground = SpriteBank.GetSprite(pathImage);
            this.position = position;
            this.Initialize();
        }
        public Control(Control control)
            : base(control.Game)
        {
            this.controlBackground = control.controlBackground;
            this.position = control.position;
            this.Initialize();
        }

        // Allows the game component to perform any initialization it needs to before starting
        // to run.  This is where it can query for any required services and load content.
        public override void Initialize()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            if (controlBackground != null)
            {
                this.controlBackground.Position = this.position;
                this.bound = new Rectangle((int)position.X, (int)position.Y, controlBackground.Texture.Width, controlBackground.Texture.Height);
            }
            countControl++;

            assetName = "Control_" + countControl.ToString();

            base.Initialize();
        }

        #endregion

        #region Methods

        // Allows the game component to update itself.
        public override void Update(GameTime gameTime)
        {
            this.CheckBound();
            if (this.enable)
                controlBackground.Update(gameTime);
            base.Update(gameTime);
        }

        // Method check for enable the object
        protected virtual void CheckBound()
        {
        }

        // Allow the game component to render it on screen
        public override void Draw(GameTime gameTime)
        {
            if (this.visible)
            {
                if (controlBackground != null)
                    controlBackground.Draw(gameTime);
                base.Draw(gameTime);
            }
        }

        // Allow the game component remove itself
        public virtual void Remove()
        {
            this.Dispose();
        }
        #endregion
    }

    public class Button : Control
    {
        #region Attributes

        // Button state structure
        public enum ButtonStated
        {
            Down,
            Up,
            Normal,
        }

        // Sprite when mouse (touch) affect to object
        protected Sprite buttonClicked;

        // The button state before and after
        protected ButtonStated state = ButtonStated.Normal;
        protected ButtonStated lastState = ButtonStated.Normal;

        // Button's event
        public event EventHandler Clicked;
        public event EventHandler Released;

        // Flag button state
        protected bool isClicked = false;

        #endregion

        #region Properties

        public new Vector2 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                this.bound.X = (int)position.X;
                this.bound.Y = (int)position.Y;
                // Set position for button's sprite
                this.controlBackground.Position = this.position;
                this.buttonClicked.Position = this.position;
            }
        }

        public Vector2 ButtonSize
        {
            get
            {
                return this.controlBackground.Size;
            }
            set
            {
                this.ControlBackground.Size = value;
                this.buttonClicked.Size = value;
            }
        }
        public float ButtonSizeX
        {
            set
            {
                this.ControlBackground.SizeX = value;
                this.buttonClicked.SizeX = value;
            }
        }
        public float ButtonSizeY
        {
            set
            {
                this.ControlBackground.SizeY = value;
                this.buttonClicked.SizeY = value;
            }
        }

        public Sprite ButtonClicked
        {
            get { return this.buttonClicked; }
            set { this.buttonClicked = value; }
        }

        #endregion

        #region Initialize

        // Constructor create new Button
        public Button(Game game, Sprite buttonBackground, Sprite buttonClicked, Vector2 position)
            : base(game, buttonBackground, position)
        {
            this.buttonClicked = buttonClicked;
            this.Initialize();
        }
        public Button(Button button)
            : base(button)
        {
            this.state = button.state;
            this.Initialize();
        }

        // Initialize the button Over + Clicked
        public new void Initialize()
        {
            buttonClicked.Position = this.Position;
        }

        #endregion

        #region Methods

        // Update button's state
        public override void Update(GameTime gameTime)
        {
            TreatEvent();

            base.Update(gameTime);
        }

        // Draw button background and button clicked background
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (this.visible)
            {
                if (isClicked)
                    buttonClicked.Draw(gameTime);
            }
        }

        // Check when touch 
        protected override void CheckBound()
        {
            if (GMouse.MousePosition.X >= bound.X && GMouse.MousePosition.X <= bound.X + bound.Width
                && GMouse.MousePosition.Y >= bound.Y && GMouse.MousePosition.Y <= bound.Y + bound.Height)
            {

            }
            base.CheckBound();
        }

        // Check and Throw button's events
        public virtual void TreatEvent()
        {
            if (this.enable)
            {
                lastState = state;
                this.CheckState();

                if (this.lastState == ButtonStated.Down && this.state == ButtonStated.Up)
                {
                    isClicked = true;
                    if (Clicked != null)
                        Clicked(this, EventArgs.Empty);
                }
                else if (this.lastState == ButtonStated.Up)
                {
                    isClicked = false;
                    if (Released != null)
                        Released(this, EventArgs.Empty);
                }
            }
        }

        // Check about button's state
        public virtual void CheckState()
        {
            if (GMouse.MousePosition.X >= bound.X && GMouse.MousePosition.X <= bound.X + bound.Width
                && GMouse.MousePosition.Y >= bound.Y && GMouse.MousePosition.Y <= bound.Y + bound.Height)
            {
                if (GMouse.IsLeftButtonDown)
                {
                    this.state = ButtonStated.Down;
                }
                else if (this.state == ButtonStated.Down && GMouse.IsLeftButtonUp)
                {
                    this.state = ButtonStated.Up;
                }
            }
            else
                this.state = ButtonStated.Normal;
        }

        #endregion
    }

    public class ListView : Control
    {
        public class ControlGriding
        {
            // Fields
            string lastKey = string.Empty;
            float align;
            float width;
            float height;

            Vector2 position;
            Dictionary<string ,Rectangle> maps;

            // Properties
            public Vector2 Position
            {
                get { return this.position; }
                set
                {
                    float movementX = value.X - position.X;
                    float movementY = value.Y - position.Y;
                    this.position = value;

                    foreach (KeyValuePair<string, Rectangle> rect in maps)
                    {
                        maps[rect.Key] = new Rectangle((int)(rect.Value.X + movementX), (int)(rect.Value.Y + movementY), 
                                                                rect.Value.Width, rect.Value.Height);
                    }
                }
            }
            public float Width
            {
                get { return this.width; }
                set { this.width = value; this.ReSort(); }
            }
            public float Height
            {
                get { return this.height; }
                set { this.height = value; this.ReSort(); }
            }
            public float Align
            {
                get { return this.align; }
                set
                {
                    float changed = value - align;
                    this.align = value;
                    foreach (KeyValuePair<string, Rectangle> rect in maps)
                    {
                        maps[rect.Key] = new Rectangle((int)(rect.Value.X + changed), (int)(rect.Value.Y + changed),
                                                                rect.Value.Width, rect.Value.Height);
                    }
                }
            }

            public ControlGriding(Vector2 position, float width, float height, float align)
            {
                this.position = position;
                this.width = width;
                this.height = height;

                this.align = align;

                maps = new Dictionary<string, Rectangle>();
            }

            public virtual bool AddItem(Control control)
            {
                if (maps.Count == 0)
                {
                    // Make sure the first control contained in the griding
                    if(control.ControlBackground.Width + 2 * align < width &&
                        control.ControlBackground.Height + align < height)
                    {
                        Rectangle newRect = new Rectangle((int)(align + position.X), (int)(align + position.Y), 
                                 (int) control.ControlBackground.Width, (int)control.ControlBackground.Height);

                        // Update control position
                        control.PositionX = newRect.X;
                        control.PositionY = newRect.Y;

                        maps.Add(control.AssetName, newRect);
                        lastKey = control.AssetName;

                        return true;
                    }
                }
                else
                {
                    Rectangle lastRect = maps[lastKey];
                    if ( (lastRect.X + lastRect.Width + control.ControlBackground.Width + align) < (position.X + width) )
                    {
                        Rectangle newRect = new Rectangle((int)(align + lastRect.X + lastRect.Width), lastRect.Y,
                                    (int)control.ControlBackground.Width, (int)control.ControlBackground.Height);

                        // Update control position
                        control.PositionX = newRect.X;
                        control.PositionY = newRect.Y;

                        maps.Add(control.AssetName, newRect);
                        lastKey = control.AssetName;

                        return true;
                    }
                    else // enter to new line
                    {
                        if ((lastRect.Y + lastRect.Height + control.ControlBackground.Height + align) < (position.Y + height))
                        {
                            Rectangle newRect = new Rectangle((int)(align + position.X), (int)(lastRect.Y + lastRect.Height + align),
                                                                (int)control.ControlBackground.Width, (int)control.ControlBackground.Height);

                            // Update control position
                            control.PositionX = newRect.X;
                            control.PositionY = newRect.Y;

                            maps.Add(control.AssetName, newRect);
                            lastKey = control.AssetName;

                            return true;
                        }
                    }
                }
                return false;
            }

            public void RemoveAt(Control control)
            {
                maps.Remove(control.AssetName);
            }

            public void Remove()
            {
                maps.Clear();
            }

            protected void ReSort()
            {
                Vector2 lastRect = new Vector2(position.X, position.Y + align);
                foreach (KeyValuePair<string, Rectangle> rect in maps)
                {
                    if ((lastRect.X + rect.Value.Width + align) < (position.X + width))
                    {
                        maps[rect.Key] = new Rectangle((int)(lastRect.X + align), (int)(lastRect.Y),
                                                                rect.Value.Width, rect.Value.Height);
                        lastRect.X = maps[rect.Key].X + rect.Value.Width;
                        lastRect.Y = maps[rect.Key].Y + rect.Value.Height;
                    }
                    else if ((lastRect.Y + 2 * rect.Value.Height + align) < (position.Y + height))
                    {
                        maps[rect.Key] = new Rectangle((int)(position.X + align), (int)(lastRect.Y + rect.Value.Height + align),
                                                               rect.Value.Width, rect.Value.Height);
                        lastRect.X = maps[rect.Key].X + rect.Value.Width;
                        lastRect.Y = maps[rect.Key].Y + rect.Value.Height;
                    }
                }
            }

        }

        #region Attributes

        public List<Control> listControl;
        protected int count = 0;

        // Griding for List
        protected ControlGriding griding;

        public new Sprite ControlBackground
        {
            get
            {
                return this.controlBackground;
            }
            set
            {
                base.ControlBackground = value;
                this.griding.Width = this.controlBackground.Width;
                this.griding.Height = this.controlBackground.Height;
            }
        }
        #endregion

        #region Inittialize
        public ListView(Game game)
            : base(game, (Sprite) null, Vector2.Zero)
        {
            this.griding = new ControlGriding(Vector2.Zero, 0f, 0f, 0f);
            this.listControl = new List<Control>();
            this.Initialize();
        }

        public ListView(Game game, Sprite controlSprite, Vector2 position, float align)
            : base(game, controlSprite, position)
        {
            this.griding = new ControlGriding(position, controlSprite.Width, controlSprite.Height, align);
            listControl = new List<Control>();
            this.Initialize();
        }
        public ListView(ListView listView)
            : base(listView)
        {
            this.griding = new ControlGriding(listView.griding.Position, listView.griding.Width, listView.griding.Height, listView.griding.Align);
            this.listControl = new List<Control>(listView.listControl);
            this.Initialize();
        }

        public new void Initialize()
        {
            count++;
            if (assetName == null)
                this.assetName = "ListView_" + count.ToString();
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            foreach (Control control in listControl)
            {
                if (control.Position.X + control.ControlBackground.Texture.Width > Bound.Width ||
                    control.Position.Y + control.ControlBackground.Texture.Height > Bound.Height)
                    control.Enable = false;
                else
                    control.Enable = true;
            }
            List<Control> copied = new List<Control>(listControl);
            foreach (Control control in copied)
            {
                control.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (Control control in listControl)
            {
                control.Draw(gameTime);
            }
        }

        public virtual bool Add(Control control)
        {
            if (griding.AddItem(control))
                listControl.Add(control);
            return true;
        }

        public virtual void RemoveControl(Control control)
        {
            listControl.Remove(control);
            griding.RemoveAt(control);

            ReSort();
        }

        protected void ReSort()
        {
            griding.Remove();
            foreach(Control control in listControl)
            {
                griding.AddItem(control);
            }
        }

        public override void Remove()
        {
            foreach (Control control in listControl)
            {
                control.Remove();
            }
            this.listControl.Clear();
            griding.Remove();
        }
        #endregion

    }

    public class TextBox : Control
    {
        #region Attributes

        Sprite caret;
        Vector2 caretPosition;
        Color textColor;

        string inputText = string.Empty;
        SpriteFont textFont;

        int count = -1;
        bool allowInput = false;

        protected int currentTime = 0;
        protected int lastTime = 0;
        protected int delay1 = 50;

        protected int sizeOfText;

        #endregion

        #region Properties

        public string InputText
        {
            get { return this.inputText; }
            set { this.inputText = value; }
        }

        #endregion

        #region Initialize

        public TextBox(Game game, Sprite controlBackground, Vector2 position,
            Sprite caret, SpriteFont textFont, Vector2 caretPosition, Color textColor)
            : base(game, controlBackground, position)
        {
            this.caret = caret;
            this.textFont = textFont;
            this.caretPosition = caretPosition;
            this.textColor = textColor;
            this.Initialize();
        }
        public TextBox(TextBox textbox)
            : base(textbox)
        {
            this.caret = textbox.caret;
            this.textFont = textbox.textFont;
            this.Initialize();
        }

        public new void Initialize()
        {
            base.Initialize();
            if (textFont == null)
                this.textFont = Game.Content.Load<SpriteFont>("font");
            this.sizeOfText = this.controlBackground.Texture.Width / ((int)textFont.MeasureString("M").X) - 2;
        }

        #endregion

        #region Method

        public override void Update(GameTime gameTime)
        {
            enableInput();
            InputTextByHand();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (this.visible)
            {
                spriteBatch.Begin();
                this.spriteBatch.DrawString(textFont, Parse(inputText), caretPosition, textColor);
                spriteBatch.End();
            }

            if (this.allowInput)
            {
                if ((currentTime - lastTime) >= delay1)
                    lastTime = currentTime;
                else
                    this.caret.Draw(gameTime);
                currentTime++;
            }
        }

        // Check for mouse to enable text
        private void enableInput()
        {
            if (GMouse.MousePosition.X >= bound.X && GMouse.MousePosition.X <= bound.X + bound.Width
                && GMouse.MousePosition.Y >= bound.Y && GMouse.MousePosition.Y <= bound.Y + bound.Height)
            {
                if (GMouse.IsLeftButtonClicked)
                    this.allowInput = true;
            }
            else
            {
                if (GMouse.IsLeftButtonDoubleClick)
                    this.allowInput = false;
            }
        }

        // Get Pressed Key to Text
        private void InputTextByHand()
        {
            ++currentTime;

            Keys[] pressedKey = GKeyBoard.State.GetPressedKeys();

            if (!this.enable)
                return;
            foreach (Keys key in pressedKey)
            {
                #region Check key 'back, left, right'
                if (this.allowInput && GKeyBoard.LastState.IsKeyUp(key))
                {
                    if (key == Keys.Back && count >= 0)
                    {
                        this.UpdateCaret(false);
                        inputText = inputText.Remove(count, 1);
                        --count;
                    }
                    else if (key == Keys.Left && count >= 0)
                    {
                        if (count >= 0)
                        {
                            this.UpdateCaret(false);
                            --count;
                        }
                    }
                    else if (key == Keys.Right)
                    {
                        ++count;
                        if (count >= inputText.Length)
                            count = inputText.Length - 1;
                        else
                            this.UpdateCaret(true);
                    }
                }
                #endregion
                #region Check input char
                if (this.allowInput && count < sizeOfText && GKeyBoard.LastState.IsKeyUp(key))
                {
                    if (key == Keys.Space)
                    {
                        ++count;
                        inputText = inputText.Insert(count, ' '.ToString());
                        this.UpdateCaret(true);
                    }
                    else if (key >= Keys.A && key <= Keys.Z)
                    {
                        ++count;
                        inputText = inputText.Insert(count, key.ToString());
                        this.UpdateCaret(true);
                    }
                }
                #endregion
            }
        }

        // Set position for caret
        public void UpdateCaret(bool up)
        {
            if (up)
            {
                Vector2 cPosition = caret.Position;
                cPosition.X += textFont.MeasureString(inputText.ElementAt(count).ToString()).X;
                caret.Position = cPosition;
            }
            else
            {
                Vector2 cPosition = caret.Position;
                cPosition.X -= textFont.MeasureString(inputText.ElementAt(count).ToString()).X;
                caret.Position = cPosition;
            }
        }

        public string Parse(string text)
        {
            string line = string.Empty;
            string returnstring = string.Empty;
            string[] wordArray = text.Split(' ');

            foreach (string word in wordArray)
            {
                if (this.textFont.MeasureString(line + word).Length() >= this.controlBackground.Texture.Width)
                {
                    returnstring = returnstring + line + "\n";
                    line = string.Empty;
                }
                line = line + word + " ";
            }
            return returnstring + line;
        }

        #endregion
    }

}
