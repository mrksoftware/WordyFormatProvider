using System;
using System.Globalization;

namespace mrk.wordyformatprovider
{
    public class WordyFormatProvider
    {
        protected WordyFormatProvider()
        {

        }

        public static IWordyFormatProvider Create(string cultureInfo)
        {
            switch (cultureInfo)
            {
                case ("it-IT"):
                    return new WordyFormatProviderITA();
                default:
                    throw new NotSupportedException($"{cultureInfo} still not supported");

            }
        }
    }
}
