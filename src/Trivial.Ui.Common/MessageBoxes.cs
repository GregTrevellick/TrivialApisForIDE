using System.Windows.Forms;

namespace Trivial.Ui.Common
{
    public class MessageBoxes
    {
        public static void DisplayInvalidIntegerError(string labelName, string caption)
        {
            MessageBox.Show(
                CommonConstants.GetInvalidInteger(labelName),
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static bool ConfirmProceedToSaveTimeOutInMilliSeconds(string caption)
        {
            var proceedToSave = false;

            var box = MessageBox.Show(
                CommonConstants.TimeOutInMilliSecondsIsOutsideRecommendedTimeoutLimits,
                caption,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                proceedToSave = true;
            }

            return proceedToSave;
        }
    }
}
