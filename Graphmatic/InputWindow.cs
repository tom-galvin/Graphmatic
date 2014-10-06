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

        public InputWindow(string promptString)
        {
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
            CreateExpressionButton(button0, expression => new DigitToken(expression, 0));
            CreateExpressionButton(button1, expression => new DigitToken(expression, 1));
            CreateExpressionButton(button2, expression => new DigitToken(expression, 2));
            CreateExpressionButton(button3, expression => new DigitToken(expression, 3));
            CreateExpressionButton(button4, expression => new DigitToken(expression, 4));
            CreateExpressionButton(button5, expression => new DigitToken(expression, 5));
            CreateExpressionButton(button6, expression => new DigitToken(expression, 6));
            CreateExpressionButton(button7, expression => new DigitToken(expression, 7));
            CreateExpressionButton(button8, expression => new DigitToken(expression, 8));
            CreateExpressionButton(button9, expression => new DigitToken(expression, 9));
            CreateExpressionButton(buttonDecimalPoint, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.DecimalPoint));
            #endregion
            #region Basic Operations
            CreateExpressionButton(buttonAdd, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Add));
            CreateExpressionButton(buttonSubtract, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Subtract));
            CreateExpressionButton(buttonMultiply, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Multiply));
            CreateExpressionButton(buttonDivide, expression => new BinaryOperationToken(expression, BinaryOperationToken.BinaryOperationType.Divide));
            #endregion
            #region Roots
            CreateExpressionButton(buttonRoot, expression => new RootToken(expression));
            CreateExpressionButton(buttonSqrt, expression =>
            {
                var token = new RootToken(expression);
                token.Power.Add(new DigitToken(token.Power, 2));
                return token;
            });
            #endregion
            #region Logs
            CreateExpressionButton(buttonLogN, expression => new LogToken(expression));
            CreateExpressionButton(buttonLogE, expression =>
            {
                var token = new LogToken(expression);
                token.Base.Add(new ConstantToken(token.Base, ConstantToken.ConstantType.E));
                return token;
            });
            CreateExpressionButton(buttonLog10, expression =>
            {
                var token = new LogToken(expression);
                token.Base.Add(new DigitToken(token.Base, 1));
                token.Base.Add(new DigitToken(token.Base, 0));
                return token;
            });
            #endregion
            #region Exponents
            CreateExpressionButton(buttonExp, expression => new ExpToken(expression));
            CreateExpressionButton(buttonSquare, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new DigitToken(token.Power, 2));
                return token;
            });
            CreateExpressionButton(buttonCube, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new DigitToken(token.Power, 3));
                return token;
            });
            CreateExpressionButton(buttonReciprocate, expression =>
            {
                var token = new ExpToken(expression);
                token.Power.Add(new BinaryOperationToken(token.Power, BinaryOperationToken.BinaryOperationType.Subtract));
                token.Power.Add(new DigitToken(token.Power, 1));
                return token;
            });
            #endregion
            #region Constants
            CreateExpressionButton(buttonPi, expression => new ConstantToken(expression, ConstantToken.ConstantType.Pi));
            CreateExpressionButton(buttonE, expression => new ConstantToken(expression, ConstantToken.ConstantType.E));
            #endregion
            #region Misc
            CreateExpressionButton(buttonFraction, expression => new FractionToken(expression));
            CreateExpressionButton(buttonAbsolute, expression => new AbsoluteToken(expression));
            CreateExpressionButton(buttonBracket, expression => new FunctionToken(expression, ""));
            CreateExpressionButton(buttonComma, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Comma));
            CreateExpressionButton(buttonPercent, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Percent));
            CreateExpressionButton(buttonSymbolicExp, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Exp10));
#endregion
        }

        private void CreateExpressionButton(Button button, Func<Expression, IToken> token)
        {
            button.Click += (sender, e) =>
                    expressionDisplay.ExpressionCursor.Insert(token(expressionDisplay.ExpressionCursor.Expression));
        }

        void ExpressionCursor_Moved(object sender, EventArgs e)
        {
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
            // MessageBox.Show(Result.ToXmlElement().ToString());
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
