using System.ComponentModel;

namespace OnlineQuiz.Common
{
    public enum AlertClass
    {
        [Description("success")]
        Success = 1,
        [Description("error")]
        Error = 2,
        [Description("warning")]
        Warning = 3
    }
}
