namespace MT040WindowsForms同步內容運作機制
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.按鈕1 = new System.Windows.Forms.Button();
            this.按鈕3 = new System.Windows.Forms.Button();
            this.按鈕2 = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 按鈕1
            // 
            this.按鈕1.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.按鈕1.Location = new System.Drawing.Point(123, 80);
            this.按鈕1.Name = "按鈕1";
            this.按鈕1.Size = new System.Drawing.Size(168, 67);
            this.按鈕1.TabIndex = 0;
            this.按鈕1.Text = "按鈕1";
            this.按鈕1.UseVisualStyleBackColor = true;
            this.按鈕1.Click += new System.EventHandler(this.按鈕1_Click);
            // 
            // 按鈕3
            // 
            this.按鈕3.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.按鈕3.Location = new System.Drawing.Point(123, 218);
            this.按鈕3.Name = "按鈕3";
            this.按鈕3.Size = new System.Drawing.Size(168, 67);
            this.按鈕3.TabIndex = 0;
            this.按鈕3.Text = "按鈕3";
            this.按鈕3.UseVisualStyleBackColor = true;
            this.按鈕3.Click += new System.EventHandler(this.按鈕3_Click);
            // 
            // 按鈕2
            // 
            this.按鈕2.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.按鈕2.Location = new System.Drawing.Point(461, 80);
            this.按鈕2.Name = "按鈕2";
            this.按鈕2.Size = new System.Drawing.Size(168, 67);
            this.按鈕2.TabIndex = 0;
            this.按鈕2.Text = "按鈕2";
            this.按鈕2.UseVisualStyleBackColor = true;
            this.按鈕2.Click += new System.EventHandler(this.按鈕2_Click);
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Font = new System.Drawing.Font("新細明體", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Message.ForeColor = System.Drawing.Color.Red;
            this.Message.Location = new System.Drawing.Point(68, 361);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(101, 37);
            this.Message.TabIndex = 1;
            this.Message.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.按鈕2);
            this.Controls.Add(this.按鈕3);
            this.Controls.Add(this.按鈕1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 按鈕1;
        private System.Windows.Forms.Button 按鈕3;
        private System.Windows.Forms.Button 按鈕2;
        private System.Windows.Forms.Label Message;
    }
}

