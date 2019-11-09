using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace GetInheritanceType
{
    public static class AssemblyUtils
    {
        // 指定した型を継承した子型を返す。（直下の子のみ。子孫以下であっても対象外）
        public static Type[] GetChildren<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(c => c.BaseType == typeof(T)).ToArray();
        }
        // 指定した型を継承した子孫型を返す。（指定した型は対象外）
        public static Type[] GetInheritanceTypes<T>()
        {
            return _GetInheritanceTypes<T>().ToArray();
        }
        private static IEnumerable<Type> _GetInheritanceTypes<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsSubclassOf(typeof(T)));
        }
        // 指定した型の末裔を返す。（指定した型や末裔以前の型は対象外）
        public static Type[] GetEndInheritanceTypes<T>()
        {
            var types = _GetInheritanceTypes<T>().ToList();
            for (int t1=types.Count-1; t1>=0; t1--) {
                for (int t2=types.Count-1; t2>=0; t2--) {
                    if (types[t2] == types[t1].BaseType) { types.RemoveAt(t2); }
                }
            }
            return types.ToArray();
        }
    }
}
