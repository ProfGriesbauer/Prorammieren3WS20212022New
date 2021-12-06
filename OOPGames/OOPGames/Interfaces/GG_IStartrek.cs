using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{   //StartrekPainter
    //Übernimmt Steuerung der Meteoriten
    //Überprüft Kollisionen
    //Zeichnet Spielfeld
    //Methode _CurrentPainter.PaintGameField() wird alle 40ms abgerufen (MainWindow.xaml.cs Zeile 97)
    public interface GG_IStartrekPainter : IPaintGame
    {   bool Collison { get; }
        //ToDo:
        //-Erbung von entsprechenden Framework-Interfaces
        //-Implementierung der IPaintGame Methoden
        //Counter für Aufruf der PaintGameField() Methode als Attribut -> daraus können Taktzeiten
        //für neue Meteoritenerzeugung und das Weiterbewegen der Meteoriten bestimmt werden

        //PaintGameField muss der reihenach:
        //-Spielfeld zeichnen (Matrix auswerten, entsprechende Symbole setzen), siehe IPaintGame
        //--> PaintGameField-Methode, in dieser dann die folgenden Methoden bei bestimmter Anzahl
        //von Aufrufen, aufrufen:
        //-nach bestimmten Rythmus neue Meteoobjekte erzeugen und in einer Liste gehalten werden 
        void spawnMeteos();

        //-nach bestimmten Rythmus alle Meteoobjekte aktuallisieren
        void moveMetos();

        //-bei allen Meteoobjekten die Position abrufen und entsprechend im Spielfeld eintragen
        //  wenn Meteorit das Spielfeld damit verlässt aus Liste löschen https://www.csharp-examples.net/foreach/
        //-auf Kollision prüfen und im Fall von Kollision entsprechende Bildschirmausgabe erzeugen 
        void checkCollison();
        //  Kollison muss dann den Wert von CheckifPlayerWon in der Rulesklasse auf true setzen, dann wurd nach Playerinteraktion(Mausklick)
        // das Spiel wirklich beendet und die Anzeige entsprechend gestalltet
        //Methode muss durch PaintGameField mit entsprechendem Cast auf GG_IStartrekGamefield aufgerufen werden 
        void PaintStartrekGameField(Canvas canvas, GG_IStartrekGamefield currentField);

    }


    public interface GG_IStartrekGamefield : IGameField
    {
        int this[int r, int c] { get; set; }
        //ToDo: Vererbung
        // 6x6 Matrix
    }

    public interface GG_IStartrekRules : IGameRules
    {
        //Hält Gamefield!
        //Muss bei Painter abfragen, ob Kollision vorliegt --> Collsion-Variable
        
    }

    public interface GG_IMeteo
    {
        int PositionRow { get; }
        int PositionColum { get; }

        //Anforderungen für Konstruktor(Kann hier nicht definiert werden):
        //erzeugt Zufallswert für PositionColum und setzt Row auf 0

        //Erhöht PositionRow um 1
        void UpdatePos();
    }

    //ToDO: 
    //Interface: neues für GG_IStartrekMove erbt von IPlaymove und IKeymove
    //Hat Methode mit getter für direction( int direction{get;})
    //Evtl auch gleich Klasse GG_StartrekMove mitimplementieren
    public interface GG_IStartrekMove : IPlayMove, IKeyMove
    {

    }
}