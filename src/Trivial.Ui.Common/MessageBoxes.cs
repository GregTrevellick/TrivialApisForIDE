using System;
using System.Windows.Forms;

namespace Trivial.Ui.Common
{
    public class MessageBoxes
    {
        public static bool ConfirmCloseWithoutSubmitingAnswer(string optionsName)
        {
            var result = false;

            var box = MessageBox.Show(
                $"You clicked the 'Close' button without having submitted your answer - are you sure ?{Environment.NewLine}{Environment.NewLine}You can disable this warning in Tools | Options | {optionsName}",
                string.Empty,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                result = true;
            }

            return result;
        }

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
