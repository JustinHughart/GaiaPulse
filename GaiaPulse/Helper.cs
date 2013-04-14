using System;

namespace GaiaPulse
{
    static public class Helper //A class that helps with various functions.
    {
        public static bool FileNameValid(String Name)  //Checks if the String is a valid filename.
        {
            bool returnvalue = true;

            for (int i = 0; i < Name.Length; i++)
            {
                switch (Name[i])
                {
                    case 'a':
                        break;
                    case 'b':
                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e':
                        break;
                    case 'f':
                        break;
                    case 'g':
                        break;
                    case 'h':
                        break;
                    case 'i':
                        break;
                    case 'j':
                        break;
                    case 'k':
                        break;
                    case 'l':
                        break;
                    case 'm':
                        break;
                    case 'n':
                        break;
                    case 'o':
                        break;
                    case 'p':
                        break;
                    case 'q':
                        break;
                    case 'r':
                        break;
                    case 's':
                        break;
                    case 't':
                        break;
                    case 'u':
                        break;
                    case 'v':
                        break;
                    case 'w':
                        break;
                    case 'x':
                        break;
                    case 'y':
                        break;
                    case 'z':
                        break;
                    case 'A':
                        break;
                    case 'B':
                        break;
                    case 'C':
                        break;
                    case 'D':
                        break;
                    case 'E':
                        break;
                    case 'F':
                        break;
                    case 'G':
                        break;
                    case 'H':
                        break;
                    case 'I':
                        break;
                    case 'J':
                        break;
                    case 'K':
                        break;
                    case 'L':
                        break;
                    case 'M':
                        break;
                    case 'N':
                        break;
                    case 'O':
                        break;
                    case 'P':
                        break;
                    case 'Q':
                        break;
                    case 'R':
                        break;
                    case 'S':
                        break;
                    case 'T':
                        break;
                    case 'U':
                        break;
                    case 'V':
                        break;
                    case 'W':
                        break;
                    case 'X':
                        break;
                    case 'Y':
                        break;
                    case 'Z':
                        break;
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        break;
                    case '6':
                        break;
                    case '7':
                        break;
                    case '8':
                        break;
                    case '9':
                        break;
                    case '0':
                        break;
                    case '-':
                        break;
                    case '_':
                        break;
                    default:
                        returnvalue = false;
                        break;
                }
            }

            return returnvalue;
        }
    }
}