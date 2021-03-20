/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 2/9/2016
 * Time: 9:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace blnk
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// statusLabel
			// 
			this.statusLabel.AllowDrop = true;
			this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.statusLabel.AutoSize = true;
			this.statusLabel.Location = new System.Drawing.Point(55, 3);
			this.statusLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 23);
			this.statusLabel.TabIndex = 0;
			this.statusLabel.DragOver += new System.Windows.Forms.DragEventHandler(this.StatusLabelDragOver);
			this.statusLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.StatusLabelDragDrop);
			this.statusLabel.DragLeave += new System.EventHandler(this.StatusLabelDragLeave);
			this.statusLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.StatusLabelDragEnter);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(166, 57);
			this.Controls.Add(this.statusLabel);
			this.Font = new System.Drawing.Font("Liberation Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.Name = "MainForm";
			this.Text = "blnk";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.DragLeave += new System.EventHandler(this.MainFormDragLeave);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFormDragEnter);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainFormDragOver);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label statusLabel;
	}
}
