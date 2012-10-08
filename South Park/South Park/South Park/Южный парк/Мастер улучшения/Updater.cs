using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace South_Park
{
    class Updater
    {


        public Updater()
        {
                 
        }
    


        public object UpdateTawer(object Tawer)
        {
            if (Tawer is CameraTawer)
            {
                if (((CameraTawer)Tawer).Level == 0)
                {
                    if (((CameraTawer)Tawer).Experience == 100)
                    {
                        ((CameraTawer)Tawer).Level = 2;
                        return Tawer;
                    }
                    return Tawer;
                }
               // return Tawer;
            }
            return Tawer;
                   
               
        }

    }
}
