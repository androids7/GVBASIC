﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVBASIC_Compiler.Compiler
{
    public class SymbolTable
    {
        protected Dictionary<string, Symbol> m_symbolDic;

        /// <summary>
        /// constructor 
        /// </summary>
        public SymbolTable()
        {
            m_symbolDic = new Dictionary<string, Symbol>();
        }

        /// <summary>
        /// define a symbol 
        /// </summary>
        /// <param name="sym"></param>
        public void Define( Symbol sym )
        {
            m_symbolDic.Add(sym.NAME, sym); 
        }

        /// <summary>
        /// return a symbol 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Symbol Resolve( string name )
        {
            if( m_symbolDic.ContainsKey( name ) )
                return m_symbolDic[name];

            Symbol symbol = null;

            if (name.EndsWith("%"))           // int value 
                symbol = new VarSymbol(Symbol.VAR, name, new BaseData(0));
            else if (name.EndsWith("$"))      // string value 
                symbol = new VarSymbol(Symbol.VAR, name, new BaseData(""));
            else                                    // float value 
                symbol = new VarSymbol(Symbol.VAR, name, new BaseData(0.0f));

            m_symbolDic.Add(name, symbol);

            return symbol;
        }

        /// <summary>
        /// has symbol type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasSymbolType( int type, string name )
        {
            if( m_symbolDic.ContainsKey( name ) )
            {
                if (m_symbolDic[name].TYPE == type )
                    return true;
            }

            return false;
        }

    }
}
