using System;
using System.Globalization;
using System.Text;

namespace mrk.wordyformatprovider
{
    internal class WordyFormatProviderITA : IWordyFormatProvider
    {

        IFormatProvider _parent;

        public WordyFormatProviderITA() : this(CultureInfo.CreateSpecificCulture("it-IT")) { }
        public WordyFormatProviderITA(IFormatProvider parent)
        {
            _parent = parent;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }

        public string Format(string format, object arg, IFormatProvider prov)
        {
            // If it's not our format string, defer to the parent provider:
            if (arg == null || format == null || !format.StartsWith("W"))
                return string.Format(_parent, "{0:" + format + "}", arg);

            int c = format.IndexOf(".") == -1 ? 2 : int.Parse(format.Replace("W.", ""));
            if (arg is double || arg is float || arg is decimal)
            {
                decimal num = Convert.ToDecimal(arg);
                long parteIntera = (long)Decimal.Truncate(num);
                int parteDecimale = (int)(Math.Round(num - parteIntera, c) * (decimal)Math.Pow(10, c));
                return convertNumberToReadableString(parteIntera) + "/" + parteDecimale.ToString();
            }
            long l = 0;
            if (long.TryParse(arg.ToString(), out l))
            {
                return convertNumberToReadableString(l);
            }
            return "Invalid argument";
        }

        static readonly string[] unita = { "zero", "uno", "due", "tre", "quattro", "cinque", "sei", "sette", "otto", "nove", "dieci", "undici", "dodici", "tredici", "quattordici", "quindici", "sedici", "diciassette", "diciotto", "diciannove" };
        static readonly string[] decine = { "", "dieci", "venti", "trenta", "quaranta", "cinquanta", "sessanta", "settanta", "ottonta", "novanta" };

        public string convertNumberToReadableString(long num)
        {
            StringBuilder result = new StringBuilder();
            long mod = 0;
            long i = 0;
            if (num == 0)
                return "zero";

            if (num > 0 && num < 20)
            {
                result.Append(unita[num]);
            }
            else
            {
                if (num < 100)
                {
                    mod = num % 10;
                    i = num / 10;
                    switch (mod)
                    {
                        case 0:
                            result.Append(decine[i]);
                            break;
                        case 1:
                            result.Append(decine[i].Substring(0, decine[i].Length - 1));
                            result.Append(unita[mod]);
                            break;
                        case 8:
                            result.Append(decine[i].Substring(0, decine[i].Length - 1));
                            result.Append(unita[mod]);
                            break;
                        default:
                            result.Append(decine[i] + unita[mod]);
                            break;
                    }
                }
                else
                {
                    if (num < 1000)
                    {
                        mod = num % 100;
                        i = (num - mod) / 100;
                        switch (i)
                        {
                            case 1:
                                result.Append("cento");
                                break;
                            default:
                                result.Append(unita[i]);
                                result.Append("cento");
                                break;
                        }
                        result.Append(convertNumberToReadableString(mod));
                    }
                    else
                    {
                        if (num < 10000)
                        {
                            mod = num % 1000;
                            i = (num - mod) / 1000;
                            switch (i)
                            {
                                case 1:
                                    result.Append("mille");
                                    break;
                                default:
                                    result.Append(unita[i]);
                                    result.Append("mila");
                                    break;
                            }
                            result.Append(convertNumberToReadableString(mod));
                        }
                        else
                        {
                            if (num < 1000000)
                            {
                                mod = num % 1000;
                                i = (num - mod) / 1000;
                                switch ((num - mod) / 1000)
                                {
                                    default:
                                        if (i < 20)
                                        {
                                            result.Append(unita[i]);
                                            result.Append("mila");
                                        }
                                        else
                                        {
                                            result.Append(convertNumberToReadableString(i));
                                            result.Append("mila");
                                        }
                                        break;
                                }
                                result.Append(convertNumberToReadableString(mod));
                            }
                            else
                            {
                                if (num < 1000000000)
                                {
                                    mod = num % 1000000;
                                    i = (num - mod) / 1000000;
                                    switch (i)
                                    {
                                        case 1:
                                            result.Append("unmilione");
                                            break;

                                        default:
                                            result.Append(convertNumberToReadableString(i));
                                            result.Append("milioni");

                                            break;
                                    }
                                    result.Append(convertNumberToReadableString(mod));
                                }
                                else
                                {
                                    if (num < 1000000000000)
                                    {
                                        mod = num % 1000000000;
                                        i = (num - mod) / 1000000000;
                                        switch (i)
                                        {
                                            case 1:
                                                result.Append("unmiliardo");
                                                break;

                                            default:
                                                result.Append(convertNumberToReadableString(i));
                                                result.Append("miliardi");

                                                break;
                                        }
                                        result.Append(convertNumberToReadableString(mod));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
