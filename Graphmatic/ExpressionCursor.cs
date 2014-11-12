using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphmatic.Expressions;
using Graphmatic.Expressions.Tokens;

namespace Graphmatic
{
    /// <summary>
    /// Represents the method that handles the user clicking on a hotspot in an expression.
    /// </summary>
    /// <param name="pointInHotspotClicked">The point representing the location clicked relative to the top-left corner of the hotspot.</param>
    /// <param name="clickedBy">The <c>ExpressionCursor</c> that was used to click on the hotspot.</param>
    public delegate void ExpressionCursorHotspotClickedCallback(Point pointInHotspotClicked, ExpressionCursor clickedBy);

    /// <summary>
    /// Provides a way of editing an equation visually.
    /// </summary>
    public class ExpressionCursor
    {
        private Expression _Expression;
        /// <summary>
        /// Gets or sets the Graphmatic expression being edited.
        /// </summary>
        public Expression Expression
        {
            get
            {
                return _Expression;
            }
            set
            {
                this._Expression = value;
                this._Index = 0;
                OnMoved();
            }
        }

        private int _Index;
        /// <summary>
        /// Gets or sets the index of the cursor in Expression.
        /// </summary>
        public int Index
        {
            get
            {
                return this._Index;
            }
            set
            {
                if (value >= 0 && value <= Expression.Count)
                {
                    this._Index = value;
                    OnMoved();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Cursor index must be within bounds of current expression.");
                }
            }
        }

        /// <summary>
        /// Gets the dictionary of Hotspots in the equation.
        /// </summary>
        public Dictionary<Rectangle, ExpressionCursorHotspotClickedCallback> Hotspots
        {
            get;
            protected set;
        }

        /// <summary>
        /// Raised when the cursor is moved.
        /// </summary>
        public event EventHandler<EventArgs> Moved;
        protected void OnMoved()
        {
            var movedHandler = Moved;
            if (movedHandler != null)
                movedHandler(this, new EventArgs());
        }

        /// <summary>
        /// Creates a new instance of the ExpressionCursor class, in the given Expression at the given Index.
        /// </summary>
        /// <param name="expression">The Expression being edited by the cursor.</param>
        /// <param name="index">The Index of the cursor in Expression.</param>
        public ExpressionCursor(Expression expression, int index)
        {
            Expression = expression;
            Index = index;
            Hotspots = new Dictionary<Rectangle, ExpressionCursorHotspotClickedCallback>();
        }

        /// <summary>
        /// Create a cursor hotspot wherein the given action will be performed in the area specified by <c>rectangleArea</c> is clicked by the user.
        /// </summary>
        /// <param name="hotspotArea">The area of the hotspot.</param>
        /// <param name="actionOnClick">The action to be performed upon the area being clicked.</param>
        public void CreateHotspot(Rectangle hotspotArea, ExpressionCursorHotspotClickedCallback actionOnClick)
        {
            Hotspots.Add(hotspotArea, actionOnClick);
        }

        /// <summary>
        /// Moves the cursor left.
        /// </summary>
        /// <returns>Returns true if the cursor movement was successful, and false otherwise.</returns>
        public bool Left()
        {
            if (Index > 0) // if cursor is not at the left edge of the current expression...
            {
                Index--;
                if (Expression[Index].Children.Length > 0) // if the expression we moved over has children...
                {
                    var children = Expression[Index].Children;
                    Expression = children[children.Length - 1]; // move us into it
                    Index = Expression.Count; // put us at the rightmost edge
                }
            }
            else
            {
                if (Expression.Parent == null) // if this expression is parentless...
                {
                    return false; // return false - we can't move
                }
                else // if this expression has a parent...
                {
                    int currentExpressionIndexInParent = Expression.IndexInParent();
                    if (currentExpressionIndexInParent > 0) // if this is not the leftmost expression in the parent token...
                    {
                        // move to the previous expression
                        currentExpressionIndexInParent--;
                        Expression = Expression.Parent.Children[currentExpressionIndexInParent];
                        // put us at the rightmost edge
                        Index = Expression.Count;
                    }
                    else // if this is the leftmost expression in the parent token...
                    {
                        if (Expression.Parent.Parent == null) // if the expression's parent has no containing token...
                        {
                            return false; // return false - we can't move
                        }
                        else // if this expression's parent has a containing token...
                        {
                            int currentExpressionParentIndexInParentParent = Expression.Parent.IndexInParent();
                            // move us outside of the current expression
                            Expression = Expression.Parent.Parent;
                            // put us to the left of the token we were just in
                            Index = currentExpressionParentIndexInParentParent;
                        }
                    }
                }
            }
            OnMoved();
            return true;
        }

        /// <summary>
        /// Moves the cursor right.
        /// </summary>
        /// <returns>Returns true if the cursor movement was successful, and false otherwise.</returns>
        public bool Right()
        {
            if (Index < Expression.Count) // if cursor is not at the right edge of the current expression...
            {
                Index++;
                if (Expression[Index - 1].Children.Length > 0) // if the expression we moved over has children...
                {
                    var children = Expression[Index - 1].Children;
                    Expression = children[0]; // move us into it
                    Index = 0; // put us at the leftmost edge
                }
            }
            else
            {
                if (Expression.Parent == null) // if this expression is parentless...
                {
                    return false; // return false - we can't move
                }
                else // if this expression has a parent...
                {
                    int currentExpressionIndexInParent = Expression.IndexInParent();

                    if (currentExpressionIndexInParent < Expression.Parent.Children.Length - 1) // if this is not the rightmost expression in the parent token...
                    {
                        // move to the next expression
                        currentExpressionIndexInParent++;
                        Expression = Expression.Parent.Children[currentExpressionIndexInParent];
                        // put us at the leftmost edge
                        Index = 0;
                    }
                    else // if this is the rightmost expression in the parent token...
                    {
                        if (Expression.Parent.Parent == null) // if the expression's parent has no containing token...
                        {
                            return false; // return false - we can't move
                        }
                        else // if this expression's parent has a containing token...
                        {
                            int currentExpressionParentIndexInParentParent = Expression.Parent.IndexInParent();
                            // move us outside of the current expression
                            Expression = Expression.Parent.Parent;
                            // put us to the right of the token we were just in
                            Index = currentExpressionParentIndexInParentParent + 1;
                        }
                    }
                }
            }
            OnMoved();
            return true;
        }

        /// <summary>
        /// Deletes whatever is to the left of the cursor.
        /// </summary>
        /// <returns>Returns true if the deletion is successful; otherwise false.</returns>
        public bool Delete()
        {
            if (_Index == 0) // if the cursor is at the far left of the expression...
            {
                if (Expression.Parent != null && Expression.Parent.Parent != null) // if the parent expression, and the parent expression's parent token exist...
                {
                    Token tokenToDelete = Expression.Parent;
                    if (tokenToDelete.Empty()) // if token to delete is empty...
                    {
                        int currentExpressionParentIndexInParentParent = Expression.Parent.IndexInParent();
                        Expression = Expression.Parent.Parent;
                        Index = currentExpressionParentIndexInParentParent;

                        Expression.Remove(tokenToDelete);
                        return true;
                    }
                    else // it's not empty, so just leave it instead
                    {
                        Left();
                        return false;
                    }
                }
                else
                {
                    Left();
                    return false;
                }
            }
            else
            {
                Token tokenToDelete = Expression[_Index - 1];
                if (tokenToDelete.Children.Length == 0) // token has no children - delete it
                {
                    Left();
                    Expression.Remove(tokenToDelete);
                    return true;
                }
                else // token has children - move inside it instead
                {
                    Left();
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a token to the left of the cursor, and moves the cursor accordingly.
        /// </summary>
        /// <param name="token">The token to insert.</param>
        public void Insert(Token token)
        {
            token.RecalculateDimensions(this);
            Expression.Insert(Index, token);
            Expression defaultExpression = token.DefaultChild;
            if (defaultExpression == null) // if the token has no default child expression...
            {
                Right(); // skip over the token
            }
            else // otherwise...
            {
                if (token is ICollectorToken)
                {
                    ICollectorToken tokenCollector = token as ICollectorToken;
                    int newIndex = Index + 1, addedTokens = 0;
                    for (int i = Index - 1; i >= 0; i--)
                    {
                        Token collectedToken = Expression[i];
                        if (collectedToken is ICollectorToken)
                        {
                            ICollectorToken collectedTokenCollector = collectedToken as ICollectorToken;
                            if (collectedTokenCollector.MaxPrecedence < tokenCollector.MaxPrecedence) break;
                        }
                        collectedToken.Parent = defaultExpression;
                        defaultExpression.Insert(0, collectedToken);
                        Expression.RemoveAt(i);
                        newIndex--;
                        addedTokens++;
                    }
                    if (addedTokens > 0)
                    {
                        if (defaultExpression.IndexInParent() != defaultExpression.Parent.Children.Length - 1)
                        {
                            Expression nextDefaultExpression = defaultExpression.Parent.Children[defaultExpression.IndexInParent() + 1];
                            if (nextDefaultExpression.Count == 0)
                            {
                                Expression = nextDefaultExpression;
                            }
                            else
                            {
                                Index = newIndex;
                            }
                        }
                        else
                        {
                            Index = newIndex;
                        }
                    }
                    else
                    {
                        Expression = defaultExpression;
                    }
                }
                else
                {
                    Expression = defaultExpression;
                }
            }
            OnMoved();
        }
    }
}
