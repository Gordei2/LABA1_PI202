namespace Converter
{
    partial class Form1
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
            this.binary_button = new System.Windows.Forms.Button();
            this.hex_button = new System.Windows.Forms.Button();
            this.number_field = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // binary_button
            // 
            this.binary_button.Location = new System.Drawing.Point(82, 173);
            this.binary_button.Name = "binary_button";
            this.binary_button.Size = new System.Drawing.Size(75, 23);
            this.binary_button.TabIndex = 0;
            this.binary_button.Text = "To binary";
            this.binary_button.UseVisualStyleBackColor = true;
            this.binary_button.Click += new System.EventHandler(this.hexClick);
            // 
            // hex_button
            // 
            this.hex_button.Location = new System.Drawing.Point(283, 173);
            this.hex_button.Name = "hex_button";
            this.hex_button.Size = new System.Drawing.Size(75, 23);
            this.hex_button.TabIndex = 1;
            this.hex_button.Text = "To hex";
            this.hex_button.UseVisualStyleBackColor = true;
            this.hex_button.Click += new System.EventHandler(this.binaryClick);
            // 
            // number_field
            // 
            this.number_field.Location = new System.Drawing.Point(134, 84);
            this.number_field.Name = "number_field";
            this.number_field.Size = new System.Drawing.Size(170, 20);
            this.number_field.TabIndex = 2;
            this.number_field.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnTextChanged);
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(214, 260);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(0, 13);
            this.result.TabIndex = 3;
            this.result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 357);
            this.Controls.Add(this.result);
            this.Controls.Add(this.number_field);
            this.Controls.Add(this.hex_button);
            this.Controls.Add(this.binary_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button binary_button;
        private System.Windows.Forms.Button hex_button;
        private System.Windows.Forms.TextBox number_field;
        private System.Windows.Forms.Label result;
    }
}

