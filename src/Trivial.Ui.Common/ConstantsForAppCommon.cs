namespace Trivial.Ui.Common
{
    public class ConstantsForAppCommon
    {
        private string _vsixName;
        private string _vsixVersion;

        public ConstantsForAppCommon()
        {
        }

        public ConstantsForAppCommon(string vsixName, string vsixVersion)
        {
            _vsixName = vsixName;
            _vsixVersion = vsixVersion;
        }

        public string Caption
        {
            get
            {
                return _vsixName + " " + _vsixVersion;
            }
        }

        public string GetInvalidInteger(string labelName)
        {
            return CommonConstants.IntegerInvalid + labelName;
        }
    }
}
