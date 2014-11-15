namespace Graphmatic
{
    partial class ExpressionEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRoot = new System.Windows.Forms.Button();
            this.buttonSqrt = new System.Windows.Forms.Button();
            this.buttonLogE = new System.Windows.Forms.Button();
            this.buttonExp = new System.Windows.Forms.Button();
            this.buttonLog10 = new System.Windows.Forms.Button();
            this.buttonLogN = new System.Windows.Forms.Button();
            this.buttonReciprocate = new System.Windows.Forms.Button();
            this.buttonCube = new System.Windows.Forms.Button();
            this.buttonSquare = new System.Windows.Forms.Button();
            this.buttonAbsolute = new System.Windows.Forms.Button();
            this.buttonPi = new System.Windows.Forms.Button();
            this.buttonBracket = new System.Windows.Forms.Button();
            this.buttonSymbolicExp = new System.Windows.Forms.Button();
            this.buttonE = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.buttonFraction = new System.Windows.Forms.Button();
            this.buttonDecimalPoint = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.buttonPercent = new System.Windows.Forms.Button();
            this.buttonMultiply = new System.Windows.Forms.Button();
            this.buttonDivide = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSubtract = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonTabs = new System.Windows.Forms.TabControl();
            this.tabPageStandard = new System.Windows.Forms.TabPage();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonVariable = new System.Windows.Forms.Button();
            this.expressionDisplay = new Graphmatic.ExpressionDisplay();
            this.buttonTabs.SuspendLayout();
            this.tabPageStandard.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRight
            // 
            this.buttonRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRight.Image = global::Graphmatic.Properties.Resources.RightArrow;
            this.buttonRight.Location = new System.Drawing.Point(606, 209);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(39, 39);
            this.buttonRight.TabIndex = 1;
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLeft.Image = global::Graphmatic.Properties.Resources.LeftArrow;
            this.buttonLeft.Location = new System.Drawing.Point(561, 209);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(39, 39);
            this.buttonLeft.TabIndex = 2;
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Image = global::Graphmatic.Properties.Resources.Delete;
            this.buttonDelete.Location = new System.Drawing.Point(516, 209);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(39, 39);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRoot
            // 
            this.buttonRoot.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRoot.Image = global::Graphmatic.Properties.Resources.ButtonRoot;
            this.buttonRoot.Location = new System.Drawing.Point(186, 6);
            this.buttonRoot.Name = "buttonRoot";
            this.buttonRoot.Size = new System.Drawing.Size(42, 39);
            this.buttonRoot.TabIndex = 4;
            this.buttonRoot.UseVisualStyleBackColor = true;
            // 
            // buttonSqrt
            // 
            this.buttonSqrt.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSqrt.Image = global::Graphmatic.Properties.Resources.ButtonSqrt;
            this.buttonSqrt.Location = new System.Drawing.Point(234, 6);
            this.buttonSqrt.Name = "buttonSqrt";
            this.buttonSqrt.Size = new System.Drawing.Size(36, 39);
            this.buttonSqrt.TabIndex = 5;
            this.buttonSqrt.UseVisualStyleBackColor = true;
            // 
            // buttonLogE
            // 
            this.buttonLogE.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogE.Image = global::Graphmatic.Properties.Resources.ButtonLn;
            this.buttonLogE.Location = new System.Drawing.Point(186, 51);
            this.buttonLogE.Name = "buttonLogE";
            this.buttonLogE.Size = new System.Drawing.Size(84, 39);
            this.buttonLogE.TabIndex = 6;
            this.buttonLogE.UseVisualStyleBackColor = true;
            // 
            // buttonExp
            // 
            this.buttonExp.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExp.Image = global::Graphmatic.Properties.Resources.ButtonExp;
            this.buttonExp.Location = new System.Drawing.Point(6, 6);
            this.buttonExp.Name = "buttonExp";
            this.buttonExp.Size = new System.Drawing.Size(35, 39);
            this.buttonExp.TabIndex = 9;
            this.buttonExp.UseVisualStyleBackColor = true;
            // 
            // buttonLog10
            // 
            this.buttonLog10.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLog10.Image = global::Graphmatic.Properties.Resources.ButtonLog10;
            this.buttonLog10.Location = new System.Drawing.Point(96, 51);
            this.buttonLog10.Name = "buttonLog10";
            this.buttonLog10.Size = new System.Drawing.Size(84, 39);
            this.buttonLog10.TabIndex = 8;
            this.buttonLog10.UseVisualStyleBackColor = true;
            // 
            // buttonLogN
            // 
            this.buttonLogN.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogN.Image = global::Graphmatic.Properties.Resources.ButtonLogN;
            this.buttonLogN.Location = new System.Drawing.Point(6, 51);
            this.buttonLogN.Name = "buttonLogN";
            this.buttonLogN.Size = new System.Drawing.Size(84, 39);
            this.buttonLogN.TabIndex = 7;
            this.buttonLogN.UseVisualStyleBackColor = true;
            // 
            // buttonReciprocate
            // 
            this.buttonReciprocate.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReciprocate.Image = global::Graphmatic.Properties.Resources.ButtonReciprocate;
            this.buttonReciprocate.Location = new System.Drawing.Point(47, 6);
            this.buttonReciprocate.Name = "buttonReciprocate";
            this.buttonReciprocate.Size = new System.Drawing.Size(43, 39);
            this.buttonReciprocate.TabIndex = 12;
            this.buttonReciprocate.UseVisualStyleBackColor = true;
            // 
            // buttonCube
            // 
            this.buttonCube.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCube.Image = global::Graphmatic.Properties.Resources.ButtonCube;
            this.buttonCube.Location = new System.Drawing.Point(140, 6);
            this.buttonCube.Name = "buttonCube";
            this.buttonCube.Size = new System.Drawing.Size(40, 39);
            this.buttonCube.TabIndex = 11;
            this.buttonCube.UseVisualStyleBackColor = true;
            // 
            // buttonSquare
            // 
            this.buttonSquare.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSquare.Image = global::Graphmatic.Properties.Resources.ButtonSquare;
            this.buttonSquare.Location = new System.Drawing.Point(96, 6);
            this.buttonSquare.Name = "buttonSquare";
            this.buttonSquare.Size = new System.Drawing.Size(38, 39);
            this.buttonSquare.TabIndex = 10;
            this.buttonSquare.UseVisualStyleBackColor = true;
            // 
            // buttonAbsolute
            // 
            this.buttonAbsolute.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbsolute.Image = global::Graphmatic.Properties.Resources.ButtonAbs;
            this.buttonAbsolute.Location = new System.Drawing.Point(96, 96);
            this.buttonAbsolute.Name = "buttonAbsolute";
            this.buttonAbsolute.Size = new System.Drawing.Size(35, 39);
            this.buttonAbsolute.TabIndex = 18;
            this.buttonAbsolute.UseVisualStyleBackColor = true;
            // 
            // buttonPi
            // 
            this.buttonPi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPi.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPi.Image = global::Graphmatic.Properties.Resources.ButtonPi;
            this.buttonPi.Location = new System.Drawing.Point(426, 209);
            this.buttonPi.Name = "buttonPi";
            this.buttonPi.Size = new System.Drawing.Size(39, 39);
            this.buttonPi.TabIndex = 17;
            this.buttonPi.UseVisualStyleBackColor = true;
            // 
            // buttonBracket
            // 
            this.buttonBracket.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBracket.Image = global::Graphmatic.Properties.Resources.ButtonBracket;
            this.buttonBracket.Location = new System.Drawing.Point(137, 96);
            this.buttonBracket.Name = "buttonBracket";
            this.buttonBracket.Size = new System.Drawing.Size(43, 39);
            this.buttonBracket.TabIndex = 15;
            this.buttonBracket.UseVisualStyleBackColor = true;
            // 
            // buttonSymbolicExp
            // 
            this.buttonSymbolicExp.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSymbolicExp.Image = global::Graphmatic.Properties.Resources.ButtonExp10;
            this.buttonSymbolicExp.Location = new System.Drawing.Point(186, 96);
            this.buttonSymbolicExp.Name = "buttonSymbolicExp";
            this.buttonSymbolicExp.Size = new System.Drawing.Size(84, 39);
            this.buttonSymbolicExp.TabIndex = 14;
            this.buttonSymbolicExp.UseVisualStyleBackColor = true;
            // 
            // buttonE
            // 
            this.buttonE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonE.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonE.Image = global::Graphmatic.Properties.Resources.ButtonE;
            this.buttonE.Location = new System.Drawing.Point(471, 209);
            this.buttonE.Name = "buttonE";
            this.buttonE.Size = new System.Drawing.Size(39, 39);
            this.buttonE.TabIndex = 13;
            this.buttonE.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::Graphmatic.Properties.Resources.Button1;
            this.button1.Location = new System.Drawing.Point(315, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 39);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::Graphmatic.Properties.Resources.Button2;
            this.button2.Location = new System.Drawing.Point(352, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 39);
            this.button2.TabIndex = 20;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::Graphmatic.Properties.Resources.Button3;
            this.button3.Location = new System.Drawing.Point(389, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 39);
            this.button3.TabIndex = 21;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = global::Graphmatic.Properties.Resources.Button4;
            this.button4.Location = new System.Drawing.Point(315, 254);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 39);
            this.button4.TabIndex = 22;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::Graphmatic.Properties.Resources.Button5;
            this.button5.Location = new System.Drawing.Point(352, 254);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(31, 39);
            this.button5.TabIndex = 23;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Image = global::Graphmatic.Properties.Resources.Button6;
            this.button6.Location = new System.Drawing.Point(389, 254);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(31, 39);
            this.button6.TabIndex = 24;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Image = global::Graphmatic.Properties.Resources.Button7;
            this.button7.Location = new System.Drawing.Point(315, 209);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(31, 39);
            this.button7.TabIndex = 25;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Image = global::Graphmatic.Properties.Resources.Button8;
            this.button8.Location = new System.Drawing.Point(352, 209);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(31, 39);
            this.button8.TabIndex = 26;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Image = global::Graphmatic.Properties.Resources.Button9;
            this.button9.Location = new System.Drawing.Point(389, 209);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(31, 39);
            this.button9.TabIndex = 27;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // buttonFraction
            // 
            this.buttonFraction.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFraction.Image = global::Graphmatic.Properties.Resources.ButtonFraction;
            this.buttonFraction.Location = new System.Drawing.Point(47, 96);
            this.buttonFraction.Name = "buttonFraction";
            this.buttonFraction.Size = new System.Drawing.Size(43, 39);
            this.buttonFraction.TabIndex = 28;
            this.buttonFraction.UseVisualStyleBackColor = true;
            // 
            // buttonDecimalPoint
            // 
            this.buttonDecimalPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDecimalPoint.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDecimalPoint.Image = global::Graphmatic.Properties.Resources.ButtonDecimal;
            this.buttonDecimalPoint.Location = new System.Drawing.Point(352, 344);
            this.buttonDecimalPoint.Name = "buttonDecimalPoint";
            this.buttonDecimalPoint.Size = new System.Drawing.Size(31, 39);
            this.buttonDecimalPoint.TabIndex = 30;
            this.buttonDecimalPoint.UseVisualStyleBackColor = true;
            // 
            // button0
            // 
            this.button0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button0.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button0.Image = global::Graphmatic.Properties.Resources.Button0;
            this.button0.Location = new System.Drawing.Point(315, 344);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(31, 39);
            this.button0.TabIndex = 29;
            this.button0.UseVisualStyleBackColor = true;
            // 
            // buttonPercent
            // 
            this.buttonPercent.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPercent.Image = global::Graphmatic.Properties.Resources.ButtonPercent;
            this.buttonPercent.Location = new System.Drawing.Point(6, 96);
            this.buttonPercent.Name = "buttonPercent";
            this.buttonPercent.Size = new System.Drawing.Size(35, 39);
            this.buttonPercent.TabIndex = 31;
            this.buttonPercent.UseVisualStyleBackColor = true;
            // 
            // buttonMultiply
            // 
            this.buttonMultiply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMultiply.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMultiply.Image = global::Graphmatic.Properties.Resources.ButtonMultiply;
            this.buttonMultiply.Location = new System.Drawing.Point(426, 254);
            this.buttonMultiply.Name = "buttonMultiply";
            this.buttonMultiply.Size = new System.Drawing.Size(39, 39);
            this.buttonMultiply.TabIndex = 33;
            this.buttonMultiply.UseVisualStyleBackColor = true;
            // 
            // buttonDivide
            // 
            this.buttonDivide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDivide.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDivide.Image = global::Graphmatic.Properties.Resources.ButtonDivide;
            this.buttonDivide.Location = new System.Drawing.Point(471, 254);
            this.buttonDivide.Name = "buttonDivide";
            this.buttonDivide.Size = new System.Drawing.Size(39, 39);
            this.buttonDivide.TabIndex = 32;
            this.buttonDivide.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Image = global::Graphmatic.Properties.Resources.ButtonAdd;
            this.buttonAdd.Location = new System.Drawing.Point(426, 299);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(39, 39);
            this.buttonAdd.TabIndex = 35;
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonSubtract
            // 
            this.buttonSubtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSubtract.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSubtract.Image = global::Graphmatic.Properties.Resources.ButtonSubtract;
            this.buttonSubtract.Location = new System.Drawing.Point(471, 299);
            this.buttonSubtract.Name = "buttonSubtract";
            this.buttonSubtract.Size = new System.Drawing.Size(39, 39);
            this.buttonSubtract.TabIndex = 34;
            this.buttonSubtract.UseVisualStyleBackColor = true;
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDone.Image = global::Graphmatic.Properties.Resources.ButtonDone;
            this.buttonDone.Location = new System.Drawing.Point(426, 344);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(84, 39);
            this.buttonDone.TabIndex = 36;
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // buttonTabs
            // 
            this.buttonTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTabs.Controls.Add(this.tabPageStandard);
            this.buttonTabs.Location = new System.Drawing.Point(12, 216);
            this.buttonTabs.Name = "buttonTabs";
            this.buttonTabs.SelectedIndex = 0;
            this.buttonTabs.Size = new System.Drawing.Size(283, 167);
            this.buttonTabs.TabIndex = 37;
            // 
            // tabPageStandard
            // 
            this.tabPageStandard.Controls.Add(this.buttonExp);
            this.tabPageStandard.Controls.Add(this.buttonRoot);
            this.tabPageStandard.Controls.Add(this.buttonSqrt);
            this.tabPageStandard.Controls.Add(this.buttonLogE);
            this.tabPageStandard.Controls.Add(this.buttonLogN);
            this.tabPageStandard.Controls.Add(this.buttonLog10);
            this.tabPageStandard.Controls.Add(this.buttonPercent);
            this.tabPageStandard.Controls.Add(this.buttonSquare);
            this.tabPageStandard.Controls.Add(this.buttonCube);
            this.tabPageStandard.Controls.Add(this.buttonReciprocate);
            this.tabPageStandard.Controls.Add(this.buttonFraction);
            this.tabPageStandard.Controls.Add(this.buttonSymbolicExp);
            this.tabPageStandard.Controls.Add(this.buttonBracket);
            this.tabPageStandard.Controls.Add(this.buttonAbsolute);
            this.tabPageStandard.Location = new System.Drawing.Point(4, 24);
            this.tabPageStandard.Name = "tabPageStandard";
            this.tabPageStandard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStandard.Size = new System.Drawing.Size(275, 139);
            this.tabPageStandard.TabIndex = 1;
            this.tabPageStandard.Text = "Standard";
            this.tabPageStandard.UseVisualStyleBackColor = true;
            // 
            // buttonVariable
            // 
            this.buttonVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVariable.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVariable.Location = new System.Drawing.Point(389, 344);
            this.buttonVariable.Name = "buttonVariable";
            this.buttonVariable.Size = new System.Drawing.Size(31, 39);
            this.buttonVariable.TabIndex = 38;
            this.buttonVariable.Text = "X";
            this.buttonVariable.UseVisualStyleBackColor = true;
            // 
            // expressionDisplay
            // 
            this.expressionDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expressionDisplay.BackColor = System.Drawing.Color.White;
            this.expressionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expressionDisplay.DisplayScale = 3;
            this.expressionDisplay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expressionDisplay.Location = new System.Drawing.Point(12, 12);
            this.expressionDisplay.MoeinMode = false;
            this.expressionDisplay.Name = "expressionDisplay";
            this.expressionDisplay.Size = new System.Drawing.Size(633, 191);
            this.expressionDisplay.TabIndex = 0;
            // 
            // InputWindow
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 395);
            this.Controls.Add(this.buttonVariable);
            this.Controls.Add(this.buttonTabs);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonSubtract);
            this.Controls.Add(this.buttonMultiply);
            this.Controls.Add(this.buttonDivide);
            this.Controls.Add(this.buttonDecimalPoint);
            this.Controls.Add(this.button0);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonPi);
            this.Controls.Add(this.buttonE);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.expressionDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::Graphmatic.Properties.Resources.Equation;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(673, 254);
            this.Name = "InputWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputWindow_FormClosing);
            this.Load += new System.EventHandler(this.InputWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputWindow_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputWindow_KeyPress);
            this.buttonTabs.ResumeLayout(false);
            this.tabPageStandard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExpressionDisplay expressionDisplay;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRoot;
        private System.Windows.Forms.Button buttonSqrt;
        private System.Windows.Forms.Button buttonLogE;
        private System.Windows.Forms.Button buttonExp;
        private System.Windows.Forms.Button buttonLog10;
        private System.Windows.Forms.Button buttonLogN;
        private System.Windows.Forms.Button buttonReciprocate;
        private System.Windows.Forms.Button buttonCube;
        private System.Windows.Forms.Button buttonSquare;
        private System.Windows.Forms.Button buttonAbsolute;
        private System.Windows.Forms.Button buttonPi;
        private System.Windows.Forms.Button buttonBracket;
        private System.Windows.Forms.Button buttonSymbolicExp;
        private System.Windows.Forms.Button buttonE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button buttonFraction;
        private System.Windows.Forms.Button buttonDecimalPoint;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button buttonPercent;
        private System.Windows.Forms.Button buttonMultiply;
        private System.Windows.Forms.Button buttonDivide;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSubtract;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TabControl buttonTabs;
        private System.Windows.Forms.TabPage tabPageStandard;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonVariable;
    }
}