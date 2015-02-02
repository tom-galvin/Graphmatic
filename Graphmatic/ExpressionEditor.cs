using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Tokens;
using Graphmatic.Interaction;

namespace Graphmatic
{
    /// <summary>
    /// Represents a form used for editing a Graphmatic expression object within an Equation resource.
    /// </summary>
    public partial class ExpressionEditor : Form
    {
        /// <summary>
        /// Gets or sets the equation containing the Expression to be edited.
        /// </summary>
        private Equation Equation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the result of this input window.
        /// </summary>
        public Expression Result
        {
            get
            {
                return Equation.Expression;
            }
        }

        /// <summary>
        /// A dictionary containing all keyboard shortcuts that can be pressed by the user, along
        /// with the delegate that is called when the shortcut key is pressed.
        /// </summary>
        public Dictionary<Tuple<Keys, Keys>, Action> Shortcuts;

        /// <summary>
        /// Raised when verification of the entered expression is to take place. Verification is
        /// handled by the form that created this <c>ExpressionEditor</c> form, normally being an
        /// <c>EquationEditor</c> form.<para/> As this event might contain several different
        /// event handlers, the <see cref="VerifyExpression"/> method must manually invoke each
        /// handler in the invocation list for the event and check that the
        /// <see cref="Graphmatic.ExpressionVerificationEventArgs.Failure"/> property is not set
        /// to true in any of the called event handlers - if it is, the verification step has failed
        /// and the appropriate error message is to be set.
        /// </summary>
        public event EventHandler<ExpressionVerificationEventArgs> Verify;

        /// <summary>
        /// Verifies that the entered expression is valid by iterating through all of the event
        /// handlers for the <see cref="Graphmatic.ExpressionEditor.Verify"/> event and checking that
        /// the <see cref="Graphmatic.ExpressionVerificationEventArgs.Failure"/> property is not
        /// set to true. If the property remains false, then this method returns true; this method
        /// returns false otherwise.
        /// </summary>
        /// <returns>Returns whether the expression verification was successful.</returns>
        private bool VerifyExpression()
        {
            EventHandler<ExpressionVerificationEventArgs> eventHandler = Verify;
            if (eventHandler != null)
            {
                var multicastHandlers = eventHandler.GetInvocationList();
                var eventArgs = new ExpressionVerificationEventArgs()
                {
                    Cursor = expressionDisplay.ExpressionCursor,
                    Failure = false,
                    Equation = Equation
                };
                foreach (EventHandler<ExpressionVerificationEventArgs> multicastHandler in multicastHandlers)
                {
                    multicastHandler(this, eventArgs);
                    if (eventArgs.Failure)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <c>ExpressionEditor</c> form, with the specified equation
        /// that is to be edited.
        /// </summary>
        /// <param name="equation">The equation that contains the expression that is to be edited by the
        /// user.</param>
        public ExpressionEditor(Equation equation)
        {
            Equation = equation;
            Shortcuts = new Dictionary<Tuple<Keys, Keys>, Action>();

            InitializeComponent();
            InitializeButtons();

            expressionDisplay.Expression = equation.Expression;
            expressionDisplay.ExpressionCursor.Expression = equation.Expression;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            expressionDisplay.ExpressionCursor.Moved += ExpressionCursor_Moved;

            buttonXVariable.Image = FontHelperExtensionMethods.GetCharacterImage('x', false, 2);
            buttonYVariable.Image = FontHelperExtensionMethods.GetCharacterImage('y', false, 2);
            buttonEquals.Image = FontHelperExtensionMethods.GetCharacterImage('=', false, 2);
        }

        /// <summary>
        /// Initializes the handlers for the buttons on the form that add the appropriate event handlers
        /// and keyboard shortcut handlers for all of the buttons on the form. This uses the
        /// <see cref="CreateTokenButton"/> method to create the event handlers.
        /// </summary>
        private void InitializeButtons()
        {
            #region Digits
            CreateTokenButton(button0, () => new DigitToken(0), "0 key", Keys.D0);
            CreateTokenButton(button1, () => new DigitToken(1), "1 key", Keys.D1);
            CreateTokenButton(button2, () => new DigitToken(2), "2 key", Keys.D2);
            CreateTokenButton(button3, () => new DigitToken(3), "3 key", Keys.D3);
            CreateTokenButton(button4, () => new DigitToken(4), "4 key", Keys.D4);
            CreateTokenButton(button5, () => new DigitToken(5), "5 key", Keys.D5);
            CreateTokenButton(button6, () => new DigitToken(6), "6 key", Keys.D6);
            CreateTokenButton(button7, () => new DigitToken(7), "7 key", Keys.D7);
            CreateTokenButton(button8, () => new DigitToken(8), "8 key", Keys.D8);
            CreateTokenButton(button9, () => new DigitToken(9), "9 key", Keys.D9);
            CreateTokenButton(buttonDecimalPoint, () => new SymbolicToken(SymbolicToken.SymbolicType.DecimalPoint), ". key", Keys.OemPeriod);
            #endregion
            #region Basic Operations
            CreateTokenButton(buttonAdd, () => new OperationToken(OperationToken.OperationType.Add), "Plus Key", Keys.Oemplus, Keys.Shift);
            CreateTokenButton(buttonSubtract, () => new OperationToken(OperationToken.OperationType.Subtract), "Minus Key", Keys.OemMinus);
            CreateTokenButton(buttonMultiply, () => new OperationToken(OperationToken.OperationType.Multiply), "Shift-8 (*)", Keys.D8, Keys.Shift);
            CreateTokenButton(buttonDivide, () => new OperationToken(OperationToken.OperationType.Divide), "Backslash", Keys.Oem5);
            #endregion
            #region Roots
            CreateTokenButton(buttonRoot, () => new RootToken(), "Ctrl-Shift-R", Keys.R, Keys.Control | Keys.Shift);
            CreateTokenButton(buttonSqrt, () =>
            {
                var token = new RootToken();
                token.Power.Add(new DigitToken(2));
                return token;
            }, "Ctrl-R", Keys.R, Keys.Control);
            #endregion
            #region Trig
            CreateTokenButton(buttonSin, () => new FunctionToken("sin"));
            CreateTokenButton(buttonCos, () => new FunctionToken("cos"));
            CreateTokenButton(buttonTan, () => new FunctionToken("tan"));
            CreateTokenButton(buttonArcsin, () => new FunctionToken("sin`"));
            CreateTokenButton(buttonArccos, () => new FunctionToken("cos`"));
            CreateTokenButton(buttonArctan, () => new FunctionToken("tan`"));
            #endregion
            #region Logs
            CreateTokenButton(buttonLogN, () => new LogToken(), "Ctrl-L", Keys.L, Keys.Control);
            CreateTokenButton(buttonLogE, () =>
            {
                var token = new LogToken();
                token.Base.Add(new ConstantToken(ConstantToken.ConstantType.E));
                return token;
            }, "Ctrl-Shift-L", Keys.L, Keys.Shift | Keys.Control);
            CreateTokenButton(buttonLog10, () =>
            {
                var token = new LogToken();
                token.Base.Add(new DigitToken(1));
                token.Base.Add(new DigitToken(0));
                return token;
            });
            #endregion
            #region Exponents
            CreateTokenButton(buttonExp, () => new ExpToken(), "Shift-6 (^)", Keys.D6, Keys.Shift);
            CreateTokenButton(buttonSquare, () =>
            {
                var token = new ExpToken();
                token.Power.Add(new DigitToken(2));
                return token;
            }, "Alt-2", Keys.D2, Keys.Alt);
            CreateTokenButton(buttonCube, () =>
            {
                var token = new ExpToken();
                token.Power.Add(new DigitToken(3));
                return token;
            }, "Alt-3", Keys.D3, Keys.Alt);
            CreateTokenButton(buttonReciprocate, () =>
            {
                var token = new ExpToken();
                token.Power.Add(new OperationToken(OperationToken.OperationType.Subtract));
                token.Power.Add(new DigitToken(1));
                return token;
            }, "Alt-Minus", Keys.OemMinus, Keys.Alt);
            #endregion
            #region Constants
            CreateTokenButton(buttonPi, () => new ConstantToken(ConstantToken.ConstantType.Pi), "Alt-P", Keys.P, Keys.Alt);
            CreateTokenButton(buttonE, () => new ConstantToken(ConstantToken.ConstantType.E), "Alt-E", Keys.E, Keys.Alt);
            #endregion
            #region Misc
            CreateTokenButton(buttonFraction, () => new FractionToken(), "Forward-slash", Keys.OemQuestion);
            CreateTokenButton(buttonAbsolute, () => new AbsoluteToken(), "|", Keys.Oem5, Keys.Shift);
            CreateTokenButton(buttonBracket, () => new FunctionToken(""), "(", Keys.D9, Keys.Shift);
            // CreateExpressionButton(buttonComma, () => new SymbolicToken(SymbolicToken.SymbolicType.Comma), ",", Keys.Oemcomma);
            CreateTokenButton(buttonPercent, () => new SymbolicToken(SymbolicToken.SymbolicType.Percent), "%", Keys.D5, Keys.Shift);
            CreateTokenButton(buttonSymbolicExp, () => new SymbolicToken(SymbolicToken.SymbolicType.Exp10), "Ctrl-E", Keys.E, Keys.Control);

            #endregion
            CreateTokenButton(buttonXVariable, () => new VariableToken('x'), "X", Keys.X);
            CreateTokenButton(buttonYVariable, () => new VariableToken('y'), "Y", Keys.Y);
            CreateTokenButton(buttonCustomVariable, () =>
            {
                // creates an EnterVariable dialog to allow the user to enter any variable
                // character they want (that isn't 'x' or 'y')
                char customVariable = EnterVariableDialog.EnterVariable();
                if (customVariable != '\0')
                {
                    return new VariableToken(customVariable);
                }
                else
                {
                    return null;
                }
            }, "Hash", Keys.Oem7);
            CreateTokenButton(buttonEquals, () => new SymbolicToken(SymbolicToken.SymbolicType.Equals), "Equals", Keys.Oemplus);
        }

        /// <summary>
        /// Creates the appropriate handlers for a button on the <c>ExpressionEditor</c> form that adds a Token to the
        /// current Expression that is being edited.
        /// </summary>
        /// <param name="button">The button for which to add the event handlers.</param>
        /// <param name="tokenFactory">A delegate returning a new instance of the token to add to the Expression.</param>
        /// <param name="toolTipLabel">The label on the button displaying the shortcut key, or <c>null</c> if the button
        /// does not have a shortcut key.</param>
        /// <param name="shortcutKey">The shortcut key for this button, or <see cref="System.Windows.Forms.Keys.None"/>
        /// if this button has not shortcut key.</param>
        /// <param name="modifierKey">The shortcut modifer key for this button, or <see cref="System.Windows.Forms.Keys.None"/>
        /// if this button has not shortcut key. If <paramref name="shortcutKey"/> equals <see cref="System.Windows.Forms.Keys.None"/>
        /// then this parameter has no effect.</param>
        private void CreateTokenButton(Button button, Func<Token> tokenFactory, string toolTipLabel = "", Keys shortcutKey = Keys.None, Keys modifierKey = Keys.None)
        {
            // a delegate to get an instance of the new Token using tokenFactory,
            // verifies that it is not null and uses the expression display's
            // ExpressionCursor to insert it into the expression
            Action insertToken = delegate()
            {
                var token = tokenFactory();
                if (token != null)
                {
                    expressionDisplay.ExpressionCursor.Insert(token);
                }
            };

            // calls insertToken when the button is clicked
            button.Click += (sender, e) =>
            {
                insertToken();
            };

            // add insertToken to the dictionary of shortcuts
            if (shortcutKey != Keys.None)
            {
                toolTip.SetToolTip(button, toolTipLabel);
                Shortcuts.Add(new Tuple<Keys, Keys>(shortcutKey, modifierKey), insertToken);
            }
        }

        void ExpressionCursor_Moved(object sender, EventArgs e)
        {
            expressionDisplay.Invalidate();
        }

        private void InputWindow_Load(object sender, EventArgs e)
        {
            if (Equation.Expression != null)
            {
                expressionDisplay.ExpressionCursor.Index = Equation.Expression.Count;
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            expressionDisplay.MoveRight();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            expressionDisplay.MoveLeft();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            expressionDisplay.Delete();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (VerifyExpression())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Processes a command key sent via the underlying Windows form message loop.<para/>
        /// This is needed as the left and right key events are not sent via the <c>KeyDown</c> event, so
        /// this override handler will trap and handle the left and right keys, and pass down everything
        /// else to the base <c>Form.ProcessCmdKey</c> method.
        /// </summary>
        /// <seealso cref="System.Windows.Forms.ProcessCmdKey"/>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                buttonLeft.PerformClick();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                buttonRight.PerformClick();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void InputWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                buttonDelete.PerformClick();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.D0 && e.Modifiers == Keys.Shift)
            {
                buttonRight.PerformClick();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
            else
            {
                var control = Shortcuts.Where(kvp =>
                    kvp.Key.Item1 == e.KeyCode     // get the shortcut handler with the same keycode...
                 && kvp.Key.Item2 == e.Modifiers); // and the same modifier code

                if (control.Count() > 0)     // if there is a key with that shortcut...
                    control.First().Value(); // call it; otherwise, do nothing

                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void InputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Result.Parent = null;
        }
    }
}
