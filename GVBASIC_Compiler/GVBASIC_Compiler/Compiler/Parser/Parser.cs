﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GVBASIC_Compiler.Compiler
{
    /// <summary>
    /// parser 
    /// </summary>
    class Parser
    {
        protected Dictionary<int, int> m_labelTable = null;
        protected List<CodeLine> m_codeLines = null;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="tokenizer"></param>
        public Parser( Tokenizer tokenizer )
        {
            tokenizer.Reset();
            m_codeLines = new List<CodeLine>();
            List<Token> tokenBuff = new List<Token>();

            // read all the tokens into codeline struct
            while( tokenizer.IsFinish == false )
            {
                Token t = tokenizer.GetNextToken();
                bool endLine = false;

                if( t.m_type == TokenType.eRem )        // filter the comment 
                {
                    tokenizer.SkipToNextLine();
                    endLine = true;
                }
                else if( t.m_type == TokenType.eEOL )   // omit end of line 
                {
                    endLine = true;
                }
                else if( t.m_type == TokenType.eError )
                {
                    // throw error , lex error.
                    throw new Exception("Error token in line " + tokenBuff[0].ToString() );
                }
                
                if( endLine )
                {
                    // save the line 
                    if (tokenBuff.Count > 0)
                    {
                        m_codeLines.Add(new CodeLine(tokenBuff));
                        tokenBuff.Clear();
                    }
                }
                else
                {
                    tokenBuff.Add(t);
                }
            }

            // add the last line 
            if (tokenBuff.Count > 0)
            {
                m_codeLines.Add(new CodeLine(tokenBuff));
            }
        }

        /// <summary>
        /// do parse
        /// </summary>
        public void DoParse()
        {
            sortCodeLines();

            //TODO 
        }


        //----------------------------- private functions ----------------------------


        /// <summary>
        /// sort code lines 
        /// </summary>
        public void sortCodeLines()
        {
            if( m_codeLines == null )
            {
                throw new Exception("[Parse]: no codelines.");
            }

            // sort the codelines
            m_codeLines.Sort((CodeLine lineA, CodeLine lineB) => { return lineA.m_lineNum - lineB.m_lineNum; });
        }

    }
}
