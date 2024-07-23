using System;
using System.Windows.Forms;
using System.Threading;
using PRAManager;

namespace Bubbles
{
    internal partial class ProgressDlg : Form
	{
		public ProgressDlg()
		{
			InitializeComponent();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			m_abortPressed = true;
		}

		public bool m_abortPressed = false;
	}

	internal class ProgressDlgParams : Object
	{
		public ProgressDlgParams() { }
		public override string ToString()
		{
			return message;
		}
		public string message = "";
        public string count = "";
        public int minimum = 0;
		public int maximum = 1;
		public int value = 0;
		public int links = 0;
		public int brokenlinks = 0;
		public bool abortEnabled = true;
		public bool abortPressed = true;
		public string title = Utils.getString("dashboard.progressdialog.title");
		public string abortTitle = Utils.getString("dashboard.progressdialog.abort.caption");
	}

	internal class ThreadedProgressDlg : Object
	{
		private ProgressDlg m_dlg = null;
		private Thread m_thread = null;
		public ProgressDlgParams dlgParams = new ProgressDlgParams();
		protected bool m_threadStarted = false;
		protected bool m_abortThread = false;
		protected int m_showOperation = 0;

		~ThreadedProgressDlg()
		{
			Destroy();
			dlgParams = null;
		}

		private void threadFunc(Object aDlgParams)
		{
			m_threadStarted = true;
			m_dlg = new ProgressDlg();
			m_dlg.Text = (aDlgParams as ProgressDlgParams).title;
			m_dlg.btnAbort.Text = (aDlgParams as ProgressDlgParams).abortTitle;
			m_dlg.Message.Text = (aDlgParams as ProgressDlgParams).message;
            m_dlg.lblCount.Text = (aDlgParams as ProgressDlgParams).count;
            m_dlg.pbTopics.Minimum = (aDlgParams as ProgressDlgParams).minimum;
			m_dlg.pbTopics.Maximum = (aDlgParams as ProgressDlgParams).maximum;
			m_dlg.pbTopics.Value = (aDlgParams as ProgressDlgParams).value;
			m_dlg.btnAbort.Enabled = (aDlgParams as ProgressDlgParams).abortEnabled;
			try
			{
				while (true)
				{
					if (m_abortThread)
					{
						m_threadStarted = false;
						break;
					}
					try
					{
						if (m_dlg.Message.Text != dlgParams.message)
							m_dlg.Message.Text = dlgParams.message;
                        if (m_dlg.lblCount.Text != dlgParams.count)
                            m_dlg.lblCount.Text = dlgParams.count;
                        if (m_dlg.pbTopics.Minimum != dlgParams.minimum)
							m_dlg.pbTopics.Minimum = dlgParams.minimum;
						if (m_dlg.pbTopics.Maximum != dlgParams.maximum)
							m_dlg.pbTopics.Maximum = dlgParams.maximum;
						if (m_dlg.pbTopics.Value != dlgParams.value)
							m_dlg.pbTopics.Value = dlgParams.value;
					}
					catch { }
					if (m_dlg.btnAbort.Enabled != dlgParams.abortEnabled)
						m_dlg.btnAbort.Enabled = dlgParams.abortEnabled;

					dlgParams.abortPressed = m_dlg.m_abortPressed;

					try
					{
						if (m_showOperation == 1 && !m_dlg.Visible)
							m_dlg.Show();
						else if (m_showOperation == 2 && m_dlg.Visible) // only Hide() set m_showOperation to 2
							m_dlg.Hide();
					}
					catch { }

					Application.DoEvents();
					Thread.Sleep(m_dlg.Visible ? 15 : 1000);
				}
			}
			catch (ThreadAbortException)
			{
				if (m_dlg != null)
					this.Destroy();
			}
		}

		public void Create()
		{	
			m_thread = new Thread(new ParameterizedThreadStart(threadFunc));
			m_thread.Start(dlgParams);
		}

		public void Destroy()
		{
			if (m_threadStarted)
			{
				m_abortThread = true;
				for (int i = 0; i < 100; i++)
				{
					if (!m_threadStarted)
						break;
					Thread.Sleep(15);
				}
			}
			if (m_threadStarted)
				m_thread.Abort();

			if (m_dlg != null)
				m_dlg.Dispose();
		}

		public void Show()
		{
			try
			{
				if (m_threadStarted)
				{
					m_showOperation = 1;
					m_dlg.m_abortPressed = false;
				}
				dlgParams.abortPressed = false;

				if (!m_dlg.Visible)
					m_dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
			}
			catch (Exception _e) 
			{
				if (m_dlg != null)
					this.Destroy();
			}
		}

		public void Hide()
		{
			if (m_threadStarted)
				m_showOperation = 2;
		}

        public bool AbortPressed => dlgParams.abortPressed;
    }
}
