using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Vecter
    {
        public double? R=null;
        public double? theta=null;
        
        public double? thetaPI
        {
            get { return theta * Math.PI / 180; }
        }
        public Vecter(double? r,double? theta)
        {
            if (r<0)
            {
                r = -r;
                theta += 180;
            }
            theta = theta % 360;
            R = r;
            this.theta = theta;
        }

        public static Vecter operator+(Vecter op1,Vecter op2)
        {
            try
            {
                double newX = op1.R.Value * Math.Sin(op1.theta.Value);
                double newY = op1.R.Value * Math.Cos(op1.theta.Value);

                double newR = Math.Sqrt(newX * newX + newY * newY);
                double newThera = Math.Atan2(newX, newY) * 180 / Math.PI;

                return new Vecter(newR, newThera);

            }
            catch (Exception)
            {

                return new Vecter(null, null);
            }
        }

        class mygen<T1,T2>
        {
            private T1 innerObj;
            public mygen(T1 INIT)
            {
                innerObj = INIT;
            }
            public mygen()
            {
                innerObj = default(T1);
            }


            public T1 InnerObj
            { 
               get
               {
                return innerObj;
               }
            }
        }
        
        public delegate void msghandler(string msgtxt);//先定义委托类型用于该事件

        class connection
        {
            public event msghandler msgArived;
            private System.Timers.Timer POLLER;
            public connection()
            {
                POLLER = new System.Timers.Timer(100);
           }

        }

       
    }
}
