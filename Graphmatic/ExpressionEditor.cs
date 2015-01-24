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
    public partial class ExpressionEditor : Form
    {
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

        public Dictionary<Tuple<Keys, Keys>, Control> Shortcuts;

        public event EventHandler<ExpressionVerificationEventArgs> Verify;
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

        public ExpressionEditor(Equation equation)
        {
            Equation = equation;
            Shortcuts = new Dictionary<Tuple<Keys, Keys>, Control>();

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

        private void InitializeButtons()
        {
            #region Digits
            CreateExpressionButton(button0, expression => new DigitToken(expression, 0), "0 key", Keys.D0);
            CreateExpressionButton(button1, expression => new DigitToken(expression, 1), "1 key", Keys.D1);
            CreateExpressionButton(button2, expression => new DigitToken(expression, 2), "2 key", Keys.D2);
            CreateExpressionButton(button3, expression => new DigitToken(expression, 3), "3 key", Keys.D3);
            CreateExpressionButton(button4, expression => new DigitToken(expression, 4), "4 key", Keys.D4);
            CreateExpressionButton(button5, expression => new DigitToken(expression, 5), "5 key", Keys.D5);
            CreateExpressionButton(button6, expression => new DigitToken(expression, 6), "6 key", Keys.D6);
            CreateExpressionButton(button7, expression => new DigitToken(expression, 7), "7 key", Keys.D7);
            CreateExpressionButton(button8, expression => new DigitToken(expression, 8), "8 key", Keys.D8);
            CreateExpressionButton(button9, expression => new DigitToken(expression, 9), "9 key", Keys.D9);
            CreateExpressionButton(buttonDecimalPoint, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.DecimalPoint), ". key", Keys.OemPeriod);
            #endregion
            #region Basic Operations
            CreateExpressionButton(buttonAdd, expression => new OperationToken(expression, OperationToken.OperationType.Add), "Plus Key", Keys.Oemplus, Keys.Shift);
            CreateExpressionButton(buttonSubtract, expression => new OperationToken(expression, OperationToken.OperationType.Subtract), "Minus Key", Keys.OemMinus);
            CreateExpressionButton(buttonMultiply, expression => new OperationToken(expression, OperationToken.OperationType.Multiply), "Shift-8 (*)", Keys.D8, Keys.Shift);
            CreateExpressionButton(buttonDivide, expression => new OperationToken(expression, OperationToken.OperationType.Divide), "Backslash", Keys.Oem5);
            #endregion
            #region Roots
            CreateExpressionButton(buttonRoot, expression => new RootToken(expression), "Ctrl-Shift-R", Keys.R, Keys.Control | Keys.Shift);
            CreateExpressionButton(buttonSqrt, expression =>
            {
                var token = new RootToken(expression);
                token.Power.Add(new DigitToken(token.Power, 2));
                return token;
            }, "Ctrl-R", Keys.R, Keys.Control);
            #endregion
            #region Trig
            CreateExpressionButton(buttonSin, expression => new FunctionToken(expression, "sin"));
            CreateExpressionButton(buttonCos, expression => new FunctionToken(expression, "cos"));
            CreateExpressionButton(buttonTan, expression => new FunctionToken(expression, "tan"));
            CreateExpressionButton(buttonArcsin, expression => new FunctionToken(expression, "sin`"));
            CreateExpressionButton(buttonArccos, expression => new FunctionToken(expression, "cos`"));
            CreateExpressionButton(buttonArctan, expression => new FunctionToken(expression, "tan`"));
            #endregion
            #region Logs
            CreateExpressionButton(buttonLogN, expression => new LogToken(expression), "Ctrl-L", Keys.L, Keys.Control);
            CreateExpressionButton(buttonLogE, expression =>
            {
                var token = new LogToken(expression);
                token.Base.Add(new ConstantToken(token.Base, ConstantToken.ConstantType.E));
                return token;
            }, "Ctrl-Shift-L", Keys.L, Keys.Shift | Keys.Control);
            CreateExpressionButton(buttonLog10, expression =>
            {
                var token = new LogToken(expression);
                token.Base.Add(new DigitToken(token.Base, 1));
                token.Base.Add(new DigitToken(token.Base, 0));
                return token;
            });
            #endregion
            #region Exponents
            CreateExpressionButton(buttonExp, expression => new ExpToken(expression), "Shift-6 (^)", Keys.D6, Keys.Shift);
            CreateExpressionButton(buttonSquare, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new DigitToken(token.Power, 2));
                return token;
            }, "Alt-2", Keys.D2, Keys.Alt);
            CreateExpressionButton(buttonCube, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new DigitToken(token.Power, 3));
                return token;
            }, "Alt-3", Keys.D3, Keys.Alt);
            CreateExpressionButton(buttonReciprocate, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new OperationToken(token.Power, OperationToken.OperationType.Subtract));
                token.Power.Add(new DigitToken(token.Power, 1));
                return token;
            }, "Alt-Minus", Keys.OemMinus, Keys.Alt);
            #endregion
            #region Constants
            CreateExpressionButton(buttonPi, expression => new ConstantToken(expression, ConstantToken.ConstantType.Pi), "Alt-P", Keys.P, Keys.Alt);
            CreateExpressionButton(buttonE, expression => new ConstantToken(expression, ConstantToken.ConstantType.E), "Alt-E", Keys.E, Keys.Alt);
            #endregion
            #region Misc
            CreateExpressionButton(buttonFraction, expression => new FractionToken(expression), "Forward-slash", Keys.OemQuestion);
            CreateExpressionButton(buttonAbsolute, expression => new AbsoluteToken(expression), "|", Keys.Oem5, Keys.Shift);
            CreateExpressionButton(buttonBracket, expression => new FunctionToken(expression, ""), "(", Keys.D9, Keys.Shift);
            // CreateExpressionButton(buttonComma, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Comma), ",", Keys.Oemcomma);
            CreateExpressionButton(buttonPercent, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Percent), "%", Keys.D5, Keys.Shift);
            CreateExpressionButton(buttonSymbolicExp, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Exp10), "Ctrl-E", Keys.E, Keys.Control);

            #endregion
            CreateExpressionButton(buttonXVariable, expression => new VariableToken(expression, 'x'), "X", Keys.X);
            CreateExpressionButton(buttonYVariable, expression => new VariableToken(expression, 'y'), "Y", Keys.Y);
            CreateExpressionButton(buttonCustomVariable, expression =>
            {
                char customVariable = EnterVariableDialog.EnterVariable();
                if (customVariable != '\0')
                {
                    return new VariableToken(expression, customVariable);
                }
                else
                {
                    return null;
                }
            }, "Hash", Keys.Oem7);
            CreateExpressionButton(buttonEquals, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Equals), "Equals", Keys.Oemplus);
        }

        private void CreateExpressionButton(Button button, Func<Expression, Token> tokenFactory, string label = "", Keys shortcutKey = Keys.None, Keys modifierKey = Keys.None)
        {

            button.Click += (sender, e) =>
            {
                var token = tokenFactory(expressionDisplay.ExpressionCursor.Expression);
                if (token != null)
                {
                    expressionDisplay.ExpressionCursor.Insert(token);
                }
            };

            if (shortcutKey != Keys.None)
            {
                toolTip.SetToolTip(button, label);
                Shortcuts.Add(new Tuple<Keys, Keys>(shortcutKey, modifierKey), button);
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
                var control = Shortcuts.FirstOrDefault(kvp => kvp.Key.Item1 == e.KeyCode && kvp.Key.Item2 == e.Modifiers);
                if (!control.Equals(default(Tuple<Keys, Keys>)))
                {
                    if (control.Value is Button)
                    {
                        (control.Value as Button).PerformClick();
                    }
                    e.Handled = e.SuppressKeyPress = true;
                }
                else
                {
                }
                // MessageBox.Show(String.Format("Key: {0}\r\nModifier: {1}", e.KeyCode.ToString(), e.Modifiers.ToString()));
            }
        }

        private void InputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Result.Parent = null;
        }
    }
}
