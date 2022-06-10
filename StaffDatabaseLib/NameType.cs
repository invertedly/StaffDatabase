using System.Globalization;

namespace StaffDatabaseLib
{
    public sealed class NameType
    {
        public NameType(string sndName,
                        string fstName,
                        string patronymic)
        {
            _fstName = GetTitleCase(fstName);
            _sndName = GetTitleCase(sndName);
            _patronymic = GetTitleCase(patronymic);
        }

        private string _fstName;
        private string _sndName;
        private string _patronymic;

        public string FirstName
        {
            set { _fstName = GetTitleCase(value); }
            get { return _fstName; }
        }

        public string SecondName
        {
            set { _sndName = GetTitleCase(value); }
            get { return _sndName; }
        }

        public string Patronymic
        {
            set { _patronymic = GetTitleCase(value); }
            get { return _patronymic; }
        }

        public override string ToString()
        {
            return string.Concat(SecondName, " ", FirstName, " ", Patronymic);
        }

        private static string GetTitleCase(string str)
        {
            if (str == null)
            {
                return str;
            }

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            // there is no conversion from all capitals to title case
            string strLowerCase = str.ToLower();
            return textInfo.ToTitleCase(strLowerCase);
        }
    }
}
