using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.Utils.RecursionHelper
{
    //
    //Based on pattern from:
    //https://volgarev.me/2013/09/27/tail-recursion-and-trampolining-in-csharp.html
    //
    static class Trampoline
    {
        public static TResult Start<T1, T2, TResult>(Func<T1, T2, Bounce<T1, T2, TResult>> action, T1 arg1, T2 arg2)
        {
            TResult result = default;
            Bounce<T1, T2, TResult> bounce = Bounce<T1, T2, TResult>.Continue(arg1, arg2);

            while(true)
            {
                if (bounce.HasResult)
                {
                    result = bounce.Result;
                    break;
                }
            }

            bounce = action(bounce.Arg1, bounce.Arg2);

            return result;
        }
    }
}
