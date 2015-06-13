﻿using UnityEngine;
using System.Collections;
using System.Text;

public class SaveCode : State 
{
    protected LineInfo m_fileName;
    protected int m_curIndex;

    public override void onInit() 
    {
        m_fileName = new LineInfo();
    }

    public override void onSwitchIn() 
    {
        string pureBasName = m_stateMgr.CUR_CODE_FILE_NAME;

        // remove the extesion name
        if( !string.IsNullOrEmpty(pureBasName) )
            pureBasName = pureBasName.Substring(0, pureBasName.Length - 4);

        m_fileName = new LineInfo(pureBasName);
        m_curIndex = 0;

        m_textDisplay.Clean();
        m_textDisplay.DrawText(0, 0, "Input file name:");
        m_textDisplay.DrawText(0, 1, pureBasName);
        m_textDisplay.Refresh();

        m_textDisplay.SetCursor(true, 0, 1);
    }

    public override void onInput(KCode key) 
    {
        int newIndex = m_fileName.KeyInput(key, m_curIndex);
        if (newIndex >= 0)
        {
            m_curIndex = newIndex;
        }
        else
        {
            switch (key)
            {
                case KCode.Escape:
                    m_stateMgr.GotoState(StateEnums.eStateMenu);
                    return;
                case KCode.Return:
                    CodeMgr.SharedInstance.SaveSourceCode(m_fileName.TEXT + ".BAS", m_stateMgr.CUR_SOURCE_CODE);
                    m_stateMgr.GotoState(StateEnums.eStateMenu);
                    return;
                default:
                    break;
            }
        }

        refresh();
    }

    protected void refresh()
    {
        m_textDisplay.Clean();
        m_textDisplay.DrawText(0, 0, "Input file name:");
        m_textDisplay.DrawText(0, 1, m_fileName.TEXT);
        m_textDisplay.SetCursor(true, m_curIndex, 1);
        m_textDisplay.Refresh();
    }

}
