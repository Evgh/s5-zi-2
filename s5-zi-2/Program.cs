using System;

namespace s5_zi_2
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskExecutor.Execute("abcdefghijklmnopqrstuvwxyz",
                                 @"textfiles/english.txt",
                                 "KasperovichEugeniyaNikolaevna"
                                );

            TaskExecutor.Execute("абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
                                 @"textfiles\russian.txt",
                                 "КасперовичЕвгенияНиколаевна"
                                );

            TaskExecutor.Execute("01", 
                                 @"textfiles/binary.txt", 
                                 "11001010 11000101"
                                );
        }        
    }
}
