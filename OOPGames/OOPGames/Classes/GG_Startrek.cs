using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class GG_StartrekPainter : GG_IStartrekPainter
    {   //Entweder oder, konnte mich nicht genug mit den Vor/Nachteilen beschäftigen
        //List<GG_Meteo> _Meteos = new List<GG_Meteo>();
        //GG_Meteo [] _MeteosArray;    
    }

    public class GG_Meteo : GG_IMeteo
    {
        private int _PositionRow;
        private int _PositionColum;
        public int PositionRow { get { return _PositionRow; } }

        public int PositionColum { get { return _PositionColum; } }

        public GG_Meteo()
        {
            _PositionRow = 0;
            _PositionColum = 0;//Zufallszahl ersetzen!
        }

        public void UpdateMeteo()
        {
            _PositionRow++;
        }
    }
}
