namespace AqlaEvents
{
    partial class Settings
    {
        [System.Configuration.UserScopedSettingAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string[] SearchQueries
        {
            get
            {
                return ((string[])(this["SearchQueries"]));
            }
            set
            {
                this["SearchQueries"] = value;
            }
        }
    }
}