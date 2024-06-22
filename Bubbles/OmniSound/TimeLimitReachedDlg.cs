using System.Windows.Forms;

namespace Bubbles
{
    public partial class TimeLimitReachedDlg : Form
    {
        public TimeLimitReachedDlg()
        {
            InitializeComponent();

            Text = Utils.getString("TimeLimitReachedDlg.time_limit.title");
            lblMessage.Text = Utils.getString("TimeLimitReachedDlg.time_limit");
            btnResume.Text = Utils.getString("TimeLimitReachedDlg.btnResume");
            btnStop.Text = Utils.getString("TimeLimitReachedDlg.btnStop");
            btnCancel.Text = Utils.getString("TimeLimitReachedDlg.btnCancel");
        }
    }
}
