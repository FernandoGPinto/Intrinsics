using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;

namespace Intrinsics
{
    public static class IntrinsicsExtentions
    {
        public static double MaxWithIntrinsics(this double[] source) => Max(source);
        public static double MinWithIntrinsics(this double[] source) => Min(source);
        public static double AverageWithIntrinsics(this double[] source) => Average(source);
        public static double SumWithIntrinsics(this double[] source) => Sum(source);

        private static unsafe double Max(ReadOnlySpan<double> source)
        {
            double value;
            var i = Vector256<double>.Count;

            fixed (double* pSource = source)
            {
                Vector256<double> vresult = Avx.LoadVector256(pSource);

                while (i + Vector256<double>.Count < source.Length)
                {
                    vresult = Avx.Max(vresult, Avx.LoadVector256(pSource + i));
                    i += Vector256<double>.Count;
                }

                value = vresult.GetElement(0);
                for (int j = 1; j < Vector256<double>.Count; j++)
                {
                    if (vresult.GetElement(j) > value)
                    {
                        value = vresult.GetElement(j);
                    }
                }
            }

            for (int k = i; (uint)k < (uint)source.Length; k++)
            {
                if (source[k] > value)
                {
                    value = source[k];
                }
            }

            return value;
        }

        private static unsafe double Min(ReadOnlySpan<double> source)
        {
            double value;
            var i = Vector256<double>.Count;

            fixed (double* pSource = source)
            {
                Vector256<double> vresult = Avx.LoadVector256(pSource);

                while (i + Vector256<double>.Count < source.Length)
                {
                    vresult = Avx.Min(vresult, Avx.LoadVector256(pSource + i));
                    i += Vector256<double>.Count;
                }

                value = vresult.GetElement(0);
                for (int j = 1; j < Vector256<double>.Count; j++)
                {
                    if (vresult.GetElement(j) < value)
                    {
                        value = vresult.GetElement(j);
                    }
                }
            }

            for (int k = i; (uint)k < (uint)source.Length; k++)
            {
                if (source[k] < value)
                {
                    value = source[k];
                }
            }

            return value;
        }

        private static unsafe double Average(ReadOnlySpan<double> source)
        {
            double value;
            var i = Vector256<double>.Count;

            fixed (double* pSource = source)
            {
                Vector256<double> vresult = Avx.LoadVector256(pSource);

                while (i + Vector256<double>.Count < source.Length)
                {
                    vresult = Avx.Add(vresult, Avx.LoadVector256(pSource + i));
                    i += Vector256<double>.Count;
                }

                vresult = Avx.HorizontalAdd(vresult, vresult);

                var vleft = Avx.ExtractVector128(vresult, 0);
                var vright = Avx.ExtractVector128(vresult, 1);
                var vsum = Sse2.Add(vleft, vright);

                value = vsum.ToScalar();

                while (i < source.Length)
                {
                    value += pSource[i];
                    i += 1;
                }
            }

            return value / source.Length;
        }

        private static unsafe double Sum(ReadOnlySpan<double> source)
        {
            double value;
            var i = Vector256<double>.Count;

            fixed (double* pSource = source)
            {
                Vector256<double> vresult = Avx.LoadVector256(pSource);

                while (i + Vector256<double>.Count < source.Length)
                {
                    vresult = Avx.Add(vresult, Avx.LoadVector256(pSource + i));
                    i += Vector256<double>.Count;
                }

                vresult = Avx.HorizontalAdd(vresult, vresult);

                var vleft = Avx.ExtractVector128(vresult, 0);
                var vright = Avx.ExtractVector128(vresult, 1);
                var vsum = Sse2.Add(vleft, vright);

                value = vsum.ToScalar();

                while (i < source.Length)
                {
                    value += pSource[i];
                    i += 1;
                }
            }

            return value;
        }
    }
}
