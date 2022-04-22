using System;
using System.Windows;

namespace Trophy
{
	public partial class TitleInput : Window
	{
		public TitleInput(string defaultAnswer = "")
		{
			InitializeComponent();
			txtAnswer.Text = defaultAnswer;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtAnswer.SelectAll();
			txtAnswer.Focus();
		}

		public string Answer
		{
			get { return txtAnswer.Text; }
		}
	}
}
