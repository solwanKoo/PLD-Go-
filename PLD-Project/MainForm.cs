/*
 * Created by SharpDevelop.
 * User: AS
 * Date: 21/05/2025
 * Time: 11:04 ص
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using com.calitha.goldparser;
namespace PLD_Project
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		MyParser p;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			p=new MyParser("Go_pld_edited.cgt",listBox1);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			p.Parse(textBox1.Text);
		}
	}
}
