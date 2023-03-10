using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSib
{
    [Serializable]
    public class Task
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool isDone { get; set; }
        public int id { get; set; }
        public int idCur { get; set; }


    }
}
