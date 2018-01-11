using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {        
        System.Nullable<int> nullableInt;
        int? nullable=2;
        int? val1 = nullable * 2;
        int val2 = (int)nullable * 2;
        int val3 = nullable.Value * 3;
        int? val4 = 2;
        int val5 = val4 * 2 ?? 5;//此处自动进行类型转换，？？运算符在初始化时不需要进行IF判断语句的使用
        Console.WriteLine("out vla5={0}", val5);
        
        List<int> MyCollc = new List<int>();
        int[] array1 = new int[5] { 1, 2, 3, 4, 5 };
        MyCollc.AddRange(array1);
        Console.WriteLine("myCollc[3]={0}", MyCollc[3]);
        Console.ReadLine();

        Dictionary<string, int> kv = new Dictionary<string, int>();
        kv.Add("no1", 1);
        kv.Add("no2", 2);
        foreach ( KeyValuePair<string,int> thing in kv)
        {
            
        }




        }
    }
}
