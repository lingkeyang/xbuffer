﻿using System.Text.RegularExpressions;

namespace xbuffer
{
    /// <summary>
    /// 原型语法解析工具
    /// </summary>
    public class Proto
    {
        public Proto_Class[] class_protos;

        public Proto(string proto)
        {
            var matchs = Regex.Matches(proto, @"((class)|(struct))\s*(\w+)\s*{\s*((\w+):([\[|\]|\w]+);\s*)*}");
            class_protos = new Proto_Class[matchs.Count];
            for (int i = 0; i < matchs.Count; i++)
            {
                class_protos[i] = new Proto_Class(matchs[i]);
            }
        }
    }

    /// <summary>
    /// 变量结构
    /// </summary>
    public class Proto_Variable
    {
        public string Var_Type;                             // 变量类型
        public string Var_Name;                             // 变量名
        public bool IsArray;                                // 是否是数组

        public Proto_Variable(string name, string type)
        {
            Var_Name = name;
            if (type.Contains("["))
            {
                Var_Type = type.Substring(1, type.Length - 2);
                IsArray = true;
            }
            else
            {
                Var_Type = type;
                IsArray = false;
            }
        }
    }

    /// <summary>
    /// 类结构
    /// </summary>
    public class Proto_Class
    {
        public string Class_Type;                               // 类型 例如 class struct
        public string Class_Name;                               // 类名
        public Proto_Variable[] Class_Variables;                // 变量列表

        public Proto_Class(Match match)
        {
            Class_Type = match.Groups[1].Value;
            Class_Name = match.Groups[4].Value;

            var varNames = match.Groups[6].Captures;
            var varTypes = match.Groups[7].Captures;
            Class_Variables = new Proto_Variable[varNames.Count];
            for (int i = 0; i < Class_Variables.Length; i++)
            {
                Class_Variables[i] = new Proto_Variable(varNames[i].Value, varTypes[i].Value);
            }
        }
    }
}