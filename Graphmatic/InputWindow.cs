﻿using System;
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

namespace Graphmatic
{
    public partial class InputWindow : Form
    {
        /// <summary>
        /// Gets the result of this input window.
        /// </summary>
        public Expression Result
        {
            get;
            protected set;
        }

        public Dictionary<Tuple<Keys, Keys>, Control> Shortcuts;

        public InputWindow(string promptString)
        {
            Shortcuts = new Dictionary<Tuple<Keys, Keys>, Control>();

            InitializeComponent();
            InitializeButtons();

            var prompt = new PromptToken(null, promptString);
            prompt.RecalculateDimensions(expressionDisplay.ExpressionCursor);
            expressionDisplay.Expression.Add(prompt);
            expressionDisplay.ExpressionCursor.Expression = prompt.Content;
            Result = prompt.Content;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            expressionDisplay.ExpressionCursor.Moved += ExpressionCursor_Moved;
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
            CreateExpressionButton(buttonAdd, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Add), "Plus Key", Keys.Oemplus, Keys.Shift);
            CreateExpressionButton(buttonSubtract, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Subtract), "Minus Key", Keys.OemMinus);
            CreateExpressionButton(buttonMultiply, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Multiply), "Shift-8 (*)", Keys.D8, Keys.Shift);
            CreateExpressionButton(buttonDivide, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Divide), "Backslash", Keys.Oem5);
            #endregion
            #region Roots
            CreateExpressionButton(buttonRoot, expression => new RootToken(expression), "R", Keys.R);
            CreateExpressionButton(buttonSqrt, expression =>
            {
                var token = new RootToken(expression);
                token.Power.Add(new DigitToken(token.Power, 2));
                return token;
            }, "Shift-R", Keys.R, Keys.Shift);
            #endregion
            #region Logs
            CreateExpressionButton(buttonLogN, expression => new LogToken(expression), "L", Keys.L);
            CreateExpressionButton(buttonLogE, expression =>
            {
                var token = new LogToken(expression);
                token.Base.Add(new ConstantToken(token.Base, ConstantToken.ConstantType.E));
                return token;
            }, "Shift+L", Keys.L, Keys.Shift);
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
                token.Power.Add(new BinaryOperationToken(token.Power, BinaryOperationToken.BinaryOperationType.Subtract));
                token.Power.Add(new DigitToken(token.Power, 1));
                return token;
            }, "Alt-Minus", Keys.OemMinus, Keys.Alt);
            #endregion
            #region Constants
            CreateExpressionButton(buttonPi, expression => new ConstantToken(expression, ConstantToken.ConstantType.Pi), "P", Keys.P);
            CreateExpressionButton(buttonE, expression => new ConstantToken(expression, ConstantToken.ConstantType.E), "E", Keys.E);
            #endregion
            #region Misc
            CreateExpressionButton(buttonFraction, expression => new FractionToken(expression), "/", Keys.OemQuestion);
            CreateExpressionButton(buttonAbsolute, expression => new AbsoluteToken(expression), "|", Keys.Oem5, Keys.Shift);
            CreateExpressionButton(buttonBracket, expression => new FunctionToken(expression, ""), "(", Keys.D9, Keys.Shift);
            CreateExpressionButton(buttonComma, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Comma), ",", Keys.Oemcomma);
            CreateExpressionButton(buttonPercent, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Percent), "%", Keys.D5, Keys.Shift);
            CreateExpressionButton(buttonSymbolicExp, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Exp10),"Shift+E", Keys.E, Keys.Shift);
#endregion
        }

        private void CreateExpressionButton(Button button, Func<Expression, IToken> token, string label = "", Keys shortcutKey = Keys.None, Keys modifierKey = Keys.None)
        {
            button.Click += (sender, e) =>
                    expressionDisplay.ExpressionCursor.Insert(token(expressionDisplay.ExpressionCursor.Expression));

            if (shortcutKey != Keys.None)
            {
                toolTip.SetToolTip(button, label);
                Shortcuts.Add(new Tuple<Keys, Keys>(shortcutKey, modifierKey), button);
            }
        }

        private bool CheckMoein()
        {
            if (Result.Count != 6) return false;
            string number = "131066";
            for (int i = 0; i < 6; i++)
            {
                if (!(Result[i] is DigitToken)) return false;
                if ((Result[i] as DigitToken).Value.ToString() != number[i].ToString()) return false;
            }
            return true;
        }

        void ExpressionCursor_Moved(object sender, EventArgs e)
        {
            expressionDisplay.MoeinMode = CheckMoein();
            expressionDisplay.Invalidate();
        }

        private void InputWindow_Load(object sender, EventArgs e)
        {

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
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void InputWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                buttonDelete.PerformClick();
            }
            else if (e.KeyCode == Keys.Left)
            {
                buttonLeft.PerformClick();
            }
            else if(e.KeyCode == Keys.Right)
            {
                buttonRight.PerformClick();
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
                    e.Handled = true;
                }
                else
                {
                }
                //MessageBox.Show(String.Format("Key: {0}\r\nModifier: {1}", e.KeyCode.ToString(), e.Modifiers.ToString()));
            }
        }
    }
}
