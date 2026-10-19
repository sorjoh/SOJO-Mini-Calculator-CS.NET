using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SOJO_Mini_Calculator
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Main : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            Zero = new Button();
            Zero.Click += new EventHandler(NumberInput_Click);
            Dot = new Button();
            Dot.Click += new EventHandler(DecimalInput_Click);
            Equal = new Button();
            Equal.Click += new EventHandler(Equal_Click);
            Plus = new Button();
            Plus.Click += new EventHandler(OperatorInput_Click);
            One = new Button();
            One.Click += new EventHandler(NumberInput_Click);
            Two = new Button();
            Two.Click += new EventHandler(NumberInput_Click);
            Three = new Button();
            Three.Click += new EventHandler(NumberInput_Click);
            Minus = new Button();
            Minus.Click += new EventHandler(OperatorInput_Click);
            Four = new Button();
            Four.Click += new EventHandler(NumberInput_Click);
            Five = new Button();
            Five.Click += new EventHandler(NumberInput_Click);
            Six = new Button();
            Six.Click += new EventHandler(NumberInput_Click);
            Divide = new Button();
            Divide.Click += new EventHandler(OperatorInput_Click);
            Seven = new Button();
            Seven.Click += new EventHandler(NumberInput_Click);
            Eight = new Button();
            Eight.Click += new EventHandler(NumberInput_Click);
            Nine = new Button();
            Nine.Click += new EventHandler(NumberInput_Click);
            Multiply = new Button();
            Multiply.Click += new EventHandler(OperatorInput_Click);
            DisplayTable = new TableLayoutPanel();
            Display = new Label();
            DisplayTable.SuspendLayout();
            SuspendLayout();
            // 
            // Zero
            // 
            Zero.BackColor = Color.Black;
            Zero.Font = new Font("MS PGothic", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Zero.ForeColor = Color.White;
            Zero.Location = new Point(8, 248);
            Zero.Name = "Zero";
            Zero.Padding = new Padding(3, 0, 0, 0);
            Zero.Size = new Size(54, 54);
            Zero.TabIndex = 0;
            Zero.Tag = "Number";
            Zero.Text = "0";
            Zero.UseVisualStyleBackColor = false;
            // 
            // Dot
            // 
            Dot.BackColor = Color.Gainsboro;
            Dot.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Dot.ForeColor = Color.Black;
            Dot.Location = new Point(68, 248);
            Dot.Name = "Dot";
            Dot.Padding = new Padding(3, 0, 0, 0);
            Dot.Size = new Size(54, 54);
            Dot.TabIndex = 10;
            Dot.Tag = "Function";
            Dot.Text = ".";
            Dot.UseVisualStyleBackColor = false;
            // 
            // Equal
            // 
            Equal.BackColor = Color.Gainsboro;
            Equal.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold);
            Equal.ForeColor = Color.Black;
            Equal.Location = new Point(128, 248);
            Equal.Name = "Equal";
            Equal.Padding = new Padding(3, 0, 0, 0);
            Equal.Size = new Size(54, 54);
            Equal.TabIndex = 11;
            Equal.Tag = "Function";
            Equal.Text = "=";
            Equal.UseVisualStyleBackColor = false;
            // 
            // Plus
            // 
            Plus.BackColor = Color.Gainsboro;
            Plus.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold);
            Plus.ForeColor = Color.Black;
            Plus.Location = new Point(188, 188);
            Plus.Name = "Plus";
            Plus.Padding = new Padding(3, 0, 0, 0);
            Plus.Size = new Size(54, 54);
            Plus.TabIndex = 13;
            Plus.Tag = "Function";
            Plus.Text = "+";
            Plus.UseVisualStyleBackColor = false;
            // 
            // One
            // 
            One.BackColor = Color.Black;
            One.Font = new Font("MS PGothic", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            One.ForeColor = Color.White;
            One.Location = new Point(8, 188);
            One.Name = "One";
            One.Padding = new Padding(3, 0, 0, 0);
            One.Size = new Size(54, 54);
            One.TabIndex = 1;
            One.Tag = "Number";
            One.Text = "1";
            One.UseVisualStyleBackColor = false;
            // 
            // Two
            // 
            Two.BackColor = Color.Black;
            Two.Font = new Font("MS PGothic", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Two.ForeColor = Color.White;
            Two.Location = new Point(68, 188);
            Two.Name = "Two";
            Two.Padding = new Padding(3, 0, 0, 0);
            Two.Size = new Size(54, 54);
            Two.TabIndex = 2;
            Two.Tag = "Number";
            Two.Text = "2";
            Two.UseVisualStyleBackColor = false;
            // 
            // Three
            // 
            Three.BackColor = Color.Black;
            Three.Font = new Font("MS PGothic", 27.75f);
            Three.ForeColor = Color.White;
            Three.Location = new Point(128, 188);
            Three.Name = "Three";
            Three.Padding = new Padding(3, 0, 0, 0);
            Three.Size = new Size(54, 54);
            Three.TabIndex = 3;
            Three.Tag = "Number";
            Three.Text = "3";
            Three.UseVisualStyleBackColor = false;
            // 
            // Minus
            // 
            Minus.BackColor = Color.Gainsboro;
            Minus.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold);
            Minus.ForeColor = Color.Black;
            Minus.Location = new Point(188, 248);
            Minus.Name = "Minus";
            Minus.Padding = new Padding(3, 0, 0, 0);
            Minus.Size = new Size(54, 54);
            Minus.TabIndex = 12;
            Minus.Tag = "Function";
            Minus.Text = "-";
            Minus.UseVisualStyleBackColor = false;
            // 
            // Four
            // 
            Four.BackColor = Color.Black;
            Four.Font = new Font("MS PGothic", 27.75f);
            Four.ForeColor = Color.White;
            Four.Location = new Point(8, 129);
            Four.Name = "Four";
            Four.Padding = new Padding(3, 0, 0, 0);
            Four.Size = new Size(54, 54);
            Four.TabIndex = 4;
            Four.Tag = "Number";
            Four.Text = "4";
            Four.UseVisualStyleBackColor = false;
            // 
            // Five
            // 
            Five.BackColor = Color.Black;
            Five.Font = new Font("MS PGothic", 27.75f);
            Five.ForeColor = Color.White;
            Five.Location = new Point(68, 129);
            Five.Name = "Five";
            Five.Padding = new Padding(3, 0, 0, 0);
            Five.Size = new Size(54, 54);
            Five.TabIndex = 5;
            Five.Tag = "Number";
            Five.Text = "5";
            Five.UseVisualStyleBackColor = false;
            // 
            // Six
            // 
            Six.BackColor = Color.Black;
            Six.Font = new Font("MS PGothic", 27.75f);
            Six.ForeColor = Color.White;
            Six.Location = new Point(128, 129);
            Six.Name = "Six";
            Six.Padding = new Padding(3, 0, 0, 0);
            Six.Size = new Size(54, 54);
            Six.TabIndex = 6;
            Six.Tag = "Number";
            Six.Text = "6";
            Six.UseVisualStyleBackColor = false;
            // 
            // Divide
            // 
            Divide.BackColor = Color.Gainsboro;
            Divide.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold);
            Divide.ForeColor = Color.Black;
            Divide.Location = new Point(188, 129);
            Divide.Name = "Divide";
            Divide.Padding = new Padding(3, 0, 0, 0);
            Divide.Size = new Size(54, 54);
            Divide.TabIndex = 14;
            Divide.Tag = "Function";
            Divide.Text = "/";
            Divide.UseVisualStyleBackColor = false;
            // 
            // Seven
            // 
            Seven.BackColor = Color.Black;
            Seven.Font = new Font("MS PGothic", 27.75f);
            Seven.ForeColor = Color.White;
            Seven.Location = new Point(8, 69);
            Seven.Name = "Seven";
            Seven.Padding = new Padding(3, 0, 0, 0);
            Seven.Size = new Size(54, 54);
            Seven.TabIndex = 7;
            Seven.Tag = "Number";
            Seven.Text = "7";
            Seven.UseVisualStyleBackColor = false;
            // 
            // Eight
            // 
            Eight.BackColor = Color.Black;
            Eight.Font = new Font("MS PGothic", 27.75f);
            Eight.ForeColor = Color.White;
            Eight.Location = new Point(68, 69);
            Eight.Name = "Eight";
            Eight.Padding = new Padding(3, 0, 0, 0);
            Eight.Size = new Size(54, 54);
            Eight.TabIndex = 8;
            Eight.Tag = "Number";
            Eight.Text = "8";
            Eight.UseVisualStyleBackColor = false;
            // 
            // Nine
            // 
            Nine.BackColor = Color.Black;
            Nine.Font = new Font("MS PGothic", 27.75f);
            Nine.ForeColor = Color.White;
            Nine.Location = new Point(128, 69);
            Nine.Name = "Nine";
            Nine.Padding = new Padding(3, 0, 0, 0);
            Nine.Size = new Size(54, 54);
            Nine.TabIndex = 9;
            Nine.Tag = "Number";
            Nine.Text = "9";
            Nine.UseVisualStyleBackColor = false;
            // 
            // Multiply
            // 
            Multiply.BackColor = Color.Gainsboro;
            Multiply.Font = new Font("MS PGothic", 21.75f, FontStyle.Bold);
            Multiply.ForeColor = Color.Black;
            Multiply.Location = new Point(188, 69);
            Multiply.Name = "Multiply";
            Multiply.Padding = new Padding(3, 0, 0, 0);
            Multiply.Size = new Size(54, 54);
            Multiply.TabIndex = 15;
            Multiply.Tag = "Function";
            Multiply.Text = "*";
            Multiply.UseVisualStyleBackColor = false;
            // 
            // DisplayTable
            // 
            DisplayTable.BackColor = Color.Black;
            DisplayTable.ColumnCount = 1;
            DisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f));
            DisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20.0f));
            DisplayTable.Controls.Add(Display, 0, 0);
            DisplayTable.Location = new Point(3, 5);
            DisplayTable.Name = "DisplayTable";
            DisplayTable.RowCount = 1;
            DisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f));
            DisplayTable.Size = new Size(245, 54);
            DisplayTable.TabIndex = 27;
            // 
            // Display
            // 
            Display.AutoEllipsis = true;
            Display.AutoSize = true;
            Display.BackColor = Color.Black;
            Display.Dock = DockStyle.Fill;
            Display.Font = new Font("MS PGothic", 24.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Display.ForeColor = Color.White;
            Display.Location = new Point(6, 6);
            Display.Margin = new Padding(6, 6, 6, 0);
            Display.Name = "Display";
            Display.Size = new Size(233, 48);
            Display.TabIndex = 42;
            Display.Text = "0";
            Display.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(252, 311);
            Controls.Add(DisplayTable);
            Controls.Add(Multiply);
            Controls.Add(Nine);
            Controls.Add(Eight);
            Controls.Add(Seven);
            Controls.Add(Divide);
            Controls.Add(Six);
            Controls.Add(Five);
            Controls.Add(Four);
            Controls.Add(Minus);
            Controls.Add(Three);
            Controls.Add(Two);
            Controls.Add(One);
            Controls.Add(Plus);
            Controls.Add(Equal);
            Controls.Add(Dot);
            Controls.Add(Zero);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            KeyPreview = true;
            Name = "Main";
            Text = "SOJO Mini Calculator";
            DisplayTable.ResumeLayout(false);
            DisplayTable.PerformLayout();
            ControlAdded += new ControlEventHandler(Main_ControlAdded);
            KeyPress += new KeyPressEventHandler(Main_KeyPress);
            KeyDown += new KeyEventHandler(Main_KeyDown);
            Load += new EventHandler(Main_Load);
            ResumeLayout(false);

        }
        internal Button Zero;
        internal Button Dot;
        internal Button Equal;
        internal Button Plus;
        internal Button One;
        internal Button Two;
        internal Button Three;
        internal Button Minus;
        internal Button Four;
        internal Button Five;
        internal Button Six;
        internal Button Divide;
        internal Button Seven;
        internal Button Eight;
        internal Button Nine;
        internal Button Multiply;
        internal TableLayoutPanel DisplayTable;
        internal Label Display;

    }
}