﻿using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace MedicalStaff.WebService.Core.Helpers.Analysers
{
    /// <summary>
    /// 
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// Determines whether the sequence is empty or not.
        /// </summary>
        /// <param name="_">This current sequence.</param>
        /// <returns><see langword="true"></see> if this current sequence is empty; otherwise <see langword="false"></see>.</returns>
        public static Boolean Empty(this IEnumerable<Char> _) => !_.Any<Char>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static Boolean Implements<TTarget>(this Type _) => _.GetInterfaces().Contains(typeof(TTarget));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="targetValues"></param>
        /// <returns></returns>
        public static String RemoveSpecifically(this IEnumerable<Char> _, IEnumerable<Char> targetValues)
            => _.Where(currentCharacter => !currentCharacter.EqualsAny(targetValues)).Join();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="targetValues"></param>
        /// <returns></returns>
        public static Boolean EqualsAny(this Char _, IEnumerable<Char> targetValues)
            => targetValues.Any<Char>(character => _.Equals(character));

        /// <summary>
        /// Concatenates the members of this current <see cref="IEnumerable{T}"/> instance implementation.
        /// </summary>
        /// <param name="_">This <see cref="IEnumerable{T}"/> current instance implementation.</param>
        /// <returns>The members of this current <see cref="IEnumerable{T}"/> instance implementation concatenated.</returns>
        public static String Join(this IEnumerable<Char> _) => String.Concat(_);

        /// <summary>
        /// Converts this <see langword="enum"></see> component value into its <see cref="Int32"/> equivalent value.
        /// </summary>
        /// <param name="_">This <see cref="HttpStatusCode"/> current value.</param>
        /// <returns></returns>
        public static Int32 ToInt32(this HttpStatusCode _) => (Int32)_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public static Boolean GreaterThan(this Int32 _, Int32 targetValue) => _ > targetValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static Boolean Default(this Guid _) => _.Equals(Guid.Empty);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="_"></param>
        /// <returns></returns>
        public static IEnumerable<TOut> As<TIn, TOut>(this IEnumerable<TIn> _) => (IEnumerable<TOut>)_;
    }
}