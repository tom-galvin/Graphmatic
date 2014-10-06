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
        private Dictionary<Button, Func<Expression, IToken>> Buttons;

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
            Buttons = new Dictionary<Button, Func<Expression, IToken>>()
            {
                { buttonRoot, expression => new RootToken(expression) },
                { buttonSqrt, expression => {
                        var token = new RootToken(expression);
                        token.Power.Add(new DigitToken(token.Power, 2));
                        return token;
                    }
                },
                { buttonLn, expression => {
                    var token = new LogToken(expression);
                    token.Base.Add(new ConstantToken(token.Base, ConstantToken.ConstantType.E));
                    return token;
                    }
                }
            };

            foreach (var buttonToken in Buttons)
            {
                buttonToken.Key.Click += (sender, e) =>
                    expressionDisplay.ExpressionCursor.Insert(buttonToken.Value(expressionDisplay.ExpressionCursor.Expression));
            }
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
