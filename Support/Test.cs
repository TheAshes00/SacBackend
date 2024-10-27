using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Test
    {
        public static bool boolCountNumberOfCoincidences(
            int[] arrintArray_I,
            int intNumberToCount_I,
            int intNumberOfRepetitionsExpected_I
            )
        {

            int intNumberOfCoincidencesInArray = arrintArray_I.Count( 
                element => element == intNumberToCount_I );

            return intNumberOfCoincidencesInArray >= intNumberOfRepetitionsExpected_I;
        }
    }
}
