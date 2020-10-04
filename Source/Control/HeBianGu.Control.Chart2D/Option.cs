﻿using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{



    public class Option
    {

        public static readonly Color First = (Color)ColorConverter.ConvertFromString("#c23531");
        public static readonly Color Second = (Color)ColorConverter.ConvertFromString("#2f4554");
        public static readonly Color Third = (Color)ColorConverter.ConvertFromString("#61a0a8");
        public static readonly Color Forth = (Color)ColorConverter.ConvertFromString("#d48265");


        /// <summary> 默认颜色列表 </summary>
        public Brush[] Color { get; set; }

        public Brush Background { get; set; }

    }

    /// <summary> 标题 </summary>
    public class Title : Label
    {

    }

    /// <summary> 图例 </summary>
    public class Legend : ListBox
    {

    }



    public class DataTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value?.ToString().Split(',')?.Select(l =>
            {
                if (double.TryParse(l, out double d))
                {
                    return d;
                }
                else
                {
                    return double.NaN;
                }
            })?.ToObservable();
        }
    }

    public class DataArrayTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            List<double[]> result = new List<double[]>();

            var ss = value?.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in ss)
            {
                double[] ds = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)?.Select(l =>
                  {
                      if (double.TryParse(l, out double d))
                      {
                          return d;
                      }
                      else
                      {
                          return double.NaN;
                      }
                  })?.ToArray();

                result.Add(ds);
            }

            return result?.ToObservable();
        }
    }

    public class BrushArrayTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var result= value?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)?.Select(l =>
                 {
                     try
                     {
                        return (Color)ColorConverter.ConvertFromString(l);

                     }
                     catch (Exception ex)
                     { 
                         Debug.WriteLine(ex);

                         return Option.First;
                     }

                 })?.ToObservable();

            return result;
        }
    }

    public class DisplayTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value?.ToString().Split(',')?.ToObservable();
        }
    }

}
