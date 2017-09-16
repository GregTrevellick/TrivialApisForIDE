namespace Trivial.Ui.Common
{
    public class ConstantsForAppCommon
    {
        public string GetCaption(string vsixName, string vsixVersion)
        {
            return vsixName + " " + vsixVersion;
        }

        public string GetInvalidInteger(string labelName)
        {
            return CommonConstants.IntegerInvalid + "'" + labelName + "'";
        }
    }
}