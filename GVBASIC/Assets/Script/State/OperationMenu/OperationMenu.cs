﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OperationMenu : State 
{
    public TextDisplay m_textDisplay;

    protected int m_curItemIdx;
    protected List<string> m_itemList = new List<string>();
    protected int m_lineOffset;

    public override void onInit()
    {
        m_itemList.Add("New File..");
        m_itemList.Add("file 1");
        m_itemList.Add("file 2");
        m_itemList.Add("file 3");
        m_itemList.Add("file 4");
        m_itemList.Add("file 5");
        m_itemList.Add("file 6");
        m_itemList.Add("file 7");
        m_itemList.Add("file 8");
        m_itemList.Add("file 9");
    }

    public override void onSwitchIn()
    {
        m_textDisplay.SetCursor(false);
        m_lineOffset = 0;

        refreshList(0);
    }

    public override void onInput(KCode key)
    {
        switch( key )
        {
            case KCode.UpArrow:
                if (m_curItemIdx > 0)
                    refreshList(m_curItemIdx - 1);
                break;
            case KCode.DownArrow:
                if (m_curItemIdx < m_itemList.Count - 1)
                    refreshList(m_curItemIdx + 1);
                break;
            case KCode.Return:
                executeItem(m_curItemIdx);
                break;
            case KCode.Delete:
                if( m_curItemIdx > 0 )
                {
                    //TODO 
                }
                break;
            default:
                break;
        }
    }


    protected void refreshList( int index )
    {
        m_curItemIdx = index;

        // update the offset 
        if( m_curItemIdx - m_lineOffset >= Defines.TEXT_AREA_HEIGHT )
            m_lineOffset = m_curItemIdx - Defines.TEXT_AREA_HEIGHT + 1;
        else if( m_curItemIdx - m_lineOffset < 0 )
            m_lineOffset = m_curItemIdx ;

        m_textDisplay.Clean();

        int y = 0;
        for (int i = m_lineOffset; i < m_itemList.Count; i++ )
            m_textDisplay.DrawText(0, y++, m_itemList[i], i == m_curItemIdx);

        m_textDisplay.Refresh();
    }

    protected void executeItem( int index )
    {
        if( index == 0 )
        {
            // create a new file 
            //TODO 

            m_stateMgr.GotoState(StateEnums.eStateEditor);
        }
        else
        {
            // run the code file 

            //TODO 
        }
    }

}
