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
            var prompt = new PromptToken(null, promptString);
            prompt.RecalculateDimensions(expressionDisplay.ExpressionCursor);
            expressionDisplay.Expression.Add(prompt);
            expressionDisplay.ExpressionCursor.Expression = prompt.Content;
            Result = prompt.Content;

            expressionDisplay.ExpressionCursor.Moved += ExpressionCursor_Moved;
            prompt.Content.Add(new DigitToken(prompt.Content, 4));
            var sine = new FunctionToken(prompt.Content, "Calc-é");
            var log = new LogToken(sine.Operand);
            var exp = new ExpToken(log.Base);
            exp.Base.Add(new DigitToken(exp.Base, 3));
            var root = new RootToken(exp.Power);
            root.Power.Add(new DigitToken(root.Power, 2));
            exp.Power.Add(root);
            log.Base.Add(exp);
            var arctan = new FunctionToken(log.Operand, "tan`");
            arctan.Operand.Add(new DigitToken(arctan.Operand, 3));
            log.Operand.Add(arctan);
            sine.Operand.Add(log);
            prompt.Content.Add(sine);
            prompt.Content.Add(new ExpToken(prompt.Content));
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
            MessageBox.Show(Result.ToXmlElement().ToString());
        }
    }
}
