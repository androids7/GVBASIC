﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVBASIC_Compiler.Compiler;

namespace GVBASIC_Compiler
{
    public class DebugAPI : IAPI
    {
        /// <summary>
        /// print 
        /// </summary>
        /// <param name="expList"></param>
        public void Print(List<BaseData> dataList)
        {
            int lastType = -1;
            bool closeTo = false;

            foreach (BaseData dat in dataList)
            {
                switch( dat.m_type )
                {
                    case BaseData.TYPE_FLOAT:
                        System.Console.Write(dat.m_floatVal.ToString());
                        break;
                    case BaseData.TYPE_INT:
                        System.Console.Write(dat.m_intVal.ToString());
                        break;
                    case BaseData.TYPE_STRING:
                        System.Console.Write(dat.m_stringVal);
                        break;
                    case BaseData.TYPE_NEXT_LINE:
                        if (lastType == BaseData.TYPE_FLOAT || lastType == BaseData.TYPE_INT || lastType == BaseData.TYPE_STRING)
                            System.Console.Write("\n");
                        break;
                    case BaseData.TYPE_SPACE:
                        break;
                    case BaseData.TYPE_TAB:
                        break;
                    case BaseData.TYPE_CLOSE_TO:
                        if (lastType == BaseData.TYPE_FLOAT || lastType == BaseData.TYPE_INT || lastType == BaseData.TYPE_STRING)
                            closeTo = true;
                        break;
                    default:
                        break;
                }

                lastType = dat.m_type;
            }

            if (!(lastType == BaseData.TYPE_CLOSE_TO && closeTo))
                System.Console.Write("\n");
        }
    }
}