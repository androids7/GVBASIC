﻿using System;
using System.Collections.Generic;
using System.Text;

// tokenizer 
namespace GVBASIC_Compiler.Compiler
{
    /// <summary>
    /// status of the tokenizer 
    /// </summary>
    enum LexStatus
    {
        eStart,
        eSymbol,
        eSpace,
        eIntNum,
        eRealNum,
        eString,
        eStringTrans,   // like '\n', '\t'
        eOpCode,
        eDelim,         // delimiter 
        //TODO 
    };

    /// <summary>
    /// lexer 
    /// </summary>
    class Tokenizer
    {
        protected string m_sourceCode;
        protected int m_curIndex;

        /// <summary>
        /// constructor 
        /// </summary>
        public Tokenizer()
        {
            m_sourceCode = null;
        }

        /// <summary>
        /// set source code 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource( string source )
        {
            m_sourceCode = source;
        }

        /// <summary>
        /// reset the tokenizer 
        /// </summary>
        public void Reset()
        {
            m_curIndex = 0;
        }

        /// <summary>
        /// return the next token 
        /// </summary>
        /// <returns></returns>
        public Token GetNextToken()
        {
            Token tok = new Token();
            StringBuilder buffer = new StringBuilder();
            LexStatus status = LexStatus.eStart;
            bool addToBuffer;
            bool isDone = false;

            for (; m_curIndex < m_sourceCode.Length && isDone == false; m_curIndex++ )
            {
                char c = m_sourceCode[m_curIndex];
                addToBuffer = true;

                switch( status )
                {
                    case LexStatus.eStart:
                        if( isWhiteChar( c ) )
                        {
                            addToBuffer = false;
                        }
                        else if( Char.IsNumber(c) )
                        {
                            status = LexStatus.eIntNum;
                        }
                        else if( c == '\"' )
                        {
                            addToBuffer = false;
                            status = LexStatus.eString;
                        }
                        else if( Char.IsLetter( c ) )
                        {
                            status = LexStatus.eSymbol;
                        }
                        //TODO 
                        break;
                    case LexStatus.eIntNum:
                        if( isWhiteChar( c ) )
                        {
                            addToBuffer = false;
                            isDone = true;
                        }
                        else if( c == '.' )
                        {
                            status = LexStatus.eRealNum;
                        }
                        //TODO 
                        break;
                    case LexStatus.eString:
                        if( c == '\\' )
                        {
                            addToBuffer = false;
                            status = LexStatus.eStringTrans;
                        }
                        else if( c == '\"' )
                        {
                            addToBuffer = false;
                            status = LexStatus.eStart;
                        }
                        //TODO 
                        break;
                    case LexStatus.eStringTrans:
                        if( c == 'n' )
                        {
                            c = '\n';
                        }
                        status = LexStatus.eString;
                        break;
                    case LexStatus.eSymbol:
                        //TODO 
                        break;
                    default:
                        break;
                }

                if( addToBuffer )
                {
                    buffer.Append(c);
                }
            }

            // confirm the token type 
            switch( status )
            {
                case LexStatus.eIntNum:
                    tok.m_type = TokenType.eIntNum;
                    tok.m_intVal = Int32.Parse(buffer.ToString());
                    break;
                case LexStatus.eString:
                    tok.m_type = TokenType.eString;
                    tok.m_strVal = buffer.ToString();
                    break;
                //TODO 
                default:
                    break;
            }

            return tok;
        }

        /// <summary>
        /// skip characters to next line 
        /// </summary>
        public void SkipToNextLine()
        {
            for( ; m_curIndex < m_sourceCode.Length; m_curIndex++ )
            {
                char c = m_sourceCode[m_curIndex];

                if( c == '\n' )
                {
                    break;
                }
            }
        }


        /// <summary>
        /// if is finished or not 
        /// </summary>
        /// <returns></returns>
        public bool IsFinish()
        {
            if( m_curIndex >= m_sourceCode.Length )
            {
                return true;
            }

            return false;
        }


        //------------------------ private functions ------------------------

        /// <summary>
        /// is white char 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected bool isWhiteChar( char c )
        {
            if( c == ' ' || c == '\t' )
            {
                return true;
            }

            return false;
        }

    }
}
