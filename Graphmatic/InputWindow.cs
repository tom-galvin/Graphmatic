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
            var prompt = new PromptToken(null, promptString, DisplaySize.Large);
            expressionDisplay.Expression.Add(prompt);
            expressionDisplay.ExpressionCursor.Expression = prompt.Content;
            Result = prompt.Content;

            expressionDisplay.ExpressionCursor.Moved += ExpressionCursor_Moved;
            prompt.Content.Add(new DigitToken(prompt.Content, 4, DisplaySize.Large));
            var sine = new FunctionToken(prompt.Content, "Calc-é", DisplaySize.Large);
            var frac = new FractionToken(sine.Operand, DisplaySize.Large);
            var exp = new ExpToken(frac.Top, DisplaySize.Small);
            exp.Base.Add(new DigitToken(exp.Base, 3, DisplaySize.Small));
            exp.Power.Add(new RootToken(exp.Power, DisplaySize.Small));
            frac.Top.Add(exp);
            var arctan = new FunctionToken(frac.Bottom, "tan`", DisplaySize.Small);
            arctan.Operand.Add(new DigitToken(arctan.Operand, 3, DisplaySize.Small));
            frac.Bottom.Add(arctan);
            sine.Operand.Add(frac);
            prompt.Content.Add(sine);
            prompt.Content.Add(new ExpToken(prompt.Content, DisplaySize.Large));
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
        }
    }
}
