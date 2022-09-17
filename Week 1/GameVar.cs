using System.Collections.Generic;
using System.Diagnostics;

namespace AGAIN
{
    public static class GameVar
    {
        public static List<GObject> ObjCollection = new List<GObject> { };
        public static Stopwatch GameWatch = new Stopwatch();
    }
}