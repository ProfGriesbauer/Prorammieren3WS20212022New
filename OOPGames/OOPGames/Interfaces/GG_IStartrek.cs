﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{   //StartrekPainter
    //Übernimmt Steuerung der Meteoriten
    //Überprüft Kollisionen
    //Zeichnet Spielfeld
    //Methode _CurrentPainter.PaintGameField() wird alle 40ms abgerufen (MainWindow.xaml.cs Zeile 97)
    public interface GG_IStartrekPainter
    {
        //ToDo:
        //-Erbung von entsprechenden Framework-Interfaces
        //-Implementierung der IPaintGame Methoden
        //Counter für Aufruf der PaintGameField() Methode als Attribut -> daraus können Taktzeiten
        //für neue Meteoritenerzeugung und das Weiterbewegen der Meteoriten bestimmt werden

        //PaintGameField muss der reihenach:
        //-Spielfeld zeichnen (Matrix auswerten, entsprechende Symbole setzen)
        //-nach bestimmten Rythmus neue Meteoobjekte erzeugen und in einer Liste gehalten werden 
        //-nach bestimmten Rythmus alle Meteoobjekte aktuallisieren
        //-bei allen Meteoobjekten die Position abrufen und entsprechend im Spielfeld eintragen
        //  wenn Meteorit das Spielfeld damit verlässt aus Liste löschen https://www.csharp-examples.net/foreach/
        //-auf Kollision prüfen und im Fall von Kollision entsprechende Bildschirmausgabe erzeugen 
        //  Kollison muss dann den Wert von CheckifPlayerWon in der Rulesklasse auf true setzen, dann wurd nach Playerinteraktion(Mausklick)
        // das Spiel wirklich beendet und die Anzeige entsprechend gestalltet
    }


    public interface GG_IStartrekGamefield
    {
        //ToDo: Vererbung
        // 6x6 Matrix
    }

    public interface GG_IStartrekRules : IGameRules
    {
        //Hält Gamefield!
        //Muss bei Painter abfragen, ob Kollision vorliegt

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



}