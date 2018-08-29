using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewParserForm
{
    class StringAccessors
    {
        private static StringAccessors instance = null;
        private static readonly object padlock = new object();
        private string code = "";

        StringAccessors()
        {
        }

        public static StringAccessors Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StringAccessors();
                    }
                    return instance;
                }
            }
        }
        public void setCode(string currentCode)
        {
            code = currentCode;
        }
        public string getCode()
        {
            return code;
        }
    }
}
