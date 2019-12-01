using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Szakdolgozat.Persistence.Structures
{
    public static class DataConverter
    {
        public static List<Preference> StringToPreferenceList(string input)
        {
            string numRegex = "-?[0-9][0-9]*";
            string codeRegex = numRegex + "(:" + numRegex + ")*";
            List<Preference> preferenceList = new List<Preference>();
            List<string> lines = input.Split(';').ToList();
            lines.RemoveAll(x => !Regex.Match(x, codeRegex).Success);
            /*
            foreach(string line in lines)
            {
                List<string> data = line.Split(':').ToList();
                preferenceList.Add(new Preference(
                    data.Select(x => Convert.ToInt32(x)).ToList()
                ));
            }*/

            return preferenceList;
        }

        public static string PreferenceListToString(List<Preference> input)
        {
            string data = "";
            /*
            foreach(Preference preference in input)
            {
                data += preference[0];
                foreach(int ID in preference.Skip(1))
                {
                    data += ":" + ID;
                }
                data += Environment.NewLine;
            }
            */
            return data;
        }

        //TODO: Replace this temporary bandage
        public class Preference
        {

        }
    }
}
