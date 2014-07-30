using System;
using SharpTestsEx;
using SharpTestsEx.Assertions;

namespace ImageShrinkerTests
{
    public static class SharpTestsExExtensions
    {
        public static IComparableBeConstraints<Double> RoughlyEqualTo(this IComparableBeConstraints<Double> constraint, Double expected, Double tolerance)
        {
            var assertion = new Assertion<Double, IComparable>("Be roughly equal to", expected,
                                                                a => Math.Abs(a - expected) <= tolerance);
            constraint.AssertionInfo.AssertUsing<Double>(assertion);
            return constraint;
        }
    } 
}