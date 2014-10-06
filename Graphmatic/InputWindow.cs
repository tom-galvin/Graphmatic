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

            expressionDisplay.ExpressionCursor.Moved += ExpressionCursor_Moved;
        }

        private void InitializeButtons()
        {
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
                token.Power.Add(new BinaryOperationToken(token.Power, BinaryOperation.Subtract));
                token.Power.Add(new DigitToken(token.Power, 1));
                return token;
            });
            #endregion
            #region Constants
            CreateExpressionButton(buttonPi, expression => new ConstantToken(expression, ConstantToken.ConstantType.Pi));
            CreateExpressionButton(buttonE, expression => new ConstantToken(expression, ConstantToken.ConstantType.E));
            #endregion
            #region Misc
            CreateExpressionButton(buttonAbsolute, expression => new AbsoluteToken(expression));
            CreateExpressionButton(buttonBracket, expression => new FunctionToken(expression, ""));
            CreateExpressionButton(buttonComma, expression => new SymbolicToken(expression, SymbolicToken.SymbolicType.Comma));
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
    }
}
