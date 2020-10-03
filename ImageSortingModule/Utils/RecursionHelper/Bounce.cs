using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.Utils.RecursionHelper
{
    //
    //Based on pattern from:
    //https://volgarev.me/2013/09/27/tail-recursion-and-trampolining-in-csharp.html
    //
    public sealed class Bounce<T1, T2, TResult>
    {
        public T1 Arg1 { get; private set; }
        public T2 Arg2 { get; private set; }
        public TResult Result { get; private set; }
        public bool HasResult { get; private set; }

        public static Bounce<T1, T2, TResult> Continue(T1 arg1, T2 arg2)
        {
            return new Bounce<T1, T2, TResult>
            {
                Arg1 = arg1,
                Arg2 = arg2,
                HasResult = false
            };
        }

        public static Bounce<T1, T2, TResult> End(TResult result)
        {
            return new Bounce<T1, T2, TResult>
            {
                Result = result,
                HasResult = true
            };
        }
    }
}
